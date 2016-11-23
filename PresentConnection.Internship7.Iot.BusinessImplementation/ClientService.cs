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
    public class ClientService : IClientService
    {
        public string CreateClient(Client client)
        {
            ClientValidator validator = new ClientValidator();
            ValidationResult results = validator.Validate(client);

            bool validationSucceeded = results.IsValid;
            if (validationSucceeded)
            {
                Db.InsertOne(client);
                return client.Id.ToString();
            }
            else
            {
                throw new BusinessException("Cannot create Client", results.Errors);
            }
        }

        public void UpdateClient(Client client)
        {
            ClientValidator validator = new ClientValidator();
            ValidationResult results = validator.Validate(client);

            bool validationSucceeded = results.IsValid;
            if (validationSucceeded)
            {
                Db.FindOneAndReplace(x => x.Id == client.Id, client);
            }
            else
            {
                throw new BusinessException("Cannot update Client", results.Errors);
            }
        }

        public bool DeleteClient(string id)
        {
            var deleteResult = Db.DeleteOne<Client>(x => x.Id == ObjectId.Parse(id));
            return deleteResult.DeletedCount == 1;
        }

        public List<Client> GetAllClients()
        {
            return Db.Find(Builders<Client>.Filter.Empty);
        }

        public Client GetClient(string id)
        {
            return Db.FindOneById<Client>(id);
        }
    }
}
