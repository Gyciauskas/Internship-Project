using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.BusinessImplementation;
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

            var connection = ConnectionService.GetConnection(request.Id);

            if (connection != null)
            {
                connection = connection.PopulateWith(request);
                connection.UniqueName = SeoService.GetSeName(request.UniqueName);
            }

            ConnectionService.UpdateConnection(connection);

            return response;
        }
    }
}
