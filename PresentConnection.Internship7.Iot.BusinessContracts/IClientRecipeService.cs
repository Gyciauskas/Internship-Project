using System.Collections.Generic;
using PresentConnection.Internship7.Iot.Domain;

namespace PresentConnection.Internship7.Iot.BusinessContracts
{
    public interface IClientRecipeService
    {
        void CreateClientRecipe(ClientRecipe clientRecipe, string responsibleClientId);
        void UpdateClientRecipe(ClientRecipe clientRecipe, string responsibleClientId);
        bool DeleteClientRecipe(string id, string responsibleClientId);
        List<ClientRecipe> GetAllClientRecipes(string clientId, string responsibleClientId);
        ClientRecipe GetClientRecipe(string id, string responsibleClientId);
    }
}
