using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.ServiceModels;
using ServiceStack;
using ServiceStack.Auth;

namespace PresentConnection.Internship7.Iot.Services
{
    public class UpdateClientConnectionService : Service
    {
        public IClientConnectionService ClientConnectionService { get; set; }
        public IAuthSession AuthSession { get; set; }

        public UpdateClientConnectionResponse Any(UpdateClientConnection request)
        {
            var response = new UpdateClientConnectionResponse();


            var clientconnection = ClientConnectionService.GetClientConnection(request.Id, AuthSession.UserAuthId).PopulateWith(request);
            ClientConnectionService.UpdateClientConnection(clientconnection, AuthSession.UserAuthId);

            return response;
        }
    }
}
