using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.Domain;
using PresentConnection.Internship7.Iot.ServiceModels;
using ServiceStack;

namespace PresentConnection.Internship7.Iot.Services
{
    public class CreateClientDeviceService : Service
    {
        public IClientDeviceService ClientDeviceService { get; set; }

        public CreateClientDeviceResponse Any(CreateClientDevice request)
        {
            var response = new CreateClientDeviceResponse();

            var clientDevice = new ClientDevice
            {
                DeviceId = request.DeviceId,
                ClientId = request.ClientId
            };
            string clientId = request.ClientId;

            ClientDeviceService.CreateClientDevice(clientDevice, clientId);

            return response;
        }
    }
}