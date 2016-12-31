using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.ServiceModels;
using ServiceStack;

namespace PresentConnection.Internship7.Iot.Services
{
    public class GetConnectionsSerive : ServiceBase
    {
        public IConnectionService ConnectionService { get; set; }

        public GetConnectionsResponse Any(GetConnections request)
        {
            var response = new GetConnectionsResponse
            {
                Result = ConnectionService.GetAllConnections(request.Name)
            };
            return response;
        }
    }
}