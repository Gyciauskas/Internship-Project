using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.ServiceModels;
using ServiceStack;

namespace PresentConnection.Internship7.Iot.Services
{
    public class UpdateConnectionGroupervice : ServiceBase
    {
        public IConnectionGroupService ConnectionGroupService { get; set; }

        public UpdateConnectionGroupResponse Any(UpdateConnectionGroup request)
        {
            var response = new UpdateConnectionGroupResponse();

            var connectionGroup = ConnectionGroupService.GetConnectionGroup(request.Id);
            var connectionGroupName = string.Empty;

            if (connectionGroup != null)
            {
                connectionGroupName = connectionGroup.Name;
                connectionGroup = connectionGroup.PopulateWith(request);
            }

            ConnectionGroupService.UpdateConnectionGroup(connectionGroup);

            var cacheKeyForListWithName = CacheKeys.ConnectionGroup.ListWithProvidedName.Fmt(connectionGroupName);
            var cacheKeyForList = CacheKeys.ConnectionGroup.List;
            var cacheKeyForItem = CacheKeys.ConnectionGroup.Item.Fmt(request.Id);

            Request.RemoveFromCache(Cache, cacheKeyForList);
            Request.RemoveFromCache(Cache, cacheKeyForListWithName);
            Request.RemoveFromCache(Cache, cacheKeyForItem);

            response.Result = connectionGroup;
            return response;
        }
    }
}