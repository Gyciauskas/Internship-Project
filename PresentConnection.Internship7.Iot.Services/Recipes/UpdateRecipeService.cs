using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.ServiceModels;
using ServiceStack;

namespace PresentConnection.Internship7.Iot.Services
{
    public class UpdateRecipeService : ServiceBase
    {
        public IRecipeService RecipeService { get; set; }

        public UpdateRecipeResponse Any(UpdateRecipe request)
        {
            var response = new UpdateRecipeResponse();

            // Get and replace
            var recipe = RecipeService.GetRecipe(request.Id).PopulateWith(request);

            RecipeService.UpdateRecipe(recipe);

            return response;
        }
    }
}
