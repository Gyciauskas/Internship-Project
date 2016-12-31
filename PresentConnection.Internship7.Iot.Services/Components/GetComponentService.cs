using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.ServiceModels;
using ServiceStack;

namespace PresentConnection.Internship7.Iot.Services
{
    public class GetComponentService : Service
    {
        public IComponentService ComponentService { get; set; }

        public GetComponentResponse Any(GetComponent request)
        {
            var response = new GetComponentResponse
            {
                Result = ComponentService.GetComponent(request.Id)
            };
            return response;
        }
    }
}
