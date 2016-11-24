using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeMash.Net;

namespace PresentConnection.Internship7.Iot.Domain
{
    public enum PowerResourceType { Battery, Voltage }
    [CollectionName("Devices")]
    public class Device : EntityBase
    {
        public Device()
        {
            Images = new List<DisplayImage>();
            AvailablePowerResources = new List<PowerResourceType>();
            DefaultRules = new List<string>();
            DefaultCommands = new List<string>();
            AvailableComponents = new List<string>();
            SimilarDevices = new List<string>();
            ArticlesUrls = new List<DisplayUrl>();
        }

        public string ManufacturerId { get; set; }
        public string ModelName { get; set; } // e.g. Raspberry PI 3
        public string UniqueName { get; set; } // e.g. raspberry-pi-3
        public List<DisplayImage> Images { get; set; }
        public string DefaultFirmwareVersion { get; set; }
        public List<PowerResourceType> AvailablePowerResources { get; set; }
        public int InstalledRAMInKB { get; set; } // in KB
        public string Processor { get; set; }
        public bool IsVisible { get; set; }
        public List<string> DefaultRules { get; set; } // e.g. OnInitFailedSendNotificationToDashboard
        public List<string> DefaultCommands { get; set; } // e.g. Ping, ChangeDeviceState
        public List<string> AvailableComponents { get; set; } // e.g. component Ids.
        public List<string> SimilarDevices { get; set; } // from other Vendors
        public string HowToConnectDescription { get; set; } // for end-user
        public string TechnicalInstructionsHowToConnect { get; set; } // for admin-developer
        public string DownloadImagePath { get; set; }
        public List<DisplayUrl> ArticlesUrls { get; set; }
        public string BuyUrl { get; set; }
    }
}
