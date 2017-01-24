using System;
using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.ServiceModels;
using ServiceStack;

namespace PresentConnection.Internship7.Iot.Services
{
    public class GetConnectionGroupService : ServiceBase
    {
        public IConnectionGroupService ConnectionGroupService { get; set; }

        public object Any(GetConnectionGroup request)
        {
            var expireInTimespan = new TimeSpan(1, 0, 0);

            return Request.ToOptimizedResultUsingCache(

                Cache,
                CacheKeys.ConnectionGroup.Item.Fmt(request.Id),
                expireInTimespan,

                () => {
                    var response = new GetConnectionGroupResponse
                    {
                        Result = ConnectionGroupService.GetConnectionGroup(request.Id)
                    };
                    return response;
                });
        }
    }
}