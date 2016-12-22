using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.ServiceModels;
using ServiceStack;

namespace PresentConnection.Internship7.Iot.Services
{
    public class GetDevicesService : ServiceBase
    {
        public IDeviceService DeviceService { get; set; }

        public GetDevicesResponse Any(GetDevices request)
        {
            var response = new GetDevicesResponse
            {
                Result = DeviceService.GetAllDevices(request.Name)
            };
            return response;
        }
    }
}
