using PresentConnection.Internship7.Iot.Domain;
using System.Collections.Generic;

namespace PresentConnection.Internship7.Iot.BusinessContracts
{
    public interface IRecipeConnectionService
    {
        void CreateRecipeConnection(RecipeConnection recipeconnection);
        void UpdateRecipeConnection(RecipeConnection recipeconnection);
        bool DeleteRecipeConnection(string id);
        List<RecipeConnection> GetAllRecipeConnections(string name = "");
        RecipeConnection GetRecipeConnection(string id);
    }
}
