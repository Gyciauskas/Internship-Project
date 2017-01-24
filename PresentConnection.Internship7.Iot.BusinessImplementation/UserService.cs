using System.Collections.Generic;
using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.Domain;
using CodeMash.Net;
using FluentValidation.Results;
using MongoDB.Bson;
using MongoDB.Driver;
using PresentConnection.Internship7.Iot.Utils;
using PresentConnection.Internship7.Iot.Domain.Validators;

namespace PresentConnection.Internship7.Iot.BusinessImplementation
{
    public class UserService : IUserService
    {
        // Created DI container, using it
        public IClientDeviceService clientDeviceService { get; set; }
        public IComponentService componentService { get; set; }
        public IRunningDeviceSimulationsService runningDeviceSimulationsService { get; set; }

        public void CreateUser(User user)
        {
            UserValidator validator = new UserValidator();
            ValidationResult results = validator.Validate(user);

            bool validationSucceeded = results.IsValid;
            if (validationSucceeded)
            {
                Db.InsertOne(user);
            }
            else
            {
                throw new BusinessException("Cannot create user", results.Errors);
            }
        }

        public void UpdateUser(User user)
        {
            UserValidator validator = new UserValidator();
            ValidationResult results = validator.Validate(user);

            bool validationSucceeded = results.IsValid;

            if (validationSucceeded)
            {
                Db.FindOneAndReplace(x => x.Id == user.Id, user);
            }
            else
            {
                throw new BusinessException("Cannot update user", results.Errors);
            }
        }

        public bool DeleteUser(string id)
        {
            var deleteResult = Db.DeleteOne<User>(x => x.Id == ObjectId.Parse(id));
            return deleteResult.DeletedCount == 1;
        }

        public List<User> GetAllUsers(string name = "")
        {
            var filterBuilder = Builders<User>.Filter;
            var filter = filterBuilder.Empty;

            if (!string.IsNullOrEmpty(name))
            {
                var findByNameFilter = Builders<User>.Filter.Eq(x => x.FullName, name);
                filter = filter & findByNameFilter;
            }

            var users = Db.Find(filter);
            return users;
        }

        public User GetUser(string id)
        {
            return Db.FindOneById<User>(id);
        }
    }
}
