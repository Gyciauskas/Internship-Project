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
        public void CreateClientConnection(ClientConnection clientconnection)
        {
            var validator = new ClientConnectionValidator();
            var results = validator.Validate(clientconnection);
            var validationSucceeded = results.IsValid;

            if (validationSucceeded)
            {
                Db.InsertOne(clientconnection);
            }
            else
            {
                throw new BusinessException("Cannot create client connection", results.Errors);
            }
        }

        /// <summary>
        ///     Update existing document in db, before check validation
        ///     if clientId or connectionId empty, throw exception
        ///     otherwise, update in db
        /// </summary>
        public void UpdateClientConnection(ClientConnection clientconnection)
        {
            var validator = new ClientConnectionValidator();
            var results = validator.Validate(clientconnection);
            var validationSucceeded = results.IsValid;

            if (validationSucceeded)
            {
                Db.FindOneAndReplace(x => x.Id == clientconnection.Id, clientconnection);
            }
            else
            {
                throw new BusinessException("Cannot update client connection", results.Errors);
            }
        }

        /// <summary>
        ///     Get all documents in db or with same clientId
        ///     return list with documents
        /// </summary>
        public List<ClientConnection> GetClientConnections(string clientId)
        {
            if (string.IsNullOrEmpty(clientId))
            {
                return new List<ClientConnection>();
            }

            var filter = Builders<ClientConnection>.Filter.Eq(x => x.ClientId, clientId);
            var clientconnections = Db.Find(filter);
            return clientconnections;
        }

        /// <summary>
        ///     Find document by Id and result it back
        /// </summary>
        public ClientConnection GetClientConnection(string id)
        {
            return Db.FindOneById<ClientConnection>(id);
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


    }
}


