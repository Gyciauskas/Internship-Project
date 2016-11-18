using MongoDB.Bson;
using NUnit.Framework;
using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.BusinessImplementation;
using PresentConnection.Internship7.Iot.Domain;
using System.Collections.Generic;
using PresentConnection.Internship7.Iot.Utils;

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
            widget.Type = "test";
            widget.Query = "test";

            widget.Configuration = new Dictionary<string, string>();
            widget.Configuration.Add("test","test");
            

            List<Widget> widgets = new List<Widget>();
            widgets.Add(widget);

         
            var dashboard = new Dashboard()
            {
                UserId = "Raspberry PI",
                Widgets = widgets

            };


            dashboardService.CreateDashboard(dashboard);
            dashboard.ShouldNotBeNull();
            dashboard.Id.ShouldNotBeNull();
        }


        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.Dashboard")]
        public void Can_get_dashboard_by_id()
        {
            Widget widget = new Widget();
            widget.Type = "test";
            widget.Query = "test";
            List<Widget> widgets = new List<Widget>();
            widgets.Add(widget);

            var dashboard = new Dashboard()
            {
                UserId = "Raspberry PI",
                Widgets = widgets
            };

            dashboardService.CreateDashboard(dashboard);

            dashboard.ShouldNotBeNull();
            dashboard.Id.ShouldNotBeNull();

            var dashboardFromDb = dashboardService.GetDashboard(dashboard.Id.ToString());
            dashboardFromDb.Id.ShouldNotBeNull();
            dashboardFromDb.UserId.ShouldEqual("Raspberry PI");
            dashboardFromDb.ShouldNotBeNull();
        }


        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.Dashboard")]
        public void Can_get_all_dashboard()
        {
            Widget widget = new Widget();
            widget.Type = "test";
            widget.Query = "test";
            List<Widget> widgets = new List<Widget>();
            widgets.Add(widget);

            var dashboard1 = new Dashboard()
            {
                UserId = "Raspberry PI",
                Widgets = widgets
            };
            Widget widget2 = new Widget();
            widget2.Type = "test";
            widget2.Query = "test";
            List<Widget> widgets2 = new List<Widget>();
            widgets2.Add(widget2);

            var dashboard2 = new Dashboard()
            {
                UserId = "Raspberry PI2",
                Widgets = widgets2
            };

            dashboardService.CreateDashboard(dashboard1);
            dashboard1.ShouldNotBeNull();
            dashboard1.Id.ShouldNotBeNull();


            dashboardService.CreateDashboard(dashboard2);
            dashboard2.ShouldNotBeNull();
            dashboard2.Id.ShouldNotBeNull();



            var dashboards = dashboardService.GetAllDashboard();

            dashboards.ShouldBe<List<Dashboard>>();
            (dashboards.Count > 0).ShouldBeTrue();
        }


        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.Dashboard")]
        public void Can_get_all_dashboards_by_name()
        {
            Widget widget1 = new Widget();
            widget1.Type = "test";
            widget1.Query = "test";
            List<Widget> widgets1 = new List<Widget>();
            widgets1.Add(widget1);

            var dashboard1 = new Dashboard()
            {
                UserId = "Raspberry PI3",
                Widgets = widgets1
            };

            Widget widget2 = new Widget();
            widget2.Type = "test";
            widget2.Query = "test";
            List<Widget> widgets2 = new List<Widget>();
            widgets2.Add(widget2);

            var dashboard2 = new Dashboard()
            {
                UserId = "Raspberry PI2",
                Widgets = widgets2
            };


            Widget widget3 = new Widget();
            widget3.Type = "test";
            widget3.Query = "test";
            List<Widget> widgets3 = new List<Widget>();
            widgets3.Add(widget3);

            var dashboard3 = new Dashboard()
            {
                UserId = "Arduino",
                Widgets = widgets3
            };

            dashboardService.CreateDashboard(dashboard1);
            dashboard1.ShouldNotBeNull();
            dashboard1.Id.ShouldNotBeNull();


            dashboardService.CreateDashboard(dashboard2);
            dashboard2.ShouldNotBeNull();
            dashboard2.Id.ShouldNotBeNull();

            dashboardService.CreateDashboard(dashboard3);
            dashboard3.ShouldNotBeNull();
            dashboard3.Id.ShouldNotBeNull();

            var dashboards = dashboardService.GetAllDashboard("Arduino");

            dashboards.ShouldBe<List<Dashboard>>();
            //dashboards.Count.ShouldEqual(1);
        }


        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.Dashboard")]
        public void Can_update_dashboard_to_database()
        {
            Widget widget = new Widget();
            widget.Type = "test";
            widget.Query = "test";
            List<Widget> widgets = new List<Widget>();
            widgets.Add(widget);

            var dashboard = new Dashboard()
            {
                UserId = "Raspberry PI",
                Widgets = widgets
            };

            dashboardService.CreateDashboard(dashboard);

            // First insertdashboard to db
            dashboard.ShouldNotBeNull();
            dashboard.Id.ShouldNotBeNull();

            // Update name and send update to db
            dashboard.UserId = "Arduino";
            dashboardService.UdpdateDashboard(dashboard);

            // Get item from db and check if name was updated
            var dashboardFromDb = dashboardService.GetDashboard(dashboard.Id.ToString());
            dashboardFromDb.ShouldNotBeNull();
            dashboardFromDb.UserId.ShouldEqual("Arduino");
        }


        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.Dashboard")]
        public void Can_delete_dashboard_from_database()
        {
            Widget widget = new Widget();
            widget.Type = "test";
            widget.Query = "test";
            List<Widget> widgets = new List<Widget>();
            widgets.Add(widget);

            var dashboard = new Dashboard()
            {
                UserId = "Raspberry PI",
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
            var dashboards = dashboardService.GetAllDashboard();
            foreach (var dashboard in dashboards)
            {
                dashboardService.DeleteDashboard(dashboard.Id.ToString());
            }
        }
    }
}
