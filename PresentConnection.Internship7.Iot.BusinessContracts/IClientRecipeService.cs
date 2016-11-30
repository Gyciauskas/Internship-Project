using System.Collections.Generic;
using PresentConnection.Internship7.Iot.Domain;

namespace PresentConnection.Internship7.Iot.BusinessContracts
{
    public interface IClientRecipeService
    {
        void CreateClientRecipe(ClientRecipe clientRecipe);
        void UpdateClientRecipe(ClientRecipe clientRecipe);
        bool DeleteClientRecipe(string id);
        List<ClientRecipe> GetAllClientRecipes(string clientId = "");
        ClientRecipe GetClientRecipe(string id);
    }
}
