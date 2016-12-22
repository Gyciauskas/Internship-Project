using PresentConnection.Internship7.Iot.BusinessContracts;
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

            var device = DeviceService.GetDevice(request.Id).PopulateWith(request);

            DeviceService.UpdateDevice(device);

            return response;
        }
    }
}
