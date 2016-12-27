using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.ServiceModels;
using ServiceStack;
using ServiceStack.Auth;

namespace PresentConnection.Internship7.Iot.Services
{
    public class GetClientConnectionsService : ServiceBase
    {
        public IClientConnectionService ClientConnectionsService { get; set; }

        public GetClientConnectionsResponse Any(GetClientConnections request)
        {
            var response = new GetClientConnectionsResponse
            {
                Result = ClientConnectionsService.GetClientConnections(request.ClientId, UserSession.UserAuthId)

            };

            return response;
        }
    }

}