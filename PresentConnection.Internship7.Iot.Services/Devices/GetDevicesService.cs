using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.ServiceModels;
using ServiceStack;

namespace PresentConnection.Internship7.Iot.Services
{
    public class GetDevicesService : Service
    {
        public IDeviceService DeviceService { get; set; }

        public GetDevicesResponse Any(GetDevices request)
        {
            var response = new GetDevicesResponse
            {
                Devices = DeviceService.GetAllDevices(request.Name)
            };
            return response;
        }
    }
}
