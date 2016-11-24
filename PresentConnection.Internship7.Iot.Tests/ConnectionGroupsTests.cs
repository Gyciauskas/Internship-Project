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

        [SetUp]
        public void SetUp()
        {
            connectionGroupService = new ConnectionGroupService();
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.ConnectionGroup")]
        public void Can_insert_connectionGroup_to_database()
        {
            var connectionGroup = new ConnectionGroup()
            {
                UniqueName = "UniqueName",
                Name = "Name",
            };
            connectionGroupService.CreateConnectionGroup(connectionGroup);

            connectionGroup.ShouldNotBeNull();
            connectionGroup.Id.ShouldNotBeNull();
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.ConnectionGroup")]
        public void Cannot_insert_connectionGroup_to_database_when_uniquename_is_not_provided()
        {
            var connectionGroup = new ConnectionGroup()
            {
                UniqueName = "",
                Name = "Name",
            };

            typeof(BusinessException).ShouldBeThrownBy(() => connectionGroupService.CreateConnectionGroup(connectionGroup));
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.ConnectionGroup")]
        public void Cannot_insert_connectionGroup_to_database_when_name_is_not_provided()
        {
            var connectionGroup = new ConnectionGroup()
            {
                UniqueName = "UniqueName",
                Name = "",
            };

            typeof(BusinessException).ShouldBeThrownBy(() => connectionGroupService.CreateConnectionGroup(connectionGroup));
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.ConnectionGroup")]
        public void Can_get_connectionGroup_by_id()
        {
            var connectionGroup = new ConnectionGroup()
            {
                UniqueName = "UniqueName",
                Name = "Name",
            };
            connectionGroupService.CreateConnectionGroup(connectionGroup);

            connectionGroup.ShouldNotBeNull();
            connectionGroup.Id.ShouldNotBeNull();

            var connectionGroupFromDb = connectionGroupService.GetConnectionGroup(connectionGroup.Id.ToString());
            connectionGroupFromDb.ShouldNotBeNull();
            connectionGroupFromDb.Id.ShouldNotBeNull();
            connectionGroupFromDb.UniqueName.ShouldEqual("UniqueName");
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.ConnectionGroup")]
        public void Can_get_all_connectionGroups()
        {
            var connectionGroup1 = new ConnectionGroup()
            {
                UniqueName = "UniqueName1",
                Name = "Name1",
            };

            var connectionGroup2 = new ConnectionGroup()
            {
                UniqueName = "UniqueName2",
                Name = "Name2",
            };

            connectionGroupService.CreateConnectionGroup(connectionGroup1);
            connectionGroup1.ShouldNotBeNull();
            connectionGroup1.Id.ShouldNotBeNull();

            connectionGroupService.CreateConnectionGroup(connectionGroup2);
            connectionGroup2.ShouldNotBeNull();
            connectionGroup2.Id.ShouldNotBeNull();

            var connectionGroups = connectionGroupService.GetAllConnectionGroups();

            connectionGroups.ShouldBe<List<ConnectionGroup>>();
            (connectionGroups.Count > 0).ShouldBeTrue();
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.ConnectionGroup")]
        public void Can_update_connectionGroup_to_database()
        {
            var connectionGroup = new ConnectionGroup()
            {
                UniqueName = "UniqueName",
                Name = "Name",
            };
            connectionGroupService.CreateConnectionGroup(connectionGroup);

            connectionGroup.ShouldNotBeNull();
            connectionGroup.Id.ShouldNotBeNull();

            connectionGroup.UniqueName = "Edited";
            connectionGroupService.UpdateConnectionGroup(connectionGroup);

            var connectionGroupFromDb = connectionGroupService.GetConnectionGroup(connectionGroup.Id.ToString());
            connectionGroupFromDb.ShouldNotBeNull();
            connectionGroupFromDb.UniqueName.ShouldEqual("Edited");
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.ConnectionGroup")]
        public void Can_delete_connectionGroup_from_database()
        {
            var connectionGroup = new ConnectionGroup()
            {
                UniqueName = "UniqueName",
                Name = "Name",
            };
            connectionGroupService.CreateConnectionGroup(connectionGroup);

            connectionGroup.ShouldNotBeNull();
            connectionGroup.Id.ShouldNotBeNull();

            connectionGroupService.DeleteConnectionGroup(connectionGroup.Id.ToString());
            var connectionGroupFromDb = connectionGroupService.GetConnectionGroup(connectionGroup.Id.ToString());

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
