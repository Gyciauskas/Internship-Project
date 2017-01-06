using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using NUnit.Framework;
using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.BusinessImplementation;
using PresentConnection.Internship7.Iot.Domain;
using PresentConnection.Internship7.Iot.Utils;

namespace PresentConnection.Internship7.Iot.Tests
{
    [TestFixture]
    public class ConnectionGroupsTests
    {
        private IConnectionGroupService connectionGroupService;
        private ConnectionGroup goodConnectionGroup;

        [SetUp]
        public void SetUp()
        {
            connectionGroupService = new ConnectionGroupService();
            goodConnectionGroup = new ConnectionGroup
            {
                UniqueName = "raspberry-pi-3",
                Name = "Name",
                Images =
                {
                    "5821dcc11e9f341d4c6d0994"
                }
            };
        }

        [Test]
        [Category("ConnectionGroup")]
        public void Can_insert_connectionGroup_to_database()
        {
            connectionGroupService.CreateConnectionGroup(goodConnectionGroup);

            goodConnectionGroup.ShouldNotBeNull();
            goodConnectionGroup.Id.ShouldNotBeNull();
        }

        [Test]
        [Category("ConnectionGroup")]
        public void Cannot_insert_connectionGroup_to_database_when_uniquename_is_not_provided()
        {
           goodConnectionGroup.UniqueName =string.Empty;

           var exception = typeof(BusinessException).ShouldBeThrownBy(() => connectionGroupService.CreateConnectionGroup(goodConnectionGroup));
           exception.Message.ShouldEqual("Cannot create connection group");
        }

        [Test]
        [Category("ConnectionGroup")]
        public void Cannot_insert_connectionGroup_to_database_when_such_uniquename_exist()
        {
            connectionGroupService.CreateConnectionGroup(goodConnectionGroup);

            var connectionGroup2 = new ConnectionGroup
            {
                UniqueName = "raspberry-pi-3",
                Name = "Name 2",
                Images =
                {
                    "5821dcc11e9f341d4c6d0994"
                }
            };

            var exception = typeof(BusinessException).ShouldBeThrownBy(() => connectionGroupService.CreateConnectionGroup(connectionGroup2));

            var businessException = exception as BusinessException;
            businessException.ShouldNotBeNull();

            businessException?.Errors.SingleOrDefault(error => error.ErrorMessage.Equals("Property  should be unique in database and in correct format !"))
            .ShouldNotBeNull("Received different error message");

            exception.Message.ShouldEqual("Cannot create connection group");
        }

        [Test]
        [Category("ConnectionGroup")]
        public void Cannot_insert_connectionGroup_to_database_when_uniquename_is_not_in_correct_format()
        {
            goodConnectionGroup.UniqueName = "Raspberry PI 3";

            var exception = typeof(BusinessException).ShouldBeThrownBy(() => connectionGroupService.CreateConnectionGroup(goodConnectionGroup));

            var businessException = exception as BusinessException;
            businessException.ShouldNotBeNull();

            businessException?.Errors.SingleOrDefault(error => error.ErrorMessage.Equals("Property  should be unique in database and in correct format !"))
            .ShouldNotBeNull("Received different error message");

            exception.Message.ShouldEqual("Cannot create connection group");
        }

        [Test]
        [Category("ConnectionGroup")]
        public void Cannot_insert_connectionGroup_to_database_when_uniquename_is_not_in_correct_format_unique_name_with_upercases()
        {
            goodConnectionGroup.UniqueName = "raspberry-PI-3";

            var exception = typeof(BusinessException).ShouldBeThrownBy(() => connectionGroupService.CreateConnectionGroup(goodConnectionGroup));

            var businessException = exception as BusinessException;
            businessException.ShouldNotBeNull();

            businessException?.Errors.SingleOrDefault(error => error.ErrorMessage.Equals("Property  should be unique in database and in correct format !"))
            .ShouldNotBeNull("Received different error message");

            exception.Message.ShouldEqual("Cannot create connection group");
        }

        [Test]
        
