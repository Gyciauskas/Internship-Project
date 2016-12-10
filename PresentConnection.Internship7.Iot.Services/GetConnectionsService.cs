using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.ServiceModels;
using ServiceStack;

namespace PresentConnection.Internship7.Iot.Services
{
    public class GetConnectionsSerive : Service
    {
        public IConnectionService ConnectionService { get; set; }

        public GetConnectionsResponse Any(GetConnections request)
        {
            var response = new GetConnectionsResponse
            {
                Connections = ConnectionService.GetAllConnections(request.Name)
            };
            return response;
        }
    }
}