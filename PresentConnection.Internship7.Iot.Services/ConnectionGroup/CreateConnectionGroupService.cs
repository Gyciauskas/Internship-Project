using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.Domain;
using PresentConnection.Internship7.Iot.ServiceModels;
using ServiceStack;

namespace PresentConnection.Internship7.Iot.Services
{
    public class CreateConnectionGroupService : ServiceBase
    {
        public IConnectionGroupService ConnectionGroupService { get; set; }

        public CreateConnectionGroupResponse Any(CreateConnectionGroup request)
        {
            var response = new CreateConnectionGroupResponse();

            var connectionGroup = new ConnectionGroup
            {
                UniqueName = request.UniqueName,
                Name = request.Name,
                RelatedConnections = request.RelatedConnections,
                Images = request.Images
            };

            ConnectionGroupService.CreateConnectionGroup(connectionGroup);

            return response;
        }
    }
}