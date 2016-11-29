using System.Collections.Generic;
using PresentConnection.Internship7.Iot.Domain;

namespace PresentConnection.Internship7.Iot.BusinessContracts
{
    public interface IRecipeService
    {
        string CreateRecipe(Recipe recipe);
        void UpdateRecipe(Recipe recipe);
        bool DeleteRecipe(string id);
        List<Recipe> GetAllRecipes(string name = "");
        Recipe GetRecipe(string id);
    }
}
