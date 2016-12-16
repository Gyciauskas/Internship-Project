using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.Domain;
using PresentConnection.Internship7.Iot.ServiceModels;
using ServiceStack;

namespace PresentConnection.Internship7.Iot.Services
{
    public class CreateRecipeService : Service
    {
        public IRecipeService RecipeService { get; set; }

        public CreateRecipeResponse Any(CreateRecipe request)
        {
            var response = new CreateRecipeResponse();

            var recipe = new Recipe
            {
                Name = request.Name,
                UniqueName = request.UniqueName
            };

            RecipeService.CreateRecipe(recipe);

            return response;
        }
    }
}
