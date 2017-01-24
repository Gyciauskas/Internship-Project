using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.ServiceModels;
using ServiceStack;

namespace PresentConnection.Internship7.Iot.Services
{
    public class DeleteClientDeviceService : ServiceBase
    {
        public IClientDeviceService ClientDeviceService { get; set; }

        public DeleteClientDeviceResponse Any(DeleteClientDevice request)
        {
            var clientDevice = ClientDeviceService.GetClientDevice(request.Id, request.Id);
            var clientDeviceName = string.Empty;

            if (clientDevice != null)
            {
                clientDeviceName = clientDevice.DeviceId;
            }

            var response = new DeleteClientDeviceResponse
            {
                Result = ClientDeviceService.DeleteClientDevice(request.Id, UserSession.UserAuthId)
            };


            if (response.Result)
            {
                var cacheKeyForListWithName = CacheKeys.ClientDevices.ListWithProvidedName.Fmt(clientDeviceName);
                var cacheKeyForList = CacheKeys.ClientDevices.List;
                var cacheKeyForItem = CacheKeys.ClientDevices.Item.Fmt(request.Id);

                Request.RemoveFromCache(Cache, cacheKeyForList);
                Request.RemoveFromCache(Cache, cacheKeyForListWithName);
                Request.RemoveFromCache(Cache, cacheKeyForItem);
            }

            return response;
        }
    }
}