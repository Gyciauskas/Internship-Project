using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.ServiceModels;
using ServiceStack;

namespace PresentConnection.Internship7.Iot.Services
{
    public class GetConnectionService : Service
    {
        public IConnectionService ConnectionService { get; set; }

        public GetConnectionResponse Any(GetConnection request)
        {
            var response = new GetConnectionResponse
            {
                ConnectionById = ConnectionService.GetConnection(request.Id)
            };
            return response;
        }
    }
}