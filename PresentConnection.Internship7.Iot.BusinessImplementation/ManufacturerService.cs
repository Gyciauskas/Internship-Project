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
    public class ManufacturerService : IManufacturerService
    {
        public string CreateManufacturer(Manufacturer manufacturer)
        {
            ManufacturerValidator validator = new ManufacturerValidator();
            ValidationResult results = validator.Validate(manufacturer);

            bool validationSucceeded = results.IsValid;

            if (validationSucceeded)
            {
                Db.InsertOne(manufacturer);
                return manufacturer.Id.ToString();
            }
            else
            {
                throw new BusinessException("Cannot create manufacturer", results.Errors);
            }
        }

        public void UdpdateManufacturer(Manufacturer manufacturer)
        {
            ManufacturerValidator validator = new ManufacturerValidator();
            ValidationResult results = validator.Validate(manufacturer);

            bool validationSucceeded = results.IsValid;

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
                var findByNameFilter = Builders<Manufacturer>.Filter.Eq(x => x.Name, name);
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
