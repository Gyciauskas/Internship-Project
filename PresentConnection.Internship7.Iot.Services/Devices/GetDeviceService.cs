using System;
using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.ServiceModels;
using ServiceStack;

namespace PresentConnection.Internship7.Iot.Services
{
    public class GetDeviceService : ServiceBase
    {
        public IDeviceService DeviceService { get; set; }
        public IImageService ImageService { get; set; }

        public object Any(GetDevice request)
        {
            var expireInTimespan = new TimeSpan(1, 0, 0);

            Request.ToOptimizedResultUsingCache(

                Cache, 
                CacheKeys.Devices.Item.Fmt(request.Id),
                expireInTimespan,
               
                () =>
                {
                    var device = DeviceService.GetDevice(request.Id);

                    var response = new GetDeviceResponse();

                    if (device != null)
                    {
                        response.Result = DeviceDto.With(ImageService)
                                                .Map(device)
                                                .ApplyImages(device.Images)
                                                .Build();
                    }
                    
                    return response;
                });

            return Cache.Get<GetDeviceResponse>(CacheKeys.Devices.Item.Fmt(request.Id));

        }
    }
}
