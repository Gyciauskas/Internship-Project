using ServiceStack;
using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.Domain;
using PresentConnection.Internship7.Iot.ServiceModels;

namespace PresentConnection.Internship7.Iot.Services
{
    public class CreateConnectionService : ServiceBase
    {
        public IConnectionService ConnectionService { get; set; }

        public CreateConnectionResponse Any(CreateConnection request)
        {
            var response = new CreateConnectionResponse();

            var connection = new Connection().PopulateWith(request);

            ConnectionService.CreateConnection(connection);
            return response;
        }
    }
}
