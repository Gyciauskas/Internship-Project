using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.Domain;
using CodeMash.Net;
using FluentValidation.Results;
using MongoDB.Bson;
using MongoDB.Driver;
using PresentConnection.Internship7.Iot.Utils;
using PresentConnection.Internship7.Iot.Domain.Validators;
using System.Collections.Generic;
using System.Linq;

namespace PresentConnection.Internship7.Iot.BusinessImplementation
{
    public class SettingsService:ISetingsService
    {

      
        public void UpdateOrInsertSettings(Settings settings)
        {
          

            SettingsValidator validator = new SettingsValidator();
            ValidationResult results = validator.Validate(settings);
            bool validationSucceeded = results.IsValid;
            if (validationSucceeded)
            {

                Db.FindOneAndReplace<Settings>(Builders<Settings>.Filter.Eq(x => x.Id, settings.Id), settings, new FindOneAndReplaceOptions<Settings> { IsUpsert = true });
          
            }

            else 
            {
                throw new BusinessException("Cannot update setting", results.Errors);
            }
        }


        public Settings GetSettings()
        {
            var settings = Db.Find<Settings>(_ => true).FirstOrDefault();
            return settings;
        }

    }
}
