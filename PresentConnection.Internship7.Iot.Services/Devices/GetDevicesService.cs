using System;
using System.Collections.Generic;
using System.Linq;
using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.ServiceModels;
using ServiceStack;

namespace PresentConnection.Internship7.Iot.Services
{
    public class GetDevicesService : ServiceBase
    {
        public IDeviceService DeviceService { get; set; }
        public IImageService ImageService { get; set; }

        public object Any(GetDevices request)
        {
            var expireInTimespan = new TimeSpan(1, 0, 0);

            var cacheKey = CacheKeys.Devices.List;

            if (!string.IsNullOrEmpty(request.Name))
            {
                cacheKey = CacheKeys.Devices.ListWithProvidedName.Fmt(request.Name);
            }

            Request.ToOptimizedResultUsingCache(

               Cache,
               cacheKey,
               expireInTimespan,

               () =>
               {
                   var response = new GetDevicesResponse();

                   var devices = DeviceService.GetAllDevices(request.Name);
                   if (devices != null && devices.Any())
                   {
                       response.Result = new List<DeviceDto>();
                       foreach (var device in devices)
                       {
                           var item = DeviceDto.With(ImageService)
                               .Map(device)
                               .ApplyImages(device.Images)
                               .Build();
                           response.Result.Add(item);
                       }
                   }
                   
                   return response;
               });

            return Cache.Get<GetDevicesResponse>(cacheKey);
        }
    }
}
