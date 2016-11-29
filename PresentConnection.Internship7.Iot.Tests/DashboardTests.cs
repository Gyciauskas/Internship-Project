using MongoDB.Bson;
using NUnit.Framework;
using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.BusinessImplementation;
using PresentConnection.Internship7.Iot.Domain;
using System.Collections.Generic;
using PresentConnection.Internship7.Iot.Utils;
using CodeMash.Net;

namespace PresentConnection.Internship7.Iot.Tests
{
    [TestFixture]
    public class DashboardTests
    {
        private IDashboardService dashboardService;

        [SetUp]
        public void SetUp()
        {
            dashboardService = new DashboardService();
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.Dashboard")]

        public void Can_insert_dashboard_to_database()
        {

            Widget widget = new Widget();
            
            widget.Query = "test";
            widget.Type = Type.BatChart;


            widget.Configuration = new Dictionary<string, object>();
            widget.Configuration.Add("test","test");
            

            List<Widget> widgets = new List<Widget>();
            widgets.Add(widget);

         
            var dashboard = new Dashboard()
            {
                UserId = "7",
                Widgets = widgets

            };


            dashboardService.CreateDashboard(dashboard);
            dashboard.ShouldNotBeNull();
            dashboard.Id.ShouldNotBeNull();
        }



        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.Dashboard")]

        public void Cannot_insert_dashboard_to_database_when_widgetType_is_NotSet()
        {

            Widget widget = new Widget();
            
            
            widget.Query = "test";
            widget.Type = Type.NotSet;


            widget.Configuration = new Dictionary<string, object>();
            widget.Configuration.Add("test", "test");


            List<Widget> widgets = new List<Widget>();
            widgets.Add(widget);


            var dashboard = new Dashboard()
            {
                UserId = "8",
                Widgets = widgets

            };

            typeof(BusinessException).ShouldBeThrownBy(() => dashboardService.CreateDashboard(dashboard));
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.Dashboard")]
        public void Can_get_dashboards_by_id()
        {
            Widget widget = new Widget();

            widget.Query = "test2";
            widget.Type = Type.BatChart;


            widget.Configuration = new Dictionary<string, object>();
            widget.Configuration.Add("test2", "test2");


            List<Widget> widgets = new List<Widget>();
            widgets.Add(widget);


            var dashboard = new Dashboard()
            {
                UserId = "19",
                Widgets = widgets

            };

            dashboardService.CreateDashboard(dashboard);

            dashboard.ShouldNotBeNull();
            dashboard.Id.ShouldNotBeNull();

            var dashboardFromDb = dashboardService.GetDashboard(dashboard.Id.ToString());
            dashboardFromDb.Id.ShouldNotBeNull();
            dashboardFromDb.UserId.ShouldEqual("19");
            dashboardFromDb.ShouldNotBeNull();
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.Dashboard")]
        public void Can_update_dashboards_to_database()
        {
            Widget widget = new Widget();
            widget.Query = "test";
            widget.Type = Type.BatChart;

            List<Widget> widgets = new List<Widget>();
            widgets.Add(widget);

            var dashboard = new Dashboard()
            {
                UserId = "88",
                Widgets = widgets
            };

            dashboardService.CreateDashboard(dashboard);

            // First insertdashboard to db
            dashboard.ShouldNotBeNull();
            dashboard.Id.ShouldNotBeNull();

            // Update name and send update to db
            dashboard.UserId = "53";
            dashboardService.UdpdateDashboard(dashboard);

            // Get item from db and check if name was updated
            var dashboardFromDb = dashboardService.GetDashboard(dashboard.Id.ToString());
            dashboardFromDb.ShouldNotBeNull();
            dashboardFromDb.UserId.ShouldEqual("53");
        }


        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.Dashboard")]
        public void Can_delete_dashboards_from_database()
        {
            Widget widget = new Widget();

            widget.Query = "test";
            widget.Type = Type.BatChart;


            widget.Configuration = new Dictionary<string, object>();
            widget.Configuration.Add("test", "test");


            List<Widget> widgets = new List<Widget>();
            widgets.Add(widget);


            var dashboard = new Dashboard()
            {
                UserId = "88",
                Widgets = widgets

            };

            dashboardService.CreateDashboard(dashboard);

            // First insert dashboard to db
            dashboard.ShouldNotBeNull();
            dashboard.Id.ShouldNotBeNull();

            // Delete dashboard from db
            dashboardService.DeleteDashboard(dashboard.Id.ToString());

            // Get item from db and check if name was updated
            var dashboardFromDb = dashboardService.GetDashboard(dashboard.Id.ToString());

            // issue with CodeMash library. Should return null when not found - not default object
            dashboardFromDb.ShouldNotBeNull();
            dashboardFromDb.Id.ShouldEqual(ObjectId.Empty);
        }

        [TearDown]
        public void Dispose()
        {
            var dashboards = Db.Find<Dashboard>(x => true);
            foreach (var dashboard in dashboards)
            {
                dashboardService.DeleteDashboard(dashboard.Id.ToString());
            }
        }
    }
}
