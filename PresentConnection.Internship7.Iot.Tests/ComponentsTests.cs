using MongoDB.Bson;
using NUnit.Framework;
using System.Linq;
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
        private Component goodComponent;

        [SetUp]
        public void SetUp()
        {
            componentService = new ComponentService();
            goodComponent = new Component
            {
                ModelName = "Raspberry PI 3",
                UniqueName = "raspberry-pi-3",
                Images =
                {
                    "5821dcc11e9f341d4c6d0994"
                }
            };

        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.Component")]
        public void Can_insert_component_to_database()
        {
            componentService.CreateComponent(goodComponent);

            goodComponent.ShouldNotBeNull();
            goodComponent.Id.ShouldNotBeNull();
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.Componet")]
        public void Cannot_insert_component_to_database_when_modelname_is_not_provided()
        {
            goodComponent.ModelName = string.Empty;

            var exception = typeof(BusinessException).ShouldBeThrownBy(() => componentService.CreateComponent(goodComponent));
            exception.Message.ShouldEqual("Cannot create component");
        }
        
        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.Component")]
        public void Cannot_insert_component_to_database_when_uniquename_is_not_provided()
        {
            goodComponent.UniqueName = string.Empty;

            var exception = typeof(BusinessException).ShouldBeThrownBy(() => componentService.CreateComponent(goodComponent));
            exception.Message.ShouldEqual("Cannot create component");
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.Component")]
        public void Cannot_insert_component_to_database_when_image_is_not_provided()
        {
            goodComponent.Images = null;
            var exception = typeof(BusinessException).ShouldBeThrownBy(() => componentService.CreateComponent(goodComponent));

            var businessException = exception as BusinessException;
            businessException.ShouldNotBeNull();
            businessException?.Errors.SingleOrDefault(error => error.ErrorMessage.Equals("Property Images should contain at least one item!"))
            .ShouldNotBeNull("Received different error message");

            exception.Message.ShouldEqual("Cannot create component");
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.Component")]
        public void Cannot_insert_component_to_database_when_such_unique_name_exist()
        {
            componentService.CreateComponent(goodComponent);

            var component2 = new Component
            {
                ModelName = "Component 2",
                UniqueName = "raspberry-pi-3",
                Images =
                {
                    "5821dcc11e9f341d4c6d0994"
                }
            };

            var exception = typeof(BusinessException).ShouldBeThrownBy(() => componentService.CreateComponent(component2));

            var businessException = exception as BusinessException;
            businessException.ShouldNotBeNull();

            businessException?.Errors.SingleOrDefault(error => error.ErrorMessage.Equals("Property  should be unique in database and in correct format !"))
            .ShouldNotBeNull("Received different error message");

            exception.Message.ShouldEqual("Cannot create component");
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.Component")]
        public void Cannot_insert_component_to_database_when_uniquename_is_not_in_correct_format()
        {
            goodComponent.UniqueName = "Raspberry PI 3";

            var exception = typeof(BusinessException).ShouldBeThrownBy(() => componentService.CreateComponent(goodComponent));

            var businessException = exception as BusinessException;
            businessException.ShouldNotBeNull();

            businessException?.Errors.SingleOrDefault(error => error.ErrorMessage.Equals("Property  should be unique in database and in correct format !"))
            .ShouldNotBeNull("Received different error message");

            exception.Message.ShouldEqual("Cannot create component");
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.Component")]
        public void Cannot_insert_component_to_database_when_uniquename_is_not_in_correct_format_unique_name_with_upercases()
        {
            goodComponent.UniqueName = "raspberry-PI-3";

            var exception = typeof(BusinessException).ShouldBeThrownBy(() => componentService.CreateComponent(goodComponent));

            var businessException = exception as BusinessException;
            businessException.ShouldNotBeNull();

            businessException?.Errors.SingleOrDefault(error => error.ErrorMessage.Equals("Property  should be unique in database and in correct format !"))
            .ShouldNotBeNull("Received different error message");

            exception.Message.ShouldEqual("Cannot create component");
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.Component")]
        public void Can_get_component_by_id()
        {
            componentService.CreateComponent(goodComponent);

            goodComponent.ShouldNotBeNull();
            goodComponent.Id.ShouldNotBeNull();

            var componentFromDb = componentService.GetComponent(goodComponent.Id.ToString());
            componentFromDb.ShouldNotBeNull();
            componentFromDb.Id.ShouldNotBeNull();
            componentFromDb.ModelName.ShouldEqual("Raspberry PI 3");
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.Component")]
        public void Can_get_all_components()
        {
            componentService.CreateComponent(goodComponent);

            var component2 = new Component
            {
                ModelName = "Component 2",
                UniqueName = "component-2",
                Images =
                {
                    "5821dcc11e9f341d4c6d0994"
                }
            };

            componentService.CreateComponent(component2);
            component2.ShouldNotBeNull();
            component2.Id.ShouldNotBeNull();

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
                ModelName = "Raspberry PI 3",
                UniqueName = "raspberry-pi-3",
                Images =
                {
                    "5821dcc11e9f341d4c6d0994"
                }
            };

            var component2 = new Component
            {
                ModelName = "Component 2",
                UniqueName = "component-2",
                Images =
                {
                    "5821dcc11e9f341d4c6d0994"
                }
            };

            var component3 = new Component
            {
                ModelName = "Component 3",
                UniqueName = "component-3",
                Images =
                {
                    "5821dcc11e9f341d4c6d0994"
                }
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

            var components = componentService.GetAllComponents("Component 3");

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
                ModelName = "Raspberry PI 3",
                UniqueName = "raspberry-pi-3",
                Images =
                {
                    "5821dcc11e9f341d4c6d0994"
                }
            };

            componentService.CreateComponent(component);
            component.ShouldNotBeNull();
            component.Id.ShouldNotBeNull();

            component.ModelName = "Video";
            componentService.UpdateComponent(component);

            var componentFromDb = componentService.GetComponent(component.Id.ToString());
            componentFromDb.ShouldNotBeNull();
            componentFromDb.ModelName.ShouldEqual("Video");
        }
        
        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.Component")]
        public void Cannot_update_component_to_database_when_such_unique_name_already_exist()
        {
            componentService.CreateComponent(goodComponent);

            var component = new Component
            {
                ModelName = "Raspberry PI 3",
                UniqueName = "raspberry-pi-2",
                Images =
                {
                    "5821dcc11e9f341d4c6d0994"
                }
            };
            componentService.CreateComponent(component);

            component.ShouldNotBeNull();
            component.Id.ShouldNotBeNull();

            component.UniqueName = "raspberry-pi-3";

            var exception = typeof(BusinessException).ShouldBeThrownBy(() => componentService.UpdateComponent(component));

            var businessException = exception as BusinessException;
            businessException.ShouldNotBeNull();

            businessException?.Errors.SingleOrDefault(error => error.ErrorMessage.Equals("Property  should be unique in database and in correct format !"))
            .ShouldNotBeNull("Received different error message");

            exception.Message.ShouldEqual("Cannot update component");
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.Component")]
        public void Can_delete_component_from_database()
        {
            componentService.CreateComponent(goodComponent);

            goodComponent.ShouldNotBeNull();
            goodComponent.Id.ShouldNotBeNull();

            componentService.DeleteComponent(goodComponent.Id.ToString());
            var componentFromDb = componentService.GetComponent(goodComponent.Id.ToString());

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

