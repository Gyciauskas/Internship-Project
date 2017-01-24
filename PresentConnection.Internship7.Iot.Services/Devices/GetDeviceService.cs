using System;
using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.ServiceModels;
using ServiceStack;

namespace PresentConnection.Internship7.Iot.Services
{
    public class GetDeviceService : ServiceBase
    {
        public IDeviceService DeviceService { get; set; }

        public object Any(GetDevice request)
        {
            var expireInTimespan = new TimeSpan(1, 0, 0);

            return Request.ToOptimizedResultUsingCache(

                Cache, 
                CacheKeys.Devices.Item.Fmt(request.Id),
                expireInTimespan,
               
                () => {
                    var response = new GetDeviceResponse
                    {
                        Result = DeviceService.GetDevice(request.Id)
                    };
                    return response;
                });
           

            
        }
    }
}
