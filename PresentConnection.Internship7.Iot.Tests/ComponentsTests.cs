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
    public class ComponentsTests
    {
        private IComponentService componentService;

        [SetUp]
        public void SetUp()
        {

            componentService = new ComponentService();
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.Component")]

    
        public void Can_insert_component_to_database()
        {

            var component = new Component
            {
                ModelName = "WiFi Adapter",
                UniqueName = "raspberry-pi-adapter",
                       
            };

            componentService.CreateComponent(component);

            component.ShouldNotBeNull();
            component.Id.ShouldNotBeNull();
        }


        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.Componet")]

        public void Cannot_insert_component_to_database_when_modelname_is_not_provided()
        {

            var component = new Component
            {
                ModelName = "",
                UniqueName = "raspberry-pi-adapter",
             };

            typeof(BusinessException).ShouldBeThrownBy(() => componentService.CreateComponent(component));
        }


        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.Component")]

        public void Cannot_insert_component_to_database_when_uniquename_is_not_provided()
        {
            var component = new Component
            {
                ModelName = "WiFi Adapter",
                UniqueName = "",
            };

            typeof(BusinessException).ShouldBeThrownBy(() => componentService.CreateComponent(component));
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.Component")]
        public void Can_get_component_by_id()
        {

            var component = new Component
            {
                ModelName = "WiFi Adapter",
                UniqueName = "raspberry-pi-adapter",
             };
            componentService.CreateComponent(component);

            component.ShouldNotBeNull();
            component.Id.ShouldNotBeNull();

            var componentFromDb = componentService.GetComponent(component.Id.ToString());
            componentFromDb.ShouldNotBeNull();
            componentFromDb.Id.ShouldNotBeNull();
            componentFromDb.ModelName.ShouldEqual("WiFi Adapter");
        }


        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.Component")]
        public void Can_get_all_components()
        {
            var component = new Component
            {
                ModelName = "WiFi Adapter",
                UniqueName = "raspberry-pi-adapter",
            };

            var component1 = new Component
            {
                ModelName = "WiFi Adapter",
                UniqueName = "raspberry-pi-adapter",
            };


            componentService.CreateComponent(component);
            component.ShouldNotBeNull();
            component.Id.ShouldNotBeNull();

            componentService.CreateComponent(component1);
            component1.ShouldNotBeNull();
            component1.Id.ShouldNotBeNull();

            var components = componentService.GetAllComponents();

            components.ShouldBe<List<Component>>();
            (components.Count > 0).ShouldBeTrue();
        }


        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.Component")]
        public void Can_get_all_components_by_name()
        {
            var component1 = new Component
            {
                ModelName = "WiFi Adapter",
                UniqueName = "raspberry-pi-adapter",
            };

            var component2 = new Component
            {
                ModelName = "Display",
                UniqueName = "raspberry-pi-adapter",
            };

            var component3 = new Component
            {
                ModelName = "Camera",
                UniqueName = "raspberry-pi-adapter",
            };


            componentService.CreateComponent(component1);
            component1.ShouldNotBeNull();
            component1.Id.ShouldNotBeNull();

            componentService.CreateComponent(component2);
            component2.ShouldNotBeNull();
            component2.Id.ShouldNotBeNull();

            componentService.CreateComponent(component3);
            component3.ShouldNotBeNull();
            component3.Id.ShouldNotBeNull();

            var components = componentService.GetAllComponents("Display");

            components.ShouldBe<List<Component>>();
            components.Count.ShouldEqual(1);
        }


        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.Component")]
        public void Can_update_component_to_database()
        {
           
            var component = new Component
            {
                ModelName = "WiFi Adapter",
                UniqueName = "raspberry-pi-adapter",
             };

            componentService.CreateComponent(component);

            // First insert component to db
            component.ShouldNotBeNull();
            component.Id.ShouldNotBeNull();

            // Update name and send update to db
            component.ModelName = "Video";
            componentService.UpdateComponent(component);

            // Get item from db and check if name was updated
            var componentFromDb = componentService.GetComponent(component.Id.ToString());
            componentFromDb.ShouldNotBeNull();
            componentFromDb.ModelName.ShouldEqual("Video");
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.Component")]
        public void Can_delete_component_from_database()
        {

           var component = new Component
            {
                ModelName = "WiFi Adapter",
                UniqueName = "raspberry-pi-adapter",
            };

            componentService.CreateComponent(component);

            // First insert component to db
            component.ShouldNotBeNull();
            component.Id.ShouldNotBeNull();

            // Delete component from db
            componentService.DeleteComponent(component.Id.ToString());

            // Get item from db and check if name was updated
            var componentFromDb = componentService.GetComponent(component.Id.ToString());

            // issue with CodeMash library. Should return null when not found - not default object
            componentFromDb.ShouldNotBeNull();
            componentFromDb.Id.ShouldEqual(ObjectId.Empty);
        }


        [TearDown]
        public void Dispose()
        {
            var components = componentService.GetAllComponents();
            foreach (var component in components)
            {
                componentService.DeleteComponent(component.Id.ToString());
            }
        }

    }
}

