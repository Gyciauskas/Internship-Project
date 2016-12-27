using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.Domain;
using PresentConnection.Internship7.Iot.ServiceModels;
using ServiceStack;

namespace PresentConnection.Internship7.Iot.Services
{
    public class UpdateClientDeviceService : ServiceBase
    {
        public IClientDeviceService ClientDeviceService { get; set; }

        public UpdateClientDeviceResponse Any(UpdateClientDevice request)
        {
            var response = new UpdateClientDeviceResponse();

            var clientDevice = ClientDeviceService.GetClientDevice(request.Id, UserSession.UserAuthId);

            clientDevice = clientDevice?.PopulateWith(request);

            ClientDeviceService.UpdateClientDevice(clientDevice, request.Id);

            response.Result = clientDevice;
            return response;
        }
    }
}
