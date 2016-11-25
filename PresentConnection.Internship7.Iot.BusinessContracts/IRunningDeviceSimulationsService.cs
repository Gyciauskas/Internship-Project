using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PresentConnection.Internship7.Iot.Domain;

namespace PresentConnection.Internship7.Iot.BusinessContracts
{
    public interface IRunningDeviceSimulationsService
    {
        string CreateRunningDeviceSimulations(RunningDeviceSimulations runningDeviceSimulations);
        void UdpdateRunningDeviceSimulations(RunningDeviceSimulations runningDeviceSimulations);
        bool DeleteRunningDeviceSimulations(string id);
        List<RunningDeviceSimulations> GetAllRunningDeviceSimulations(string name = "");
        RunningDeviceSimulations GetRunningDeviceSimulations(string id);
    }
}
