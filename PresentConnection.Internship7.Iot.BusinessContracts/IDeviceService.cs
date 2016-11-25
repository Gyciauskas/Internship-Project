using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PresentConnection.Internship7.Iot.Domain;

namespace PresentConnection.Internship7.Iot.BusinessContracts
{
    public interface IDeviceService
    {
        string CreateDevice(Device device);
        void UpdateDevice(Device device);
        bool DeleteDevice(string id);
        List<Device> GetAllDevices(string name = "");
        Device GetDevice(string id);
    }
}