        [Category("ConnectionGroup")]
        public void Cannot_insert_connectionGroup_to_database_when_name_is_not_provided()
        {
            goodConnectionGroup.Name = string.Empty;

            var exception = typeof(BusinessException).ShouldBeThrownBy(() => connectionGroupService.CreateConnectionGroup(goodConnectionGroup));
            exception.Message.ShouldEqual("Cannot create connection group");
        }

        [Test]
        [Category("ConnectionGroup")]
        public void Cannot_insert_connectionGroup_to_database_when_image_is_not_provided()
        {
            goodConnectionGroup.Images = null;
            var exception = typeof(BusinessException).ShouldBeThrownBy(() => connectionGroupService.CreateConnectionGroup(goodConnectionGroup));

            var businessException = exception as BusinessException;
            businessException.ShouldNotBeNull();
            businessException?.Errors.SingleOrDefault(error => error.ErrorMessage.Equals("Property Images should contain at least one item!"))
            .ShouldNotBeNull("Received different error message");

            exception.Message.ShouldEqual("Cannot create connection group");
        }

        [Test]
        [Category("ConnectionGroup")]
        public void Can_get_connectionGroup_by_id()
        {
            connectionGroupService.CreateConnectionGroup(goodConnectionGroup);

            goodConnectionGroup.ShouldNotBeNull();
            goodConnectionGroup.Id.ShouldNotBeNull();

            var connectionGroupFromDb = connectionGroupService.GetConnectionGroup(goodConnectionGroup.Id.ToString());
            connectionGroupFromDb.ShouldNotBeNull();
            connectionGroupFromDb.Id.ShouldNotBeNull();
            connectionGroupFromDb.UniqueName.ShouldEqual("raspberry-pi-3");
        }

        [Test]
        [Category("ConnectionGroup")]
        public void Can_get_all_connectionGroups()
        {
            connectionGroupService.CreateConnectionGroup(goodConnectionGroup);

            var connectionGroup2 = new ConnectionGroup
            {
                UniqueName = "raspberry-pi-4",
                Name = "Dropbox",
                Images =
                {
                    "5821dcc11e9f341d4c6d0994"
                },
            };

            connectionGroupService.CreateConnectionGroup(connectionGroup2);
            connectionGroup2.ShouldNotBeNull();
            connectionGroup2.Id.ShouldNotBeNull();

            var connectionGroups = connectionGroupService.GetAllConnectionGroups();

            connectionGroups.ShouldBe<List<ConnectionGroup>>();
            (connectionGroups.Count > 0).ShouldBeTrue();
        }

        [Test]
        [Category("ConnectionGroup")]
        public void Can_update_connectionGroup_to_database()
        {
            connectionGroupService.CreateConnectionGroup(goodConnectionGroup);

            goodConnectionGroup.ShouldNotBeNull();
            goodConnectionGroup.Id.ShouldNotBeNull();

            goodConnectionGroup.UniqueName = "raspberry-pi-4";
            connectionGroupService.UpdateConnectionGroup(goodConnectionGroup);

            var connectionGroupFromDb = connectionGroupService.GetConnectionGroup(goodConnectionGroup.Id.ToString());
            connectionGroupFromDb.ShouldNotBeNull();
            connectionGroupFromDb.UniqueName.ShouldEqual("raspberry-pi-4");
        }

        [Test]
        [Category("ConnectionGroup")]
        public void Can_delete_connectionGroup_from_database()
        {
           
            connectionGroupService.CreateConnectionGroup(goodConnectionGroup);

            goodConnectionGroup.ShouldNotBeNull();
            goodConnectionGroup.Id.ShouldNotBeNull();

            connectionGroupService.DeleteConnectionGroup(goodConnectionGroup.Id.ToString());
            var connectionGroupFromDb = connectionGroupService.GetConnectionGroup(goodConnectionGroup.Id.ToString());

            connectionGroupFromDb.ShouldNotBeNull();
            connectionGroupFromDb.Id.ShouldEqual(ObjectId.Empty);
        }

        [TearDown]
        public void Dispose()
        {
            var connectionGroups = connectionGroupService.GetAllConnectionGroups();
            foreach (var connectionGroup in connectionGroups)
            {
                connectionGroupService.DeleteConnectionGroup(connectionGroup.Id.ToString());
            }
        }
    }
}
