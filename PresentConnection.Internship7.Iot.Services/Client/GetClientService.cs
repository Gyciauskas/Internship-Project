using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.ServiceModels;
using ServiceStack;

namespace PresentConnection.Internship7.Iot.Services
{
    public class GetClientService : ServiceBase
    {
        public IClientService ClientService { get; set; }

        public GetClientResponse Any(GetClient request)
        {
            var response = new GetClientResponse
            {
                Result = ClientService.GetClient(request.Id)
            };
            return response;
        }
    }
}
