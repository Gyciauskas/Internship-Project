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
    public class DeviceService : IDeviceService
    {
        public string CreateDevice(Device device)
        {
            DeviceValidator validator = new DeviceValidator();
            DeviceUniqueNameValidator uniqueNameValidator = new DeviceUniqueNameValidator();
            ValidationResult results = validator.Validate(device);
            ValidationResult results2 = uniqueNameValidator.Validate(device);
            bool validationSucceeded = results.IsValid && results2.IsValid;
            if (validationSucceeded)
            {
                Db.InsertOne(device);
                return device.Id.ToString();
            }
            else
            {
                throw new BusinessException("Cannot create device", results.Errors);
            }
        }

        public void UpdateDevice(Device device)
        {
            DeviceValidator validator = new DeviceValidator();
            ValidationResult results = validator.Validate(device);
            bool validationSucceeded = results.IsValid;
            if (validationSucceeded)
            {
                Db.FindOneAndReplace(x => x.Id == device.Id, device);
            }
            else
            {
                throw new BusinessException("Cannot update device", results.Errors);
            }
        }

        public bool DeleteDevice(string id)
        {
            var deleteResult = Db.DeleteOne<Device>(x => x.Id == ObjectId.Parse(id));
            return deleteResult.DeletedCount == 1;
        }

        public List<Device> GetAllDevices(string name = "")
        {
            var filterBuilder = Builders<Device>.Filter;
            var filter = filterBuilder.Empty;

            if (!string.IsNullOrEmpty(name))
            {
                var findByNameFilter = Builders<Device>.Filter.Eq(x => x.ModelName, name);
                filter = filter & findByNameFilter;
            }

            var devices = Db.Find(filter);
            return devices;
        }

        public Device GetDevice(string id)
        {
            return Db.FindOneById<Device>(id);
        }
    }
}
