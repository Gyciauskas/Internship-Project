using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.Domain;
using CodeMash.Net;
using MongoDB.Driver;
using PresentConnection.Internship7.Iot.Utils;
using PresentConnection.Internship7.Iot.Domain.Validators;
using System.Linq;

namespace PresentConnection.Internship7.Iot.BusinessImplementation
{
    public class SettingsService : ISettingsService
    {
        public void UpdateOrInsertSettings(Settings settings)
        {
            var validator = new SettingsValidator();
            var results = validator.Validate(settings);
            var validationSucceeded = results.IsValid;

            if (validationSucceeded)
            {
                Db.ReplaceOne(Builders<Settings>.Filter.Eq(x => x.Id, settings.Id), settings, new UpdateOptions { IsUpsert = true });
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
