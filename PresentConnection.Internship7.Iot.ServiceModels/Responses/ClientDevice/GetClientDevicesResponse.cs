using PresentConnection.Internship7.Iot.Domain;
using System.Collections.Generic;

namespace PresentConnection.Internship7.Iot.ServiceModels
{
    public class GetClientDevicesResponse
    {
        public GetClientDevicesResponse()
        {
            ClientDevices = new List<ClientDevice>();
        }
        public List<ClientDevice> ClientDevices { get; set; }      
    }
}