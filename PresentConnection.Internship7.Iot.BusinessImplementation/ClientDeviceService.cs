using System.Collections.Generic;
using System.Linq;
using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.Domain;
using CodeMash.Net;
using MongoDB.Bson;
using MongoDB.Driver;
using PresentConnection.Internship7.Iot.Utils;

namespace PresentConnection.Internship7.Iot.BusinessImplementation
{
    public class ClientDeviceService : IClientDeviceService
    {
        public IRunningDeviceSimulationsService RunningDeviceSimulationsService { get; set; }

        public void CreateClientDevice(ClientDevice clientDevice, string responsibleClientId)
        {
            var validator = new ClientDeviceValidator(responsibleClientId);
            var results = validator.Validate(clientDevice);
            var validationSucceeded = results.IsValid;

            if (validationSucceeded)
            {
                Db.InsertOne(clientDevice);
            }
            else
            {
                throw new BusinessException("Cannot create client device", results.Errors);
            }
        }

        public void UpdateClientDevice(ClientDevice clientDevice, string responsibleClientId)
        {
            try
            {
                GetClientDevice(clientDevice.Id.ToString(), responsibleClientId);
            }
            catch (BusinessException e)
            {
                throw new BusinessException("You don't have permissions to update this client device", e);
            }

            var validator = new ClientDeviceValidator(responsibleClientId);
            var results = validator.Validate(clientDevice);
            var validationSucceeded = results.IsValid;

            if (validationSucceeded)
            {
                Db.FindOneAndReplace(x => x.Id == clientDevice.Id, clientDevice);
            }
            else
            {
                throw new BusinessException("Cannot update client device", results.Errors);
            }
        }

        public bool DeleteClientDevice(string id, string responsibleClientId)
        {
            try
            {
                GetClientDevice(id, responsibleClientId);
            }
            catch (BusinessException e)
            {
                throw new BusinessException("You don't have permissions to delete this client device", e);
            }

            var deleteResult = Db.DeleteOne<ClientDevice>(x => x.Id == ObjectId.Parse(id));
            return deleteResult.DeletedCount == 1;
        }

        public List<ClientDevice> GetClientDevices(string clientId, string responsibleClientId)
        {
            var filterBuilder = Builders<ClientDevice>.Filter;
            var filter = filterBuilder.Empty;

            if (!string.IsNullOrEmpty(clientId))
            {
                var findByNameFilter = Builders<ClientDevice>.Filter.Eq(x => x.ClientId, clientId);
                filter = filter & findByNameFilter;
            }

            var clientDevices = Db.Find(filter);

            var validator = new RecordsPermissionValidator(responsibleClientId);
            var results = validator.Validate(clientDevices.Cast<IEntityWithSensitiveData>().ToList());
            var validationSucceeded = results.IsValid;

            if (validationSucceeded)
            {
                return clientDevices;
            }
            throw new BusinessException("You don't have permissions to get client devices", results.Errors);
        }

        public ClientDevice GetClientDevice(string id, string responsibleClientId)
        {
            var clientDevice = Db.FindOneById<ClientDevice>(id);

            if (clientDevice == null)
            {
                return null;
            }

            var validator = new RecordPermissionValidator(responsibleClientId);
            var results = validator.Validate(clientDevice);
            var validationSucceeded = results.IsValid;

            if (validationSucceeded)
            {
                return clientDevice;
            }
            throw new BusinessException("You don't have permissions to get client device", results.Errors);
        }

        public void DeviceStarted(string id, string responsibleClientId)
        {
            RunningDeviceSimulationsService = new RunningDeviceSimulationsService();
            var clientDevice = GetClientDevice(id, responsibleClientId);
            if (clientDevice == null) return;
            clientDevice.AddDeviceStatus(DeviceStatus.Connected);
            if (clientDevice.IsSimulationDevice)
            {
                var runningDeviceSimulation = new RunningDeviceSimulation
                {
                    DeviceId = clientDevice.DeviceId,
                    SimulationType = clientDevice.SimulationType
                };
                RunningDeviceSimulationsService.CreateRunningDeviceSimulations(runningDeviceSimulation);
            }
            else
            {
                // should rise notification?
            }
        }

        public void DeviceStopped(string id, string responsibleClientId)
        {
            RunningDeviceSimulationsService = new RunningDeviceSimulationsService();
            var clientDevice = GetClientDevice(id, responsibleClientId);
            if (clientDevice == null) return;
            clientDevice.AddDeviceStatus(DeviceStatus.Disconnected);
            if (clientDevice.IsSimulationDevice)
            {
                var simulations =
                    RunningDeviceSimulationsService.GetAllRunningDeviceSimulations(clientDevice.DeviceId);
                if (simulations == null) return;
                foreach (var simulation in simulations)
                {
                    RunningDeviceSimulationsService.DeleteRunningDeviceSimulations(simulation.Id.ToString());
                }
            }
            else
            {
                // should rise notification?
            }
        }
    }
}
