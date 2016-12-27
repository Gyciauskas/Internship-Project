using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.ServiceModels;

namespace PresentConnection.Internship7.Iot.Services
{
    public class GetClientDevicesService : ServiceBase
    {
        public IClientDeviceService ClientDeviceService { get; set; }

        public GetClientDevicesResponse Any(GetClientDevices request)
        {
            var response = new GetClientDevicesResponse
            {
                Result = ClientDeviceService.GetClientDevices(request.ClientId, UserSession.UserAuthId)
            };

            return response;
        }
    }
}