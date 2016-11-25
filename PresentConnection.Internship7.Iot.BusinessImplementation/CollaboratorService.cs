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
    public class CollaboratorService : ICollaboratorService
    {
        public string CreateCollaborator(Collaborator collaborator)
        {
            CollaboratorValidator validator = new CollaboratorValidator();
            ValidationResult results = validator.Validate(collaborator);

            bool validationSucceeded = results.IsValid;

            if (validationSucceeded)
            {
                Db.InsertOne(collaborator);
                return collaborator.Id.ToString();
            }
            else
            {
                throw new BusinessException("Cannot create collaborator", results.Errors);
            }
        }

        public void UpdateCollaborator(Collaborator collaborator)
        {
            CollaboratorValidator validator = new CollaboratorValidator();
            ValidationResult results = validator.Validate(collaborator);

            bool validationSucceeded = results.IsValid;

            if (validationSucceeded)
            {
                Db.FindOneAndReplace(x => x.Id == collaborator.Id, collaborator);
            }
            else
            {
                throw new BusinessException("Cannot update collaborator", results.Errors);
            }
        }

        public bool DeleteCollaborator(string id)
        {
            var deleteResult = Db.DeleteOne<Collaborator>(x => x.Id == ObjectId.Parse(id));
            return deleteResult.DeletedCount == 1;
        }

        public List<Collaborator> GetAllCollaborator(string name = "")
        {
            var filterBuilder = Builders<Collaborator>.Filter;
            var filter = filterBuilder.Empty;

            if (!string.IsNullOrEmpty(name))
            {
                var findByNameFilter = Builders<Collaborator>.Filter.Eq(x => x.Name, name);
                filter = filter & findByNameFilter;
            }

            var collaborator = Db.Find(filter);
            return collaborator;
        }

        public Collaborator GetCollaborator(string id)
        {
            return Db.FindOneById<Collaborator>(id);
        }
    }
}
