using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.Domain;
using CodeMash.Net;
using FluentValidation.Results;
using MongoDB.Bson;
using MongoDB.Driver;
using PresentConnection.Internship7.Iot.Utils;
using FluentValidation;


namespace PresentConnection.Internship7.Iot.BusinessImplementation
{
    public class DashboardService : IDashboardService
    {

        public string CreateDashboard(Dashboard dashboard)
        {
            DashboardValidator validator = new DashboardValidator();
            ValidationResult results = validator.Validate(dashboard);

            bool validationSucceeded = results.IsValid;

            if (validationSucceeded)
            {
                Db.InsertOne(dashboard);
                return dashboard.Id.ToString();
            }
            else
            {
                throw new BusinessException("Cannot create dashboard", results.Errors);
            }
        }

        public void UdpdateDashboard(Dashboard dashboard)
        {
            DashboardValidator validator = new DashboardValidator();
            ValidationResult results = validator.Validate(dashboard);

            bool validationSucceeded = results.IsValid;

            if (validationSucceeded)
            {
                Db.FindOneAndReplace(x => x.Id == dashboard.Id, dashboard);
            }
            else
            {
                throw new BusinessException("Cannot update dashboard", results.Errors);
            }
        }

        public bool DeleteDashboard(string id)
        {
            var deleteResult = Db.DeleteOne<Dashboard>(x => x.Id == ObjectId.Parse(id));
            return deleteResult.DeletedCount == 1;
        }

        public List<Dashboard> GetAllDashboard(string userId = "")
        {
            var filterBuilder = Builders<Dashboard>.Filter;
            var filter = filterBuilder.Empty;

            if (!string.IsNullOrEmpty(userId))
            {
                var findByNameFilter = Builders<Dashboard>.Filter.Eq(x => x.UserId, userId);
                filter = filter & findByNameFilter;
            }

            var dashboard = Db.Find(filter);
            return dashboard;
        }

        public Dashboard GetDashboard(string id)
        {
            return Db.FindOneById<Dashboard>(id);
        }


    }
}
