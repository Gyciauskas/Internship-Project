using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.Domain;
using PresentConnection.Internship7.Iot.ServiceModels;
using ServiceStack;
using ServiceStack.Auth;


namespace PresentConnection.Internship7.Iot.Services
{
    public class CreateClientConnectionService : Service 
    {
        public IClientConnectionService ClientConnectionService { get; set; }
        public IAuthSession AuthSession { get; set; }
        public CreateClientConnectionResponse Any(CreateClientConnection request)
        {
            var response = new CreateClientConnectionResponse();

            var clientconnection = new ClientConnection
            {
                ConnectionId = request.ConnectionId,
                ClientId = request.ClientId
            };
            string ClientId = AuthSession.UserAuthId;
            ClientConnectionService.CreateClientConnection(clientconnection, ClientId);

            return response;
        }
    }
}