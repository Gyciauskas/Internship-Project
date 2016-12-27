using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.ServiceModels;
using ServiceStack;

namespace PresentConnection.Internship7.Iot.Services
{
    class UpdateConnectionService : ServiceBase
    {
        public IConnectionService ConnectionService { get; set; }

        public UpdateConnectionResponse Any(UpdateConnection request)
        {
            var response = new UpdateConnectionResponse();

            var connection = ConnectionService.GetConnection(request.Id).PopulateWith(request);

            ConnectionService.UpdateConnection(connection);

            return response;
        }
    }
}
