using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.Domain;
using CodeMash.Net;
using MongoDB.Bson;
using MongoDB.Driver;
using PresentConnection.Internship7.Iot.Utils;

namespace PresentConnection.Internship7.Iot.BusinessImplementation
{
    public class DashboardService : IDashboardService
    {
        public void UpdateDashboard(Dashboard dashboard)
        {
            var validator = new DashboardValidator();
            var results = validator.Validate(dashboard);
            var validationSucceeded = results.IsValid;

            if (validationSucceeded)
            {
                Db.ReplaceOne(x => x.Id == dashboard.Id, dashboard, new UpdateOptions { IsUpsert = true });
            }
            else
            {
                throw new BusinessException("Cannot update dashboard", results.Errors);
            }
        }
        
        public Dashboard GetDashboard(string clientId)
        {
            return Db.FindOne<Dashboard>(x => x.ClientId == clientId);
        }


    }
}
