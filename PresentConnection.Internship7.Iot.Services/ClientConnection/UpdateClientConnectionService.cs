using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.ServiceModels;
using ServiceStack;
using ServiceStack.Auth;

namespace PresentConnection.Internship7.Iot.Services
{
    public class UpdateClientConnectionService : ServiceBase
    {
        public IClientConnectionService ClientConnectionService { get; set; }
        
        public UpdateClientConnectionResponse Any(UpdateClientConnection request)
        {
            var response = new UpdateClientConnectionResponse();

            var clientConnection = ClientConnectionService.GetClientConnection(request.Id, UserSession.UserAuthId);
                
            clientConnection = clientConnection?.PopulateWith(request);
            ClientConnectionService.UpdateClientConnection(clientConnection, UserSession.UserAuthId);

            response.Result = clientConnection;
            return response;
        }
    }
}
