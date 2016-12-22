using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.ServiceModels;
using ServiceStack;

namespace PresentConnection.Internship7.Iot.Services
{
    public class GetClientsService : ServiceBase
    {
        public IClientService ClientsService { get; set; }

        public GetClientsResponse Any(GetClients request)
        {
            var response = new GetClientsResponse
            {
                Result = ClientsService.GetAllClients()
            };

            return response;
        }
    }
}
