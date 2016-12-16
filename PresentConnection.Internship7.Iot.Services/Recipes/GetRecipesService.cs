using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.ServiceModels;
using ServiceStack;

namespace PresentConnection.Internship7.Iot.Services
{
    public class GetRecipesService : Service
    {
        public IRecipeService RecipeService { get; set; }

        public GetRecipesResponse Any(GetRecipes request)
        {
            var response = new GetRecipesResponse
            {
                Result = RecipeService.GetAllRecipes(request.Name)
            };

            return response;
        }
    }
}
