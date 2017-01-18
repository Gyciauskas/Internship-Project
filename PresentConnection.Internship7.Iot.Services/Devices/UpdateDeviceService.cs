using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.BusinessImplementation;
using PresentConnection.Internship7.Iot.Domain;
using PresentConnection.Internship7.Iot.ServiceModels;
using ServiceStack;

namespace PresentConnection.Internship7.Iot.Services
{
    public class UpdateDeviceService : ServiceBase
    {
        public IDeviceService DeviceService { get; set; }

        public UpdateDeviceResponse Any(UpdateDevice request)
        {
            var response = new UpdateDeviceResponse();

            var device = DeviceService.GetDevice(request.Id);
            var deviceName = string.Empty;

            if (device != null)
            {
                deviceName = device.ModelName;
                device = device.PopulateWith(request);
                device.UniqueName = SeoService.GetSeName(request.UniqueName);
            }

            DeviceService.UpdateDevice(device);

            var cacheKeyForListWithName = CacheKeys.Devices.ListWithProvidedName.Fmt(deviceName);
            var cacheKeyForList = CacheKeys.Devices.List;
            var cacheKeyForItem = CacheKeys.Devices.Item.Fmt(request.Id);

            Request.RemoveFromCache(Cache, cacheKeyForList);
            Request.RemoveFromCache(Cache, cacheKeyForListWithName);
            Request.RemoveFromCache(Cache, cacheKeyForItem);

            response.Result = device;
            return response;
        }
    }
}
