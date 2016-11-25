using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeMash.Net;

namespace PresentConnection.Internship7.Iot.Domain
{
    public enum SimulationType
    {
        NotSet,
        Telemetry,
        GPS,
        ManufacturyMashine
    }

    [CollectionName("RunningDeviceSimulations")]
    public class RunningDeviceSimulations : EntityBase
    {
        public string DeviceId { get; set; }
        public SimulationType SimulationType { get; set; }
        public Dictionary<string, object> Configuration { get; set; }
    }
}
