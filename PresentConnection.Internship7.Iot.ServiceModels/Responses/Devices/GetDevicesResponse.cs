using System.Collections.Generic;
using PresentConnection.Internship7.Iot.Domain;

namespace PresentConnection.Internship7.Iot.ServiceModels
{
    public class GetDevicesResponse
    {
        public GetDevicesResponse()
        {
            Devices = new List<Device>();
        }
        public List<Device> Devices { get; set; }
    }
}
