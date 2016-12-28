using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.ServiceModels;
using ServiceStack;

namespace PresentConnection.Internship7.Iot.Services
{
    public class DeleteComponentService : Service
    {
        public IComponentService ComponentService { get; set; }

        public DeleteComponentResponse Any(DeleteComponent request)
        {
            var response = new DeleteComponentResponse
            {
                Result = ComponentService.DeleteComponent(request.Id)
            };

            return response;
        }
    }
}
