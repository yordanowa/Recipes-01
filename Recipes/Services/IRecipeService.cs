using Recipes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recipes.Services
{
    public interface IRecipeService
    {
        public SingleRecipeViewModel GetById(int id);
        public IEnumerable<IndexPageRecipeViewModel> GetRandom(int count);
    }
}
