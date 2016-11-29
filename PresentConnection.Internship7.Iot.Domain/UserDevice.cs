using CodeMash.Net;
using System.Collections.Generic;

namespace PresentConnection.Internship7.Iot.Domain
{
    [CollectionName("UserDevices")]
    public class UserDevice : EntityBase
    {
        public UserDevice()
        {
            Rules = new Dictionary<string, object>();
            Commands = new Dictionary<string, object>();
            Components = new Dictionary<string, object>();
            PowerResource = new PowerResource();
            SimulationType = SimulationType.NotSet;
        }

        public string UserId { get; set; }
        public string DeviceId { get; set; }
        public string DeviceDisplayId { get; set; }
        public bool IsEnabled { get; set; }// when user register device but don't do real action yet
        public bool IsConnected { get; set; }// when user establish connection from device
        public PowerResource PowerResource { get; set; }// see below description
        public string SerialNumber { get; set; }
        public string FirmwareVersion { get; set; }// GetDefaultVersion From Device
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public Dictionary<string, object> Rules { get; set; } // e.g. component unique name, json
        public Dictionary<string, object> Commands { get; set; } // e.g. component unique name, json
        public Dictionary<string, object> Components { get; set; } // e.g. component unique name, json conf
        public string AuthKey1 { get; set; }
        public string AuthKey2 { get; set; }
        public SimulationType SimulationType { get; set; }
        public bool IsSimulationDevice { get; set; }
        public string CreatedBy { get; set; }
    }
}
