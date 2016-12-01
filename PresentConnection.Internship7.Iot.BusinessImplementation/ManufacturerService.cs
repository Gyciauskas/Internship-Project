using System.Collections.Generic;
using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.Domain;
using CodeMash.Net;
using MongoDB.Bson;
using MongoDB.Driver;
using PresentConnection.Internship7.Iot.Utils;

namespace PresentConnection.Internship7.Iot.BusinessImplementation
{
    public class ManufacturerService : IManufacturerService
    {
        public void CreateManufacturer(Manufacturer manufacturer)
        {
            var validator = new ManufacturerValidator();
            var results = validator.Validate(manufacturer);
            var validationSucceeded = results.IsValid;

            if (validationSucceeded)
            {
                Db.InsertOne(manufacturer);
            }
            else
            {
                throw new BusinessException("Cannot create manufacturer", results.Errors);
            }
        }

        public void UdpdateManufacturer(Manufacturer manufacturer)
        {
            var validator = new ManufacturerValidator();
            var results = validator.Validate(manufacturer);
            var validationSucceeded = results.IsValid;

            if (validationSucceeded)
            {
                Db.FindOneAndReplace(x => x.Id == manufacturer.Id, manufacturer);
            }
            else
            {
                throw new BusinessException("Cannot update manufacturer", results.Errors);
            }
        }

        public bool DeleteManufacturer(string id)
        {
            var deleteResult = Db.DeleteOne<Manufacturer>(x => x.Id == ObjectId.Parse(id));
            return deleteResult.DeletedCount == 1;
        }

        public List<Manufacturer> GetAllManufacturers(string name = "")
        {
            var filterBuilder = Builders<Manufacturer>.Filter;
            var filter = filterBuilder.Empty;

            if (!string.IsNullOrEmpty(name))
            {
                var findByNameFilter = Builders<Manufacturer>.Filter.Regex(x => x.Name, new BsonRegularExpression(name, "i"));
                filter = filter & findByNameFilter;
            }

            var manufacturers = Db.Find(filter);
            return manufacturers;
        }

        public Manufacturer GetManufacturer(string id)
        {
            return Db.FindOneById<Manufacturer>(id);
        }
    }
}
