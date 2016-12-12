using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.ServiceModels;
using ServiceStack;

namespace PresentConnection.Internship7.Iot.Services
{
    public class GetRecipeService : Service
    {
        public IRecipeService RecipeService { get; set; }

        public GetRecipeResponse Any(GetRecipe request)
        {
            var response = new GetRecipeResponse
            {
                Recipe = RecipeService.GetRecipe(request.Id)
            };

            return response;
        }
    }
}
