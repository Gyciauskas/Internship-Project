using System.Collections.Generic;
using CodeMash.Net;
using FluentValidation.Results;
using MongoDB.Bson;
using MongoDB.Driver;
using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.Domain;
using PresentConnection.Internship7.Iot.Utils;

namespace PresentConnection.Internship7.Iot.BusinessImplementation
{
    public class LookupService : ILookupService
    {
        public void CreateLookup(Lookup lookup)
        {
            var validator = new LookupValidator();
            var results = validator.Validate(lookup);
            var validationSucceeded = results.IsValid;

            if (validationSucceeded)
            {
                Db.InsertOne(lookup);
            }
            else
            {
                throw new BusinessException("Cannot create Lookup", results.Errors);
            }
        }

        public void UpdateLookup(Lookup lookup)
        {
            var validator = new LookupValidator();
            var results = validator.Validate(lookup);
            var validationSucceeded = results.IsValid;

            if (validationSucceeded)
            {
                Db.FindOneAndReplace(x => x.Id == lookup.Id, lookup);
            }
            else
            {
                throw new BusinessException("Cannot update Lookup", results.Errors);
            }
        }

        public bool DeleteLookup(string id)
        {
            var deleteResult = Db.DeleteOne<Lookup>(x => x.Id == ObjectId.Parse(id));
            return deleteResult.DeletedCount == 1;
        }

        public List<Lookup> GetAllLookups(string type = "")
        {
            var filterBuilder = Builders<Lookup>.Filter;
            var filter = filterBuilder.Empty;

            if (!string.IsNullOrEmpty(type))
            {
                var findByTypeFilter = Builders<Lookup>.Filter.Eq(x => x.Type, type);
                filter = filter & findByTypeFilter;
            }
            var lookups = Db.Find(filter);
            return lookups;
        }

        public Lookup GetLookup(string id)
        {
            return Db.FindOneById<Lookup>(id);
        }
    }
}
