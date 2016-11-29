using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.Domain;
using CodeMash.Net;
using FluentValidation.Results;
using MongoDB.Bson;
using MongoDB.Driver;
using PresentConnection.Internship7.Iot.Utils;

namespace PresentConnection.Internship7.Iot.BusinessImplementation
{
    public class UserDeviceService : IUserDeviceService
    {
        public string CreateUserDevice(UserDevices userDevice)
        {
            UserDeviceValidators validator = new UserDeviceValidators();
            ValidationResult results = validator.Validate(userDevice);

            bool validationSucceeded = results.IsValid;

            if (validationSucceeded)
            {
                Db.InsertOne(userDevice);
                return userDevice.Id.ToString();
            }
            else
            {
                throw new BusinessException("Cannot create UserDevice", results.Errors);
            }
        }

        public void UpdateUserDevice(UserDevices userDevice)
        {
            UserDeviceValidators validator = new UserDeviceValidators();
            ValidationResult results = validator.Validate(userDevice);
            bool validationSucceeded = results.IsValid;

            if (validationSucceeded)
            {
                Db.FindOneAndReplace(x => x.Id == userDevice.Id, userDevice);
            }
            else
            {
                throw new BusinessException("Cannot update UserDevice", results.Errors);
            }
        }

        public bool DeleteUserDevice(string id)
        {
            var deleteResult = Db.DeleteOne<UserDevices>(x => x.Id == ObjectId.Parse(id));
            return deleteResult.DeletedCount == 1;
        }

        public List<UserDevices> GetAllUserDevices(string userId = "")
        {
            var filterBuilder = Builders<UserDevices>.Filter;
            var filter = filterBuilder.Empty;

            if (!string.IsNullOrEmpty(userId))
            {
                var findByNameFilter = Builders<UserDevices>.Filter.Eq(x => x.UserId, userId);
                filter = filter & findByNameFilter;
            }

            var userDevices = Db.Find(filter);
            return userDevices;
        }

        public UserDevices GetUserDevice(string id)
        {
            return Db.FindOneById<UserDevices>(id);
        }
    }
}
