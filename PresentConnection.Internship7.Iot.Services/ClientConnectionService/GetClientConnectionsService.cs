using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.ServiceModels;
using ServiceStack;

namespace PresentConnection.Internship7.Iot.Services
{
    public class GetClientConnectionsService : Service
    {
        public IClientConnectionService ClientConnectionsService { get; set; }

        public GetClientConnectionsResponse Any(GetClientConnections request)
        {
            var response = new GetClientConnectionsResponse
            {

                clientconnections = ClientConnectionsService.GetClientConnections(request.ConnectionId, request.ClientId)

            };

            return response;
        }
    }

}