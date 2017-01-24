using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.BusinessImplementation;
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
                Name = request.Name,
                UniqueName = SeoService.GetSeName(request.Name),
                RelatedConnections = request.RelatedConnections,
                Images = request.Images
            };

            ConnectionGroupService.CreateConnectionGroup(connectionGroup);

            var cacheKey = CacheKeys.ConnectionGroup.List;
            Request.RemoveFromCache(Cache, cacheKey);

            response.Result = connectionGroup;

            return response;
        }
    }
}