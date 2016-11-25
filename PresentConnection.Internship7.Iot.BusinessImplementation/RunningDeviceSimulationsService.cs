using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeMash.Net;
using FluentValidation.Results;
using MongoDB.Bson;
using MongoDB.Driver;
using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.Domain;
using PresentConnection.Internship7.Iot.Utils;

namespace PresentConnection.Internship7.Iot.BusinessImplementation
{
    public class RunningDeviceSimulationsService : IRunningDeviceSimulationsService
    {
        public string CreateRunningDeviceSimulations(RunningDeviceSimulations runningDeviceSimulations)
        {
            RunningDeviceSimulationsValidator validator = new RunningDeviceSimulationsValidator();
            ValidationResult results = validator.Validate(runningDeviceSimulations);

            bool validationSucceeded = results.IsValid;

            if (validationSucceeded)
            {
                Db.InsertOne(runningDeviceSimulations);
                return runningDeviceSimulations.Id.ToString();
            }
            else
            {
                throw new BusinessException("Cannot create runningDeviceSimulations", results.Errors);
            }
        }

        public void UdpdateRunningDeviceSimulations(RunningDeviceSimulations runningDeviceSimulations)
        {
            RunningDeviceSimulationsValidator validator = new RunningDeviceSimulationsValidator();
            ValidationResult results = validator.Validate(runningDeviceSimulations);

            bool validationSucceeded = results.IsValid;

            if (validationSucceeded)
            {
                Db.FindOneAndReplace(x => x.Id == runningDeviceSimulations.Id, runningDeviceSimulations);
            }
            else
            {
                throw new BusinessException("Cannot update runningDeviceSimulations", results.Errors);
            }
        }

        public bool DeleteRunningDeviceSimulations(string id)
        {
            var deleteResult = Db.DeleteOne<RunningDeviceSimulations>(x => x.Id == ObjectId.Parse(id));
            return deleteResult.DeletedCount == 1;
        }

        public List<RunningDeviceSimulations> GetAllRunningDeviceSimulations(string name = "")
        {
            var filterBuilder = Builders<RunningDeviceSimulations>.Filter;
            var filter = filterBuilder.Empty;

            if (!string.IsNullOrEmpty(name))
            {
                var findByNameFilter = Builders<RunningDeviceSimulations>.Filter.Eq(x => x.DeviceId, name);
                filter = filter & findByNameFilter;
            }

            var runningDeviceSimulations = Db.Find(filter);
            return runningDeviceSimulations;
        }

        public RunningDeviceSimulations GetRunningDeviceSimulations(string id)
        {
            return Db.FindOneById<RunningDeviceSimulations>(id);
        }
    }
}
