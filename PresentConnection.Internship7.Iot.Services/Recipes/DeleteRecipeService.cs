using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.ServiceModels;
using ServiceStack;

namespace PresentConnection.Internship7.Iot.Services
{
    public class DeleteRecipeService : ServiceBase
    {
        public IRecipeService RecipeService { get; set; }

        public DeleteRecipeResponse Any(DeleteRecipe request)
        {
            var response = new DeleteRecipeResponse
            {
                Result = RecipeService.DeleteRecipe(request.Id)
            };

            return response;
        }
    }
}
