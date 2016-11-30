using System.Collections.Generic;
using CodeMash.Net;
using MongoDB.Bson;
using MongoDB.Driver;
using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.Domain;
using PresentConnection.Internship7.Iot.Utils;

namespace PresentConnection.Internship7.Iot.BusinessImplementation
{
    public class RunningDeviceSimulationsService : IRunningDeviceSimulationsService
    {
        public void CreateRunningDeviceSimulations(RunningDeviceSimulation runningDeviceSimulation)
        {
            var validator = new RunningDeviceSimulationsValidator();
            var results = validator.Validate(runningDeviceSimulation);
            var validationSucceeded = results.IsValid;

            if (validationSucceeded)
            {
                Db.InsertOne(runningDeviceSimulation);
            }
            else
            {
                throw new BusinessException("Cannot create runningDeviceSimulation", results.Errors);
            }
        }

        public void UdpdateRunningDeviceSimulations(RunningDeviceSimulation runningDeviceSimulation)
        {
            var validator = new RunningDeviceSimulationsValidator();
            var results = validator.Validate(runningDeviceSimulation);
            var validationSucceeded = results.IsValid;

            if (validationSucceeded)
            {
                Db.FindOneAndReplace(x => x.Id == runningDeviceSimulation.Id, runningDeviceSimulation);
            }
            else
            {
                throw new BusinessException("Cannot update runningDeviceSimulation", results.Errors);
            }
        }

        public bool DeleteRunningDeviceSimulations(string id)
        {
            var deleteResult = Db.DeleteOne<RunningDeviceSimulation>(x => x.Id == ObjectId.Parse(id));
            return deleteResult.DeletedCount == 1;
        }

        public List<RunningDeviceSimulation> GetAllRunningDeviceSimulations(string name = "")
        {
            var filterBuilder = Builders<RunningDeviceSimulation>.Filter;
            var filter = filterBuilder.Empty;

            if (!string.IsNullOrEmpty(name))
            {
                var findByNameFilter = Builders<RunningDeviceSimulation>.Filter.Eq(x => x.DeviceId, name);
                filter = filter & findByNameFilter;
            }

            var runningDeviceSimulations = Db.Find(filter);
            return runningDeviceSimulations;
        }

        public RunningDeviceSimulation GetRunningDeviceSimulations(string id)
        {
            return Db.FindOneById<RunningDeviceSimulation>(id);
        }
    }
}
