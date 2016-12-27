using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.ServiceModels;
using ServiceStack;

namespace PresentConnection.Internship7.Iot.Services
{
    public class DeleteClientDeviceService : ServiceBase
    {
        public IClientDeviceService ClientDeviceService { get; set; }

        public DeleteClientDeviceResponse Any(DeleteClientDevice request)
        {
            var response = new DeleteClientDeviceResponse
            {
                Result = ClientDeviceService.DeleteClientDevice(request.Id, UserSession.UserAuthId)
            };
            
            return response;
        }
    }
}
