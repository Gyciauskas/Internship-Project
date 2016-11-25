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
    public class ConectionService : IConnectionService
    {
        public string CreateConnection(Connection connection)
        {
            ConnectionValidator validator = new ConnectionValidator();
            ValidationResult results = validator.Validate(connection);
            bool validationSucceeded = results.IsValid;
            if (validationSucceeded)
            {
                Db.InsertOne(connection);
                return connection.Id.ToString();
            }
            else
            {
                throw new BusinessException("Cannot create connection", results.Errors);
            }
        }

        public void UpdateConnection(Connection connection)
        {
            ConnectionValidator validator = new ConnectionValidator();
            ValidationResult results = validator.Validate(connection);
            bool validationSucceeded = results.IsValid;
            if (validationSucceeded)
            {
                Db.FindOneAndReplace(x => x.Id == connection.Id, connection);
            }
            else
            {
                throw new BusinessException("Cannot update connection", results.Errors);
            }
        }

        public bool DeleteConnection(string id)
        {
            var deleteResult = Db.DeleteOne<Connection>(x => x.Id == ObjectId.Parse(id));
            return deleteResult.DeletedCount == 1;
        }

        public List<Connection> GetAllConnections()
        {
            return Db.Find(Builders<Connection>.Filter.Empty);
        }

        public Connection GetConnection(string id)
        {
            return Db.FindOneById<Connection>(id);
        }
    }
}
