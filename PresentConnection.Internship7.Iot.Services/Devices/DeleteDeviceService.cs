using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.ServiceModels;
using ServiceStack;

namespace PresentConnection.Internship7.Iot.Services
{
    public class DeleteDeviceService : ServiceBase
    {
        public IDeviceService DeviceService { get; set; }

        public DeleteDeviceResponse Any(DeleteDevice request)
        {
            var device = DeviceService.GetDevice(request.Id);
            var deviceName = string.Empty;

            if (device != null)
            {
                deviceName = device.ModelName;
            }
            var response = new DeleteDeviceResponse
            {
                Result = DeviceService.DeleteDevice(request.Id)
            };

            if (response.Result)
            {
                var cacheKeyForListWithName = CacheKeys.Devices.ListWithProvidedName.Fmt(deviceName);
                var cacheKeyForList = CacheKeys.Devices.List;
                var cacheKeyForItem = CacheKeys.Devices.Item.Fmt(request.Id);

                Request.RemoveFromCache(Cache, cacheKeyForList);
                Request.RemoveFromCache(Cache, cacheKeyForListWithName);
                Request.RemoveFromCache(Cache, cacheKeyForItem);
            }

            return response;
        }
    }
}
