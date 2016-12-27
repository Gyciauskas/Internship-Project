using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.ServiceModels;
using ServiceStack;

namespace PresentConnection.Internship7.Iot.Services
{
    public class GetRecipeService : ServiceBase
    {
        public IRecipeService RecipeService { get; set; }

        public GetRecipeResponse Any(GetRecipe request)
        {
            var response = new GetRecipeResponse
            {
                Result = RecipeService.GetRecipe(request.Id)
            };

            return response;
        }
    }
}
