using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.ServiceModels;
using ServiceStack;

namespace PresentConnection.Internship7.Iot.Services
{
    public class GetComponentsService : Service
    {
        public IComponentService ComponentService { get; set; }

        public GetComponentsResponse Any(GetComponents request)
        {
            var response = new GetComponentsResponse
            {
                Components = ComponentService.GetAllComponents(request.ModelName)
            };
            return response;
        }
    }
}
