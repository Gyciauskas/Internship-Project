using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.ServiceModels;
using ServiceStack;
using System;

namespace PresentConnection.Internship7.Iot.Services
{
    public class GetClientDeviceService : ServiceBase
    {
        public IClientDeviceService ClientDeviceService { get; set; }

        public object Any(GetClientDevice request)
        {
            var expireInTimespan = new TimeSpan(1, 0, 0);

            return Request.ToOptimizedResultUsingCache(

                Cache,
                CacheKeys.ClientDevices.Item.Fmt(request.Id),
                expireInTimespan,

                () => {
                    var response = new GetClientDeviceResponse
                    {
                        Result = ClientDeviceService.GetClientDevice(request.Id, UserSession.UserAuthId)
                    };
                    return response;
                });
        }
    }
}