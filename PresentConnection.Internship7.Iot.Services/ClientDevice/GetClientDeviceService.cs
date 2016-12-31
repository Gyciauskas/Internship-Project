using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.ServiceModels;
using ServiceStack;

namespace PresentConnection.Internship7.Iot.Services
{
    public class GetClientDeviceService : ServiceBase
    {
        public IClientDeviceService ClientDeviceService { get; set; }

        public GetClientDeviceResponse Any(GetClientDevice request)
        {
            var response = new GetClientDeviceResponse
            {
                Result = ClientDeviceService.GetClientDevice(request.Id, UserSession.UserAuthId)
            };
            return response;
        }
    }
}
