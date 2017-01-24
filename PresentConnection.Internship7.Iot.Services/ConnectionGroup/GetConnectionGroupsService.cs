using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.ServiceModels;
using System;
using ServiceStack;

namespace PresentConnection.Internship7.Iot.Services
{
    public class GetConnectionGroupsService : ServiceBase
    {
        public IConnectionGroupService ConnectionGroupService { get; set; }

        public object Any(GetConnectionGroups request)
        {
            var expireInTimespan = new TimeSpan(1, 0, 0);
            
            var cacheKey = CacheKeys.ConnectionGroup.List;
            
            cacheKey = CacheKeys.ConnectionGroup.ListWithProvidedName.Fmt();
            
            return Request.ToOptimizedResultUsingCache(
            
                Cache,
                cacheKey,
                expireInTimespan,
            
                () => {
                    var response = new GetConnectionGroupsResponse
                    {
                        Result = ConnectionGroupService.GetAllConnectionGroups()
                    };
                
                    return response;
            });
        }
    }
}