using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.BusinessImplementation;
using PresentConnection.Internship7.Iot.Domain;
using PresentConnection.Internship7.Iot.ServiceModels;
using ServiceStack;

namespace PresentConnection.Internship7.Iot.Services
{
    public class CreateDeviceService : ServiceBase
    {
        public IDeviceService DeviceService { get; set; }

        public CreateDeviceResponse Any(CreateDevice request)
        {
            var response = new CreateDeviceResponse();

            var device = new Device().PopulateWith(request);
            device.UniqueName = SeoService.GetSeName(request.ModelName);

            DeviceService.CreateDevice(device);

            var casheKey = CacheKeys.Devices.List;
            Request.RemoveFromCache(Cache, casheKey);

            response.Result = device;
            return response;
        }
    }
}
