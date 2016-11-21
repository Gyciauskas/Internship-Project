using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.Domain;
using CodeMash.Net;
using FluentValidation.Results;
using MongoDB.Bson;
using MongoDB.Driver;
using PresentConnection.Internship7.Iot.Utils;
using PresentConnection.Internship7.Iot.Domain.Validators;
using System.Collections.Generic;

namespace PresentConnection.Internship7.Iot.BusinessImplementation
{
    public class SettingService:ISetingService
    {
        public string CreateSetting(Setting setting)
        {
            SettingValidator validator = new SettingValidator();
            ValidationResult results = validator.Validate(setting);
            bool validationSucceeded = results.IsValid;
            if (validationSucceeded)
            {
                Db.InsertOne(setting);
                return setting.Id.ToString();
            }
            else
            {
                throw new BusinessException("Cannot create setting", results.Errors);
            }

        }

        public void UpdateSetting(Setting setting)
        {
            SettingValidator validator = new SettingValidator();
            ValidationResult results = validator.Validate(setting);
            bool validationSucceeded = results.IsValid;
            if (validationSucceeded)
            {
                Db.FindOneAndReplace(x => x.Id == setting.Id, setting);
            }
            else
            {
                throw new BusinessException("Cannot update setting", results.Errors);
            }
        }

        public bool DeleteSetting(string id)
        {
            var deleteResult = Db.DeleteOne<Setting>(x => x.Id == ObjectId.Parse(id));
            return deleteResult.DeletedCount == 1;
        }

        public List<Setting> GetAllSettings(string name = "")
        {
            var filterBuilder = Builders<Setting>.Filter;
            var filter = filterBuilder.Empty;

            if (!string.IsNullOrEmpty(name))
            {
                var findByNameFilter = Builders<Setting>.Filter.Eq(x => x.SettingsAsJson, name);
                filter = filter & findByNameFilter;
            }

            var settings = Db.Find(filter);
            return settings;
        }

        public Setting GetSetting(string id)
        {

            return Db.FindOneById<Setting>(id);

        }


    }
}
