using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.ServiceModels;
using ServiceStack;

namespace PresentConnection.Internship7.Iot.Services
{
    public class DeleteConnectionService : Service
    {
        public IConnectionService ConnectionService { get; set; }

        public DeleteConnectionResponse Any(DeleteConnection request)
        {
            var response = new DeleteConnectionResponse
            {
                Deleted = ConnectionService.DeleteConnection(request.Id)
            };
            return response;
        }
    }
}