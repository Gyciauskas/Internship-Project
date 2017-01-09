using System.Collections.Generic;
using CodeMash.Net;

namespace PresentConnection.Internship7.Iot.Domain
{
    [CollectionName(Statics.Collections.Devices)]
    public class Device : EntityBase, IEntityWithUniqueName
    {
        public Device()
        {
            Images = new List<string>();
            AvailablePowerResources = new List<PowerResourceType>();
            DefaultRules = new List<string>();
            DefaultCommands = new List<string>();
            AvailableComponents = new List<string>();
            SimilarDevices = new List<string>();
            ArticlesUrls = new List<DisplayUrl>();
        }

        public string ManufacturerId { get; set; }
        /// <summary>
        /// e.g. Raspberry PI 3
        /// </summary>
        public string ModelName { get; set; }

        /// <summary>
        /// e.g. raspberry-pi-3
        /// </summary>
        public string UniqueName { get; set; } 
        public List<string> Images { get; set; }
        public string DefaultFirmwareVersion { get; set; }
        public List<PowerResourceType> AvailablePowerResources { get; set; }
        /// <summary>
        /// in KB
        /// </summary>
        public int InstalledRAMInKB { get; set; } 
        public string Processor { get; set; }
        public bool IsVisible { get; set; }
        /// <summary>
        /// e.g. OnInitFailedSendNotificationToDashboard
        /// </summary>
        public List<string> DefaultRules { get; set; }
        /// <summary>
        /// e.g. Ping, ChangeDeviceState
        /// </summary>
        public List<string> DefaultCommands { get; set; }

        /// <summary>
        /// e.g. component Ids.
        /// </summary>
        public List<string> AvailableComponents { get; set; }

        /// <summary>
        /// from other Vendors
        /// </summary>
        public List<string> SimilarDevices { get; set; }
        /// <summary>
        /// for end-user
        /// </summary>
        public string HowToConnectDescription { get; set; }
        /// <summary>
        /// for admin-developer
        /// </summary>
        public string TechnicalInstructionsHowToConnect { get; set; }  
        public string DownloadImagePath { get; set; }
        public List<DisplayUrl> ArticlesUrls { get; set; }
        public string BuyUrl { get; set; }
    }
}
