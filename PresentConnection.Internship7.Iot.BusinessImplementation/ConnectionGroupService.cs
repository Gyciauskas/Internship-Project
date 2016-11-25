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
    public class ConnectionGroupService : IConnectionGroupService
    {
        public string CreateConnectionGroup(ConnectionGroup connectionGroup)
        {
            ConnectionGroupValidator validator = new ConnectionGroupValidator();
            ValidationResult results = validator.Validate(connectionGroup);
            bool validationSucceeded = results.IsValid;
            if (validationSucceeded)
            {
                Db.InsertOne(connectionGroup);
                return connectionGroup.Id.ToString();
            }
            else
            {
                throw new BusinessException("Cannot create connectiongroup", results.Errors);
            }
        }

        public void UpdateConnectionGroup(ConnectionGroup connectionGroup)
        {
            ConnectionGroupValidator validator = new ConnectionGroupValidator();
            ValidationResult results = validator.Validate(connectionGroup);
            bool validationSucceded = results.IsValid;
            if (validationSucceded)
            {
                Db.FindOneAndReplace(x => x.Id == connectionGroup.Id, connectionGroup);
            }
            else
            {
                throw new BusinessException("Cannot update connectiongroup", results.Errors);
            }
        }

        public bool DeleteConnectionGroup(string id)
        {
            var deleteResult = Db.DeleteOne<ConnectionGroup>(x => x.Id == ObjectId.Parse(id));
            return deleteResult.DeletedCount == 1;
        }

        public List<ConnectionGroup> GetAllConnectionGroups()
        {
            return Db.Find(Builders<ConnectionGroup>.Filter.Empty);
        }

        public ConnectionGroup GetConnectionGroup(string id)
        {
            return Db.FindOneById<ConnectionGroup>(id);
        }
    }
}
