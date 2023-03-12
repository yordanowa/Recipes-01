using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Recipes.Data;
using Recipes.Models;
using Recipes.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Recipes.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext db;
        private readonly IRecipeService service;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db, IRecipeService service)
        {
            _logger = logger;
            this.db = db;
            this.service = service;
        }

        public IActionResult Index()
        {
            var viewModel = new IndexViewModel
            {
                CategoriesCount = this.db.Categories.Count(),
                RecipesCount = this.db.Recipes.Count(),
                IngredientsCount = this.db.Ingredients.Count(),
                ImagesCount = this.db.Images.Count(),
                RandomRecipes = this.service.GetRandom(10)
            };
            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
