using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.ServiceModels;
using ServiceStack;

namespace PresentConnection.Internship7.Iot.Services
{
    public class GetClientDevicesService : Service
    {
        public IClientDeviceService ClientDeviceService { get; set; }

        public GetClientDevicesResponse Any(GetClientDevices request)
        {
            var response = new GetClientDevicesResponse
            {
                ClientDevices = ClientDeviceService.GetClientDevices(request.ClientId, request.ClientId)
            };

            return response;
        }
    }
}