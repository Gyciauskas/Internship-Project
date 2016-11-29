using CodeMash.Net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentConnection.Internship7.Iot.Domain
{
    [CollectionName("UserDevices")]
    public class UserDevices : EntityBase
    {
        public string UserId { get; set; }
        public string DeviceId { get; set; }
        public string DeviceDisplayId { get; set; }
        public bool IsEnabled { get; set; }// when user register device but don't do real action yet
        public bool isConnected { get; set; }// when user establish connection from device
        public PowerResource PowerResource { get; set; }// see below description
        public string SerialNumber { get; set; }
        public string FirmwareVersion { get; set; }// GetDefaultVersion From Device
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public Dictionary<string, string> Rules { get; set; } // e.g. component unique name, json
        public Dictionary<string, string> Commands { get; set; } // e.g. component unique name, json
        public Dictionary<string, string> Components { get; set; } // e.g. component unique name, json conf
        public string AuthKey1 { get; set; }
        public string AuthKey2 { get; set; }
        enum SimulationType { NotSet, Telemetry, GPS, ManufacturyMachine }
        bool IsSimulationDevice { get; set; }
        string CreatedBy { get; set; }

    }
}
