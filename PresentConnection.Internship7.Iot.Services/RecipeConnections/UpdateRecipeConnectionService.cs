using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.ServiceModels;
using ServiceStack;

namespace PresentConnection.Internship7.Iot.Services
{
    public class UpdateRecipeConnectionService : Service
    {
        public IRecipeConnectionService RecipeConnectionService { get; set; }

        public UpdateRecipeConnectionResponse Any(UpdateRecipeConnection request)
        {
            var response = new UpdateRecipeConnectionResponse();

            // Get and replace
            var recipeconnection = RecipeConnectionService.GetRecipeConnection(request.Id).PopulateWith(request);

            RecipeConnectionService.UpdateRecipeConnection(recipeconnection);

            return response;
        }
    }
}
