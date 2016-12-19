using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.ServiceModels;
using ServiceStack;

namespace PresentConnection.Internship7.Iot.Services
{
    public class DeleteRecipeConnectionService : Service
    {
        public IRecipeConnectionService RecipeConnectionService { get; set; }

        public DeleteRecipeConnectionResponse Any(DeleteRecipeConnection request)
        {
            var response = new DeleteRecipeConnectionResponse
            {
                Result = RecipeConnectionService.DeleteRecipeConnection(request.Id.ToString())
            };

            return response;
        }
    }
}
