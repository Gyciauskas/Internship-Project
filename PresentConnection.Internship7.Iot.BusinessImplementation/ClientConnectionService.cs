using System;
using CodeMash.Net;
using PresentConnection.Internship7.Iot.Domain;
using FluentValidation.Results;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using PresentConnection.Internship7.Iot.Utils;
using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.BusinessImplementation;

namespace BusinessImplementation
{
    public class ClientConnectionService : IClientConnectionService
    {
        /// <summary>
        ///     Check or clientId and connectionId is not empty, if no throw exception
        ///     Insert document to mongodb database
        ///     return clientId as string
        /// </summary>
        public void CreateClientConnection(ClientConnection clientconnection, string responsibleClientId)
        {
            var validator = new ClientConnectionValidator(responsibleClientId);
            var results = validator.Validate(clientconnection);
            var validationSucceeded = results.IsValid;

            if (validationSucceeded)
            {
                Db.InsertOne(clientconnection);

                // Create and insert dashboard to db
                IDashboardService dashboardService = new DashboardService();

                var dashboard = new Dashboard { ClientId = clientconnection.ClientId };

                // To pass validation, I have to put to dashboard ClientId.
                dashboardService.UpdateDashboard(dashboard);
            }
            else
            {
                throw new BusinessException("Couldn't create client connection", results.Errors);
            }
        }

        /// <summary>
        ///     Update existing document in db, before check validation
        ///     if clientId or connectionId empty, throw exception
        ///     otherwise, update in db
        /// </summary>
        public void UpdateClientConnection(ClientConnection clientconnection, string responsibleClientId)
        {
            try
            {
                GetClientConnection(clientconnection.Id.ToString(), responsibleClientId);
            }
            catch (BusinessException e)
            {

                throw new BusinessException("You don't have permissions to update this client connection", e);
            }

            var validator = new ClientConnectionValidator(responsibleClientId);
            var results = validator.Validate(clientconnection);
            var validationSucceeded = results.IsValid;

            if (validationSucceeded)
            {
                Db.FindOneAndReplace(x => x.Id == clientconnection.Id, clientconnection);
            }
            else
            {
                throw new BusinessException("Couldn't update client connection", results.Errors);
            }
        }

        /// <summary>
        ///     Get all documents in db or with same clientId
        ///     return list with documents
        /// </summary>
        public List<ClientConnection> GetClientConnections(string clientId, string responsibleClientId)
        {
            if (string.IsNullOrEmpty(clientId))
            {
                return new List<ClientConnection>();
            }

            var filter = Builders<ClientConnection>.Filter.Eq(x => x.ClientId, clientId);
            var clientConnections = Db.Find(filter);

            var validator = new RecordsPermissionValidator(responsibleClientId);
            var results = validator.Validate(clientConnections.Cast<IEntityWithSensitiveData>().ToList());
            var validationSucceeded = results.IsValid;

            if (validationSucceeded)
            {
                return clientConnections;
            }
            throw new BusinessException("You don't have permissions to get client connections", results.Errors);
        }

        /// <summary>
        ///     Find document by Id and result it back
        /// </summary>
        public ClientConnection GetClientConnection(string id, string responsibleClientId)
        {
            var clientConnection = Db.FindOneById<ClientConnection>(id);
            if (clientConnection == null)
            {
                return null;
            }

            var validator = new RecordPermissionValidator(responsibleClientId);
            var results = validator.Validate(clientConnection);
            var validationSucceeded = results.IsValid;

            if (validationSucceeded)
            {
                return clientConnection;
            }
            throw new BusinessException("You don't have permissions to get client connection", results.Errors);
        }

        /// <summary>
        ///    delete document by them Id
        ///    check result, it should be equal to 1
        ///    return result as bool
        /// </summary>
        public bool DeleteClientConnection(string id, string responsibleClientId)
        {
            try
            {
                GetClientConnection(id, responsibleClientId);
            }
            catch (BusinessException e)
            {
                
                throw new BusinessException("You don't have permissions to delete this client connection", e);
            }
            var deleteResult = Db.DeleteOne<ClientConnection>(x => x.Id == ObjectId.Parse(id));
            return deleteResult.DeletedCount == 1;
        }


    }
}


