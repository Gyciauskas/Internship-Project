using System.Collections.Generic;
using PresentConnection.Internship7.Iot.Domain;

namespace PresentConnection.Internship7.Iot.BusinessContracts
{
    public interface IClientRecipeService
    {
        string CreateClientRecipe(ClientRecipe clientRecipe);
        void UpdateClientRecipe(ClientRecipe clientRecipe);
        bool DeleteClientRecipe(string id);
        List<ClientRecipe> GetAllClientRecipes(string recipeId = "");
        ClientRecipe GetClientRecipe(string id);
    }
}
