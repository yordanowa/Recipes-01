﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recipes.Data.Models
{
    public class Recipe
    {
        public Recipe()
        {
            this.Ingredients = new HashSet<RecipeIngredient>();
            this.Images = new HashSet<Image>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public TimeSpan PreparationTime { get; set; }
        public TimeSpan CookingTime { get; set; }
        public int PortionCount { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public string AddedByUserId { get; set; }
        public virtual ApplicationUser AddedByUser { get; set; }
        public virtual ICollection<RecipeIngredient> Ingredients { get; set; }
        public virtual ICollection<Image> Images { get; set; }
    }
}
