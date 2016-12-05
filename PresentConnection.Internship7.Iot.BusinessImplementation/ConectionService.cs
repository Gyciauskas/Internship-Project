using System.Collections.Generic;
using CodeMash.Net;
using MongoDB.Bson;
using MongoDB.Driver;
using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.Domain;
using PresentConnection.Internship7.Iot.Utils;

namespace PresentConnection.Internship7.Iot.BusinessImplementation
{
    public class ConectionService : IConnectionService
    {
        public void CreateConnection(Connection connection)
        {
            var validator = new ConnectionValidator();
            var results = validator.Validate(connection);
            var validationSucceeded = results.IsValid;

            if (validationSucceeded)
            {
                Db.InsertOne(connection);
            }
            else
            {
                throw new BusinessException("Cannot create connection", results.Errors);
            }
        }

        public void UpdateConnection(Connection connection)
        {
            var validator = new ConnectionValidator();
            var results = validator.Validate(connection);
            var validationSucceeded = results.IsValid;

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

        public List<Connection> GetAllConnections(string name= "")
        {
            var filterBuilder = Builders<Connection>.Filter;
            var filter = filterBuilder.Empty;

            if (!string.IsNullOrEmpty(name))
            {
                var findByNameFilter = Builders<Connection>.Filter.Regex(x => x.Name, new BsonRegularExpression(name, "i"));
                filter = filter & findByNameFilter;
            }

            var connections = Db.Find(filter);
            return connections;
        }

        public Connection GetConnection(string id)
        {
            return Db.FindOneById<Connection>(id);
        }
    }
}
