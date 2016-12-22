using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.ServiceModels;

namespace PresentConnection.Internship7.Iot.Services
{
    public class DeleteClientRecipeService : ServiceBase
    {
        public IClientRecipeService ClientRecipeService { get; set; }

        public DeleteClientRecipeResponse Any(DeleteClientRecipe request)
        {
            var response = new DeleteClientRecipeResponse
            {
                Result = ClientRecipeService.DeleteClientRecipe(request.Id, UserSession.UserAuthId)
            };
            
            return response;
        }
    }
}
