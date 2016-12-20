using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.Domain;
using PresentConnection.Internship7.Iot.ServiceModels;
using ServiceStack;

namespace PresentConnection.Internship7.Iot.Services
{
    public class CreateRecipeConnectionService : Service
    {
        public IRecipeConnectionService RecipeConnectionService { get; set; }

        public CreateRecipeConnectionResponse Any(CreateRecipeConnnection request)
        {
            var response = new CreateRecipeConnectionResponse();

            var recipeconnection = new RecipeConnection().PopulateWith(request);

            RecipeConnectionService.CreateRecipeConnection(recipeconnection);

            return response;
        }
    }
}
