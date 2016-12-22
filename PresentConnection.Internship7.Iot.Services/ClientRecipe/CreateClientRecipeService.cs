using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.Domain;
using PresentConnection.Internship7.Iot.ServiceModels;
using ServiceStack;

namespace PresentConnection.Internship7.Iot.Services
{
    public class CreateClientRecipeService : Service
    {
        public IClientRecipeService ClientRecipeService { get; set; }

        public CreateClientRecipeResponse Any(CreateClientRecipe request)
        {
            var response = new CreateClientRecipeResponse();

            var clientRecipe = new ClientRecipe
            {
                RecipeId = request.RecipeId,
                ClientId = request.ClientId
            };
            string clientId = request.ClientId;

            ClientRecipeService.CreateClientRecipe(clientRecipe, clientId);

            return response;
        }
    }
}