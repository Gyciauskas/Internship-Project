using CodeMash.Net;
using PresentConnection.Internship7.Iot.Domain;
using FluentValidation.Results;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using PresentConnection.Internship7.Iot.Utils;
using PresentConnection.Internship7.Iot.BusinessContracts;

namespace BusinessImplementation
{
    public class ClientConnectionService : IClientConnectionService
    {
        /// <summary>
        ///     Check or clientId and connectionId is not empty, if no throw exception
        ///     Insert document to mongodb database
        ///     return clientId as string
        /// </summary>
        public string CreateClientConnection(ClientConnection clientconnection)
        {
            ClientConnectionValidator validator = new ClientConnectionValidator();
            ValidationResult results = validator.Validate(clientconnection);

            bool validationSucceeded = results.IsValid;

            if (validationSucceeded)
            {
                Db.InsertOne(clientconnection);
                return clientconnection.ClientId.ToString();
            }
            else
            {
                throw new BusinessException("Cannot create clientconnection", results.Errors);
            }
        }

        /// <summary>
        ///    delete document by them Id
        ///    check result, it should be equal to 1
        ///    return result as bool
        /// </summary>
        public bool DeleteClientConnection(string id)
        {
            var deleteResult = Db.DeleteOne<ClientConnection>(x => x.Id == ObjectId.Parse(id));

            return deleteResult.DeletedCount == 1;
        }

        /// <summary>
        ///     Get all documents in db or with same clientId
        ///     return list with documents
        /// </summary>
        public List<ClientConnection> GetAllClientConnections(string clientId = "")
        {
            var filterBuilder = Builders<ClientConnection>.Filter;
            var filter = filterBuilder.Empty;

            if (!string.IsNullOrEmpty(clientId))
            {
                var findByclientIdFilter = Builders<ClientConnection>.Filter.Eq(x => x.ClientId, clientId);
                filter = filter & findByclientIdFilter;
            }

            var clientconnections = Db.Find(filter);

            return clientconnections;
        }

        /// <summary>
        ///     Find document by Id and result it back
        /// </summary>
        public ClientConnection GetClientConnection(string Id)
        {
            return Db.FindOneById<ClientConnection>(Id);
        }

        /// <summary>
        ///     Update existing document in db, before check validation
        ///     if clientId or connectionId empty, throw exception
        ///     otherwise, update in db
        /// </summary>
        public void UpdateClientConnection(ClientConnection clientconnection)
        {
            ClientConnectionValidator validator = new ClientConnectionValidator();
            ValidationResult results = validator.Validate(clientconnection);

            bool validationSucceeded = results.IsValid;

            if (validationSucceeded)
            {
                Db.FindOneAndReplace(x => x.Id == clientconnection.Id, clientconnection);
            }
            else
            {
                throw new BusinessException("Cannot update clientconnection", results.Errors);
            }
        }
    }
}


