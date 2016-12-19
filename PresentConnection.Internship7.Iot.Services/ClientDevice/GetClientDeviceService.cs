using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.ServiceModels;
using ServiceStack;

namespace PresentConnection.Internship7.Iot.Services
{
    public class GetClientDeviceService : Service
    {
        public IClientDeviceService ClientDeviceService { get; set; }

        public GetClientDeviceResponse Any(GetClientDevice request)
        {
            var response = new GetClientDeviceResponse
            {
                ClientDevice = ClientDeviceService.GetClientDevice(request.Id, request.ClientId)
            };
            return response;
        }
    }
}
