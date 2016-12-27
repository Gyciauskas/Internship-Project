using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.ServiceModels;
using ServiceStack;

namespace PresentConnection.Internship7.Iot.Services
{
    public class GetClientService : Service
    {
        public IClientService ClientService { get; set; }

        public GetClientResponse Any(GetClient request)
        {
            var response = new GetClientResponse
            {
                client = ClientService.GetClient(request.Id)
            };
            return response;
        }
    }
}
