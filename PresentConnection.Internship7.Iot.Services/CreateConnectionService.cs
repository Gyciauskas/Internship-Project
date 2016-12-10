using ServiceStack;
using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.Domain;
using PresentConnection.Internship7.Iot.ServiceModels;

namespace PresentConnection.Internship7.Iot.Services
{
    class CreateConnectionService : Service
    {
        public IConnectionService ConnectionService { get; set; }
        public CreateConnectionResponse Any(CreateConnection request)
        {
            var response = new CreateConnectionResponse();

            var connection = new Connection
            {
                Name = request.Name,
                UniqueName = request.UniqueName,
                Images = request.Images,
                Url = request.Url,
                Description = request.Description
            };
            ConnectionService.CreateConnection(connection);
            return response;
        }
    }
}
