using System.Collections.Generic;
using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.Domain;
using CodeMash.Net;
using MongoDB.Bson;
using MongoDB.Driver;
using PresentConnection.Internship7.Iot.Utils;

namespace PresentConnection.Internship7.Iot.BusinessImplementation
{
    public class CollaboratorService : ICollaboratorService
    {
        public void CreateCollaborator(Collaborator collaborator)
        {
            var validator = new CollaboratorValidator();
            var results = validator.Validate(collaborator);

            var validationSucceeded = results.IsValid;

            if (validationSucceeded)
            {
                Db.InsertOne(collaborator);
            }
            else
            {
                throw new BusinessException("Cannot create collaborator", results.Errors);
            }
        }

        public void UpdateCollaborator(Collaborator collaborator)
        {
            var validator = new CollaboratorValidator();
            var results = validator.Validate(collaborator);
            var validationSucceeded = results.IsValid;

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

        public List<Collaborator> GetAllCollaborators(string name = "")
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
