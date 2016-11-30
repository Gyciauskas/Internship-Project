using System.Collections.Generic;
using PresentConnection.Internship7.Iot.Domain;

namespace PresentConnection.Internship7.Iot.BusinessContracts
{
    public interface IDeviceService
    {
        void CreateDevice(Device device);
        void UpdateDevice(Device device);
        bool DeleteDevice(string id);
        List<Device> GetAllDevices(string name = "");
        Device GetDevice(string id);
    }
}
