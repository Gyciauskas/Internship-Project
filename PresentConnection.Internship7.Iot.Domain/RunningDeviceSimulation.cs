using System;
using System.Collections.Generic;
using CodeMash.Net;

namespace PresentConnection.Internship7.Iot.Domain
{
    [CollectionName(Statics.Collections.RunningDeviceSimulations)]
    public class RunningDeviceSimulation : EntityBase
    {
        public RunningDeviceSimulation()
        {
            SimulationType = SimulationType.NotSet;
            Configuration = new Dictionary<string, object>();
        }

        public string DeviceId { get; set; }
        public SimulationType SimulationType { get; set; }
        public Dictionary<string, object> Configuration { get; set; }
    }
}
