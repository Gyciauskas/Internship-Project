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
    public class ComponentService : IComponentService
    {

        public string CreateComponent(Component component)
        {
            ComponentValidator validator = new ComponentValidator();
            ValidationResult results = validator.Validate(component);
            bool validationSucceeded = results.IsValid;
            if (validationSucceeded)
            {
                Db.InsertOne(component);
                return component.Id.ToString();
            }
            else
            {
                throw new BusinessException("Cannot create component", results.Errors);
            }

        }

        public void UpdateComponent(Component component)
        {
            ComponentValidator validator = new ComponentValidator();
            ValidationResult results = validator.Validate(component);
            bool validationSucceeded = results.IsValid;
            if (validationSucceeded)
            {
                Db.FindOneAndReplace(x => x.Id == component.Id, component);
            }
            else
            {
                throw new BusinessException("Cannot update component", results.Errors);
            }
        }



        public bool DeleteComponent(string id)
        {
            var deleteResult = Db.DeleteOne<Component>(x => x.Id == ObjectId.Parse(id));
            return deleteResult.DeletedCount == 1;
        }

        public List<Component> GetAllComponents(string name = "")
        {
            var filterBuilder = Builders<Component>.Filter;
            var filter = filterBuilder.Empty;

            if (!string.IsNullOrEmpty(name))
            {
                var findByNameFilter = Builders<Component>.Filter.Eq(x => x.ModelName, name);
                filter = filter & findByNameFilter;
            }

            var components = Db.Find(filter);
            return components;
        }


        public Component GetComponent(string id)
        {

            return Db.FindOneById<Component>(id);

        }

    }
}

