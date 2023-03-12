using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recipes.Models
{
    public class SingleRecipeViewModel
    {
        public SingleRecipeViewModel()
        {
            this.Ingredients = new HashSet<RecipeIngredientInputModel>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string CategoryName { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ImageUrl { get; set; }
        public string Instructions { get; set; }
        public TimeSpan PreparationTime { get; set; }
        public TimeSpan CookingTime { get; set; }
        public int PortionCount { get; set; }
        public IEnumerable<RecipeIngredientInputModel> Ingredients { get; set; }
    
}
}
