using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using NUnit.Framework;
using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.BusinessImplementation;
using PresentConnection.Internship7.Iot.Domain;
using PresentConnection.Internship7.Iot.Utils;

namespace PresentConnection.Internship7.Iot.Tests
{
    [TestFixture]
    public class RunningDeviceSimulationsTests
    {
        private IRunningDeviceSimulationsService runningDeviceSimulationsService;

        [SetUp]
        public void SetUp()
        {
            runningDeviceSimulationsService = new RunningDeviceSimulationsService();
        }

        [Test]
        [Category("IntegrationTests")]
        [Category("RunningDeviceSimulations")]
        public void Can_insert_runningDeviceSimulations_to_database()
        {
            var runningDeviceSimulations = new RunningDeviceSimulation()
            {
                DeviceId = "123",
                SimulationType = SimulationType.GPS
            };
            runningDeviceSimulationsService.CreateRunningDeviceSimulations(runningDeviceSimulations);

            runningDeviceSimulations.ShouldNotBeNull();
            runningDeviceSimulations.Id.ShouldNotBeNull();
        }


        [Test]
        [Category("IntegrationTests")]
        [Category("RunningDeviceSimulations")]
        public void Cannot_insert_runningDeviceSimulations_to_database_when_deviceid_is_not_provided()
        {
            var runningDeviceSimulations = new RunningDeviceSimulation()
            {
                DeviceId = "",
                SimulationType = SimulationType.GPS
            };

            typeof(BusinessException).ShouldBeThrownBy(() => runningDeviceSimulationsService.CreateRunningDeviceSimulations(runningDeviceSimulations));
        }

        [Test]
        [Category("IntegrationTests")]
        [Category("RunningDeviceSimulations")]
        public void Cannot_insert_runningDeviceSimulations_to_database_when_simulationtype_is_not_provided()
        {
            var runningDeviceSimulations = new RunningDeviceSimulation()
            {
                DeviceId = "123"                
            };

            typeof(BusinessException).ShouldBeThrownBy(() => runningDeviceSimulationsService.CreateRunningDeviceSimulations(runningDeviceSimulations));
        }


        [Test]
        [Category("IntegrationTests")]
        [Category("RunningDeviceSimulations")]
        public void Can_get_runningDeviceSimulations_by_id()
        {
            var runningDeviceSimulations = new RunningDeviceSimulation()
            {
                DeviceId = "123",
                SimulationType = SimulationType.GPS
            };
            runningDeviceSimulationsService.CreateRunningDeviceSimulations(runningDeviceSimulations);

            runningDeviceSimulations.ShouldNotBeNull();
            runningDeviceSimulations.Id.ShouldNotBeNull();

            var runningDeviceSimulationsFromDb = runningDeviceSimulationsService.GetRunningDeviceSimulations(runningDeviceSimulations.Id.ToString());
            runningDeviceSimulationsFromDb.ShouldNotBeNull();
            runningDeviceSimulationsFromDb.Id.ShouldNotBeNull();
            runningDeviceSimulationsFromDb.DeviceId.ShouldEqual("123");

        }


        [Test]
        [Category("IntegrationTests")]
        [Category("RunningDeviceSimulations")]
        public void Can_get_all_runningDeviceSimulations()
        {
            var runningDeviceSimulations1 = new RunningDeviceSimulation()
            {
                DeviceId = "123",
                SimulationType = SimulationType.GPS
            };

            var runningDeviceSimulations2 = new RunningDeviceSimulation()
            {
                DeviceId = "456",
                SimulationType = SimulationType.ManufacturyMashine
            };

            runningDeviceSimulationsService.CreateRunningDeviceSimulations(runningDeviceSimulations1);
            runningDeviceSimulations1.ShouldNotBeNull();
            runningDeviceSimulations1.Id.ShouldNotBeNull();


            runningDeviceSimulationsService.CreateRunningDeviceSimulations(runningDeviceSimulations2);
            runningDeviceSimulations2.ShouldNotBeNull();
            runningDeviceSimulations2.Id.ShouldNotBeNull();



            var allRunningDeviceSimulations = runningDeviceSimulationsService.GetAllRunningDeviceSimulations();

            allRunningDeviceSimulations.ShouldBe<List<RunningDeviceSimulation>>();
            (allRunningDeviceSimulations.Count > 0).ShouldBeTrue();
        }


