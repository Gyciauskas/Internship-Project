using MongoDB.Bson;
using NUnit.Framework;
using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.BusinessImplementation;
using PresentConnection.Internship7.Iot.Domain;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using PresentConnection.Internship7.Iot.Utils;
using CodeMash.Net;
using MongoDB.Driver;

namespace PresentConnection.Internship7.Iot.Tests
{
    [TestFixture]
    public class DashboardTests
    {
        private IDashboardService dashboardService;
        private Dashboard goodDashboard;

        [SetUp]
        public void SetUp()
        {
            dashboardService = new DashboardService();


            var widget = new Widget
            {
                Query = "test",
                WidgetType = WidgetType.BatChart,
                Configuration = new Dictionary<string, object> {{"test", "test"}}
            };
            
            var widgets = new List<Widget> {widget};

            goodDashboard = new Dashboard
            {
                ClientId = "5",
                Widgets = widgets

            };
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.Dashboard")]

        public void Can_insert_dashboard_to_database()
        {
            dashboardService.UpdateDashboard(goodDashboard);

            goodDashboard.ShouldNotBeNull();
            goodDashboard.Id.ShouldNotBeNull();
        }



        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.Dashboard")]
        public void Cannot_insert_dashboard_to_database_when_clientId_is_NotSet()
        {
            goodDashboard.ClientId = string.Empty;

            typeof(BusinessException).ShouldBeThrownBy(() => dashboardService.UpdateDashboard(goodDashboard));
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.Dashboard")]
        public void Can_get_dashboard_by_client_id()
        {
            dashboardService.UpdateDashboard(goodDashboard);

            goodDashboard.ShouldNotBeNull();
            goodDashboard.Id.ShouldNotBeNull();

            var dashboardFromDb = dashboardService.GetDashboard(goodDashboard.ClientId);

            dashboardFromDb.Id.ShouldNotBeNull();
            dashboardFromDb.ClientId.ShouldEqual("5");
            dashboardFromDb.ShouldNotBeNull();
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.Dashboard")]
        public void Can_update_dashboards_to_database()
        {
            var widget = new Widget
            {
                Query = "test",
                WidgetType = WidgetType.BatChart
            };

            var widgets = new List<Widget> {widget};

            var dashboard = new Dashboard()
            {
                ClientId = "88",
                Widgets = widgets
            };

            dashboardService.UpdateDashboard(dashboard);

            // First insertdashboard to db
            dashboard.ShouldNotBeNull();
            dashboard.Id.ShouldNotBeNull();

            // Update name and send update to db
            goodDashboard.ClientId = "53";
            dashboardService.UpdateDashboard(goodDashboard);

            // Get item from db and check if name was updated
            var dashboardFromDb = dashboardService.GetDashboard(goodDashboard.ClientId);
            dashboardFromDb.ShouldNotBeNull();
            dashboardFromDb.ClientId.ShouldEqual("53");
        }


        [TearDown]
        public void Dispose()
        {
            var dashboards = Db.Find<Dashboard>(x => true);

            foreach (var dashboard in dashboards)
            {
                Db.FindOneAndDelete(Builders<Dashboard>.Filter.Eq(x => x.Id, dashboard.Id));
            }
        }
    }
}
