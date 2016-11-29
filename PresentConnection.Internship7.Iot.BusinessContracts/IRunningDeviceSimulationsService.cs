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
        string CreateRunningDeviceSimulations(RunningDeviceSimulation runningDeviceSimulation);
        void UdpdateRunningDeviceSimulations(RunningDeviceSimulation runningDeviceSimulation);
        bool DeleteRunningDeviceSimulations(string id);
        List<RunningDeviceSimulation> GetAllRunningDeviceSimulations(string name = "");
        RunningDeviceSimulation GetRunningDeviceSimulations(string id);
    }
}
