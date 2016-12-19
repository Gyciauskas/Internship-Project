using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.ServiceModels;
using ServiceStack;

namespace PresentConnection.Internship7.Iot.Services
{
    public class DeleteClientRecipeService : Service
    {
        public IClientRecipeService ClientRecipeService { get; set; }

        public DeleteClientRecipeResponse Any(DeleteClientRecipe request)
        {
            var response = new DeleteClientRecipeResponse
            {
                IsDeleted = ClientRecipeService.DeleteClientRecipe(request.Id, request.ClientId)
            };
            
            return response;
        }
    }
}
