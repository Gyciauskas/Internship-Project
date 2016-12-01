using System.Collections.Generic;
using CodeMash.Net;
using MongoDB.Bson;
using MongoDB.Driver;
using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.Domain;
using PresentConnection.Internship7.Iot.Utils;

namespace PresentConnection.Internship7.Iot.BusinessImplementation
{
    public class DeviceService : IDeviceService
    {
        public void CreateDevice(Device device)
        {
            var validator = new DeviceValidator();
            var results = validator.Validate(device);
            var validationSucceeded = results.IsValid;

            if (validationSucceeded)
            {
                Db.InsertOne(device);
            }
            else
            {
                throw new BusinessException("Cannot create device", results.Errors);
            }
        }

        public void UpdateDevice(Device device)
        {
            var validator = new DeviceValidator();
            var results = validator.Validate(device);
            var validationSucceeded = results.IsValid;

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
