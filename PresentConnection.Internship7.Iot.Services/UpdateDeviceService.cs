using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.Domain;
using PresentConnection.Internship7.Iot.ServiceModels;
using ServiceStack;

namespace PresentConnection.Internship7.Iot.Services
{
    public class UpdateDeviceService : Service
    {
        public IDeviceService DeviceService { get; set; }

        public UpdateDeviceResponse Any(UpdateDevice request)
        {
            var response = new UpdateDeviceResponse();

            var device = new Device
            {
                ModelName = request.ModelName,
                UniqueName = request.UniqueName
            }.PopulateWithNonDefaultValues(request);

            // Temporary because it's ignored then object is serialized but is used in validation
            // so I have to add it if I want to test 
            device.Images.Add("5821dcc11e9f341d4c6d0994");

            DeviceService.UpdateDevice(device);

            return response;
        }
    }
}