        [Test]
        [Category("IntegrationTests")]
        [Category("RunningDeviceSimulations")]
        public void Can_get_all_runningDeviceSimulations_by_name()
        {
            var runningDeviceSimulations1 = new RunningDeviceSimulation()
            {
                DeviceId = "123",
                SimulationType = SimulationType.GPS
            };

            var runningDeviceSimulations2 = new RunningDeviceSimulation()
            {
                DeviceId = "456",
                SimulationType = SimulationType.ManufacturyMashine
            };

            var runningDeviceSimulations3 = new RunningDeviceSimulation()
            {
                DeviceId = "789",
                SimulationType = SimulationType.Telemetry
            };

            runningDeviceSimulationsService.CreateRunningDeviceSimulations(runningDeviceSimulations1);
            runningDeviceSimulations1.ShouldNotBeNull();
            runningDeviceSimulations1.Id.ShouldNotBeNull();


            runningDeviceSimulationsService.CreateRunningDeviceSimulations(runningDeviceSimulations2);
            runningDeviceSimulations2.ShouldNotBeNull();
            runningDeviceSimulations2.Id.ShouldNotBeNull();

            runningDeviceSimulationsService.CreateRunningDeviceSimulations(runningDeviceSimulations3);
            runningDeviceSimulations3.ShouldNotBeNull();
            runningDeviceSimulations3.Id.ShouldNotBeNull();

            var allRunningDeviceSimulations = runningDeviceSimulationsService.GetAllRunningDeviceSimulations("123");

            allRunningDeviceSimulations.ShouldBe<List<RunningDeviceSimulation>>();
            allRunningDeviceSimulations.Count.ShouldEqual(1);
        }


        [Test]
        [Category("IntegrationTests")]
        [Category("RunningDeviceSimulations")]
        public void Can_update_runningDeviceSimulations_to_database()
        {
            var runningDeviceSimulations = new RunningDeviceSimulation()
            {
                DeviceId = "123",
                SimulationType = SimulationType.GPS
            };

            runningDeviceSimulationsService.CreateRunningDeviceSimulations(runningDeviceSimulations);

            runningDeviceSimulations.ShouldNotBeNull();
            runningDeviceSimulations.Id.ShouldNotBeNull();

            runningDeviceSimulations.DeviceId = "edited";
            runningDeviceSimulationsService.UdpdateRunningDeviceSimulations(runningDeviceSimulations);

            var runningDeviceSimulationsFromDb = runningDeviceSimulationsService.GetRunningDeviceSimulations(runningDeviceSimulations.Id.ToString());
            runningDeviceSimulationsFromDb.ShouldNotBeNull();
            runningDeviceSimulationsFromDb.DeviceId.ShouldEqual("edited");
        }


        [Test]
        [Category("IntegrationTests")]
        [Category("RunningDeviceSimulations")]
        public void Can_delete_runningDeviceSimulations_from_database()
        {
            var runningDeviceSimulations = new RunningDeviceSimulation()
            {
                DeviceId = "123",
                SimulationType = SimulationType.GPS
            };

            runningDeviceSimulationsService.CreateRunningDeviceSimulations(runningDeviceSimulations);

            runningDeviceSimulations.ShouldNotBeNull();
            runningDeviceSimulations.Id.ShouldNotBeNull();

            runningDeviceSimulationsService.DeleteRunningDeviceSimulations(runningDeviceSimulations.Id.ToString());

            var runningDeviceSimulationsFromDb = runningDeviceSimulationsService.GetRunningDeviceSimulations(runningDeviceSimulations.Id.ToString());

            runningDeviceSimulationsFromDb.ShouldNotBeNull();
            runningDeviceSimulationsFromDb.Id.ShouldEqual(ObjectId.Empty);
        }


        [TearDown]
        public void Dispose()
        {
            var allRunningDeviceSimulations = runningDeviceSimulationsService.GetAllRunningDeviceSimulations();
            foreach (var runningDeviceSimulations in allRunningDeviceSimulations)
            {
                runningDeviceSimulationsService.DeleteRunningDeviceSimulations(runningDeviceSimulations.Id.ToString());
            }
        }
    }
}
