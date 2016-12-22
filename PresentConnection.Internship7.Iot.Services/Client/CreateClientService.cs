using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.Domain;
using PresentConnection.Internship7.Iot.ServiceModels;
using ServiceStack;

namespace PresentConnection.Internship7.Iot.Services
{
    public class CreateClientService : ServiceBase
    {
        public IClientService ClientService { get; set; }

        public CreateClientResponse Any(CreateClient request)
        {
            var response = new CreateClientResponse();

            var client = new Client
            {
                Name = request.Name,
                Subscriptions = request.Subscriptions
            };

            ClientService.CreateClient(client);

            response.Result = client;
            return response;
        }
    }
}
