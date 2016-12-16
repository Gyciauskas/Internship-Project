using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.ServiceModels;
using ServiceStack;

namespace PresentConnection.Internship7.Iot.Services
{
    public class GetDeviceService : Service
    {
        public IDeviceService DeviceService { get; set; }

        public GetDeviceResponse Any(GetDevice request)
        {
            var response = new GetDeviceResponse
            {
                Device = DeviceService.GetDevice(request.Id)
            };
            return response;
        }
    }
}
