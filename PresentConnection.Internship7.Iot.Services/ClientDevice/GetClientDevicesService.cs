using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.ServiceModels;
using ServiceStack;
using System;

namespace PresentConnection.Internship7.Iot.Services
{
    public class GetClientDevicesService : ServiceBase
    {
        public IClientDeviceService ClientDeviceService { get; set; }

        public object Any(GetClientDevices request)
        {
            var expireInTimespan = new TimeSpan(1, 0, 0);

            var cacheKey = CacheKeys.ClientDevices.List;

            if (!string.IsNullOrEmpty(request.ClientId))
            {
                cacheKey = CacheKeys.ClientDevices.ListWithProvidedName.Fmt(request.ClientId);
            }

            return Request.ToOptimizedResultUsingCache(

                Cache,
                cacheKey,
                expireInTimespan,

                () => {
                    var response = new GetClientDevicesResponse
                    {
                        Result = ClientDeviceService.GetClientDevices(request.ClientId, UserSession.UserAuthId)
                    };

                    return response;
                });
        }
    }
}