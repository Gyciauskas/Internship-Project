using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.Domain;
using PresentConnection.Internship7.Iot.ServiceModels;
using ServiceStack;
using ServiceStack.Auth;


namespace PresentConnection.Internship7.Iot.Services
{
    public class CreateClientConnectionService : ServiceBase 
    {
        public IClientConnectionService ClientConnectionService { get; set; }
        
        public CreateClientConnectionResponse Any(CreateClientConnection request)
        {
            var response = new CreateClientConnectionResponse();


            var clientConnection = new ClientConnection().PopulateWith(request);

            ClientConnectionService.CreateClientConnection(clientConnection, UserSession.UserAuthId);

            response.Result = clientConnection;
            return response;
        }
    }
}