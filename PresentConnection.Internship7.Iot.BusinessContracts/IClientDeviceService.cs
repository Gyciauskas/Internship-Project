using PresentConnection.Internship7.Iot.Domain;
using System.Collections.Generic;

namespace PresentConnection.Internship7.Iot.BusinessContracts
{
    public interface IClientDeviceService
    {
        void CreateClientDevice(ClientDevice clientDevice, string responsibleClientId);
        void UpdateClientDevice(ClientDevice clientDevice, string responsibleClientId);
        bool DeleteClientDevice(string id, string responsibleClientId);
        List<ClientDevice> GetClientDevices(string clientId, string responsibleClientId);
        ClientDevice GetClientDevice(string id, string responsibleClientId);
        void DeviceStarted(string id, string responsibleClientId);
        void DeviceStopped(string id, string responsibleClientId);
    }
}
