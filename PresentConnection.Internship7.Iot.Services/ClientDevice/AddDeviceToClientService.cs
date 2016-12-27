using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.Domain;
using PresentConnection.Internship7.Iot.ServiceModels;

namespace PresentConnection.Internship7.Iot.Services
{
    public class AddDeviceToClientService : ServiceBase
    {
        public IClientDeviceService ClientDeviceService { get; set; }

        public AddDeviceToClientResponse Any(AddDeviceToClient request)
        {
            var response = new AddDeviceToClientResponse();

            var clientDevice = new ClientDevice
            {
                DeviceId = request.DeviceId,
                ClientId = request.ClientId,
                // TODO : add other properties later
            };

            ClientDeviceService.CreateClientDevice(clientDevice, UserSession.UserAuthId);

            response.Result = clientDevice;
            return response;
        }
    }
}