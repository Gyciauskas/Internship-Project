using PresentConnection.Internship7.Iot.Domain;
using System.Collections.Generic;

namespace PresentConnection.Internship7.Iot.BusinessContracts
{
    public interface IClientDeviceService
    {
        void CreateClientDevice(ClientDevice clientDevice);
        void UpdateClientDevice(ClientDevice clientDevice);
        bool DeleteClientDevice(string id);
        List<ClientDevice> GetAllClientDevices(string clientId);
        ClientDevice GetClientDevice(string id);
    }
}
