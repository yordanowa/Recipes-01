using Recipes.Data;
using Recipes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recipes.Services
{
    public class RecipeService:IRecipeService
    {
        private readonly ApplicationDbContext db;

        public RecipeService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public SingleRecipeViewModel GetById(int id)
        {
            var recipe = this.db.Recipes.Where(x => x.Id == id)
                .Select(x => new SingleRecipeViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    CategoryName = x.Category.Name,
                    PreparationTime = x.PreparationTime,
                    CookingTime = x.CookingTime,
                    PortionCount = x.PortionCount,
                    Instructions = x.Description,
                    ImageUrl = "/img/" + x.Images.FirstOrDefault().Id + "." + x.Images.FirstOrDefault().Extention,
                    Ingredients = x.Ingredients.Select(x => new RecipeIngredientInputModel
                    {
                        Name = x.Ingredient.Name,
                        Quantity = x.Quantity
                    })
                }).FirstOrDefault();

            return recipe;
        }
    }
}
