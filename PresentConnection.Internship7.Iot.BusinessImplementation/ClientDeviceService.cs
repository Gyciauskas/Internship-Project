using System.Collections.Generic;
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
        public void CreateClientDevice(ClientDevice clientDevice)
        {
            var validator = new UserDeviceValidators();
            var results = validator.Validate(clientDevice);
            var validationSucceeded = results.IsValid;

            if (validationSucceeded)
            {
                Db.InsertOne(clientDevice);
            }
            else
            {
                throw new BusinessException("Cannot create ClientDevice", results.Errors);
            }
        }

        public void UpdateClientDevice(ClientDevice clientDevice)
        {
            var validator = new UserDeviceValidators();
            var results = validator.Validate(clientDevice);
            var validationSucceeded = results.IsValid;

            if (validationSucceeded)
            {
                Db.FindOneAndReplace(x => x.Id == clientDevice.Id, clientDevice);
            }
            else
            {
                throw new BusinessException("Cannot update ClientDevice", results.Errors);
            }
        }

        public bool DeleteClientDevice(string id)
        {
            var deleteResult = Db.DeleteOne<ClientDevice>(x => x.Id == ObjectId.Parse(id));
            return deleteResult.DeletedCount == 1;
        }

        public List<ClientDevice> GetAllClientDevices(string clientId)
        {
            var filterBuilder = Builders<ClientDevice>.Filter;
            var filter = filterBuilder.Empty;

            if (!string.IsNullOrEmpty(clientId))
            {
                var findByNameFilter = Builders<ClientDevice>.Filter.Eq(x => x.ClientId, clientId);
                filter = filter & findByNameFilter;
            }

            var userDevices = Db.Find(filter);
            return userDevices;
        }

        public ClientDevice GetClientDevice(string id)
        {
            return Db.FindOneById<ClientDevice>(id);
        }
    }
}
