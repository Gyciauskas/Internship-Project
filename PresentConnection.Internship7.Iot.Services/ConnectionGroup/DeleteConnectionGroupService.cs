using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.ServiceModels;
using ServiceStack;

namespace PresentConnection.Internship7.Iot.Services
{
    public class DeleteConnectionGroupService : ServiceBase
    {
        public IConnectionGroupService ConnectionGroupService { get; set; }

        public DeleteConnectionGroupResponse Any(DeleteConnectionGroup request)
        {
            var connectionGroup = ConnectionGroupService.GetConnectionGroup(request.Id);
            var connectionGroupName = string.Empty;

            if (connectionGroup != null)
            {
                connectionGroupName = connectionGroup.Name;
            }

            var response = new DeleteConnectionGroupResponse
            {
                Result = ConnectionGroupService.DeleteConnectionGroup(request.Id)
            };


            if (response.Result)
            {
                var cacheKeyForListWithName = CacheKeys.ConnectionGroup.ListWithProvidedName.Fmt(connectionGroupName);
                var cacheKeyForList = CacheKeys.ConnectionGroup.List;
                var cacheKeyForItem = CacheKeys.ConnectionGroup.Item.Fmt(request.Id);

                Request.RemoveFromCache(Cache, cacheKeyForList);
                Request.RemoveFromCache(Cache, cacheKeyForListWithName);
                Request.RemoveFromCache(Cache, cacheKeyForItem);
            }

            return response;
        }
    }
}