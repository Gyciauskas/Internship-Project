using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.Domain;
using PresentConnection.Internship7.Iot.ServiceModels;
using ServiceStack;

namespace PresentConnection.Internship7.Iot.Services
{
    public class UpdateClientDeviceService : ServiceBase
    {
        public IClientDeviceService ClientDeviceService { get; set; }

        public UpdateClientDeviceResponse Any(UpdateClientDevice request)
        {
            var response = new UpdateClientDeviceResponse();

            var clientDevice = ClientDeviceService.GetClientDevice(request.Id, UserSession.UserAuthId);
            clientDevice = clientDevice?.PopulateWith(request);
            var clientDeviceName = string.Empty;

            if (clientDevice != null)
            {
                clientDeviceName = clientDevice.DeviceId;
                clientDevice = clientDevice.PopulateWith(request);
            }

            ClientDeviceService.UpdateClientDevice(clientDevice, request.Id);

            var cacheKeyForListWithName = CacheKeys.ClientDevices.ListWithProvidedName.Fmt(clientDeviceName);
            var cacheKeyForList = CacheKeys.ClientDevices.List;
            var cacheKeyForItem = CacheKeys.ClientDevices.Item.Fmt(request.Id);

            Request.RemoveFromCache(Cache, cacheKeyForList);
            Request.RemoveFromCache(Cache, cacheKeyForListWithName);
            Request.RemoveFromCache(Cache, cacheKeyForItem);

            response.Result = clientDevice;
            return response;
        }
    }
}