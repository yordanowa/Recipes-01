using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Recipes.Data;
using Recipes.Data.Models;
using Recipes.Models;
using Recipes.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Recipes.Controllers
{
    public class RecipeController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly string[] allowedExtensions = new[] { "png", "jpg", "jfif" };
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IRecipeService recipeService;

        public RecipeController(ApplicationDbContext db,
            IWebHostEnvironment webHostEnvironment, 
            UserManager<ApplicationUser> userManager,
            IRecipeService recipeService)
        {
            this.db = db;
            this.webHostEnvironment = webHostEnvironment;
            this.userManager = userManager;
            this.recipeService = recipeService;
        }
        public IActionResult Index() //All Recipes
        {
            var model = db.Recipes.Select(x => new RecipeInputModel
            {
                Id = x.Id,
                Name = x.Name,
                CategoryName = x.Category.Name,
                ImageURL = $"/img/{x.Images.FirstOrDefault().Id}.{x.Images.FirstOrDefault().Extention}"

            }).ToList();
            return View(model);
        }
        public IActionResult ById(int id) //Single recipe info
        {
            var recipeViewModel = this.recipeService.GetById(id);
            return this.View(recipeViewModel);
        }
        [HttpGet]
        public IActionResult Create()
        {
            var categories = db.Categories.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();
            var model = new RecipeInputModel { Categories = categories };
            return this.View(model);
        }
        [HttpPost]  // Add recipe in DB
        public async Task<IActionResult> Create(RecipeInputModel model)
        {
            var recipe = new Recipe
            {
                Name = model.Name,
                CategoryId = model.CategoryId,
                Description = model.Description,
                PreparationTime = TimeSpan.FromMinutes(model.PreparationTime),
                CookingTime = TimeSpan.FromMinutes(model.CookingTime),
                PortionCount = model.PortionCount,

            };
            foreach (var item in model.Ingredients)
            {
                Ingredient ingredient = db.Ingredients.FirstOrDefault(x => x.Name == item.Name);
                if (ingredient == null)
                {
                    ingredient = new Ingredient { Name = item.Name };
                }
                recipe.Ingredients.Add(new RecipeIngredient
                {
                    Ingredient = ingredient,
                    Quantity = item.Quantity
                });
            }
            var user = await userManager.GetUserAsync(this.User);
            recipe.AddedByUserId = user.Id;

            var extention = Path.GetExtension(model.Image.FileName).TrimStart('.');
            if (!this.allowedExtensions.Any(x => extention.EndsWith(x)))
            {
                throw new Exception($"Invalid image extension {extention}");
            }
            var dbImage = new Image // името на файла е = ID
            {
                Extention = extention,
                AddedByUserId = user.Id
            };

            var physicalPath = $"{this.webHostEnvironment.WebRootPath}/img/{dbImage.Id}.{extention}";
            using (FileStream fs = new FileStream(physicalPath, FileMode.Create))
            {
                model.Image.CopyTo(fs);
            }

            recipe.Images.Add(dbImage);
            this.db.Recipes.Add(recipe);
            this.db.SaveChanges();
            return this.Redirect("/");
        }
    }
}
