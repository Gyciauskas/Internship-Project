using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.ServiceModels;
using ServiceStack;

namespace PresentConnection.Internship7.Iot.Services
{
    public class DeleteDeviceService : Service
    {
        public IDeviceService DeviceService { get; set; }

        public DeleteDeviceResponse Any(DeleteDevice request)
        {
            var response = new DeleteDeviceResponse
            {
                IsDeleted = DeviceService.DeleteDevice(request.Id)
            };

            return response;
        }
    }
}
