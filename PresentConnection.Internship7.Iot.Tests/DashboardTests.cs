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
        private Dashboard gooddashboard;

        [SetUp]
        public void SetUp()
        {
            dashboardService = new DashboardService();

           
            Widget widget = new Widget();
            widget.Query = "test";
            widget.WidgetType = WidgetType.BatChart;

            widget.Configuration = new Dictionary<string, object>();
            widget.Configuration.Add("test", "test");

            List<Widget> widgets = new List<Widget>();
            widgets.Add(widget);

            gooddashboard = new Dashboard()
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
            dashboardService.UpdateDashboard(gooddashboard);
            gooddashboard.ShouldNotBeNull();
            gooddashboard.Id.ShouldNotBeNull();
        }



        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.Dashboard")]
        public void Cannot_insert_dashboard_to_database_when_clientId_is_NotSet()
        {
            gooddashboard.ClientId = string.Empty;

            typeof(BusinessException).ShouldBeThrownBy(() => dashboardService.UpdateDashboard(gooddashboard));
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.Dashboard")]
        public void Can_get_dashboard_by_client_id()
        {
           

            dashboardService.UpdateDashboard(gooddashboard);

            gooddashboard.ShouldNotBeNull();
            gooddashboard.Id.ShouldNotBeNull();

            var dashboardFromDb = dashboardService.GetDashboard(gooddashboard.ClientId);
            dashboardFromDb.Id.ShouldNotBeNull();
            dashboardFromDb.ClientId.ShouldEqual("3");
            dashboardFromDb.ShouldNotBeNull();
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.Dashboard")]
        public void Can_update_dashboards_to_database()
        {
            Widget widget = new Widget();
            widget.Query = "test";
            widget.WidgetType = WidgetType.BatChart;

            List<Widget> widgets = new List<Widget>();
            widgets.Add(widget);

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
            gooddashboard.ClientId = "53";
            dashboardService.UpdateDashboard(gooddashboard);

            // Get item from db and check if name was updated
            var dashboardFromDb = dashboardService.GetDashboard(gooddashboard.ClientId);
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
