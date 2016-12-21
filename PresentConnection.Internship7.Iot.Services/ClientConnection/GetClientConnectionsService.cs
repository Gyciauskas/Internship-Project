using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.ServiceModels;
using ServiceStack;
using ServiceStack.Auth;

namespace PresentConnection.Internship7.Iot.Services
{
    public class GetClientConnectionsService : Service
    {
        public IClientConnectionService ClientConnectionsService { get; set; }
        public IAuthSession AuthSession { get; set; }

        public GetClientConnectionsResponse Any(GetClientConnections request)
        {
            var response = new GetClientConnectionsResponse
            {

                clientconnections = ClientConnectionsService.GetClientConnections(request.ClientId, AuthSession.UserAuthId)

            };

            return response;
        }
    }

}