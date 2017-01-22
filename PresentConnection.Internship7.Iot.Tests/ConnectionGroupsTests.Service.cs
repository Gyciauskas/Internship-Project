using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using MongoDB.Bson;
using NSubstitute;
using NSubstitute.Core.Arguments;
using NUnit.Framework;
using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.BusinessImplementation;
using PresentConnection.Internship7.Iot.Domain;
using PresentConnection.Internship7.Iot.ServiceModels;
using PresentConnection.Internship7.Iot.Services;
using ServiceStack;
using CodeMash.Net;

namespace PresentConnection.Internship7.Iot.Tests
{
    [TestFixture]
    public partial class ConnectionGroupsTests : TestsBase
    {
        private ServiceStackHost appHost;
        private IConnectionGroupService connectionGroupService;
        private ConnectionGroup goodConnectionGroup;
        private IConnectionGroupService connectionGroupServiceMock;

        private void SetupMocks()
        {
            connectionGroupServiceMock = Substitute.For<IConnectionGroupService>();

            // when pass any parameter (Id) to connectionGroupService.GetConnectionGroup, return goodConnectionGroup with passed id
            connectionGroupServiceMock.GetConnectionGroup(Arg.Any<string>()).Returns(info =>
            {
                goodConnectionGroup.Id = ObjectId.Parse(info.ArgAt<string>(0));
                return goodConnectionGroup;
            });

            // when we are trying get connectionGroups, just return generated list
            connectionGroupServiceMock.GetAllConnectionGroups().Returns(info =>
            {
                var list = new List<ConnectionGroup>();

                goodConnectionGroup.Id = ObjectId.GenerateNewId();
                list.Add(goodConnectionGroup);

                var connectionGroup2 = new ConnectionGroup
                {
                    UniqueName = "raspberry-pi3",
                    Name = "Name",
                    Images =
                {
                    "5821dcc11e9f341d4c6d0994"
                }
                };
                connectionGroup2.Id = ObjectId.GenerateNewId();
                connectionGroup2.Name = "Object1";
                list.Add(connectionGroup2);

                var connectionGroup3 = new ConnectionGroup
                {
                    UniqueName = "raspberry-pi4",
                    Name = "Name1",
                    Images =
                {
                    "5821dcc11e9f341d4c6d0994"
                }
                };
                connectionGroup3.Id = ObjectId.GenerateNewId();
                connectionGroup3.Name = "Object2";
                list.Add(connectionGroup2);

                return list;
            });
        }

        [SetUp]
        public void SetUp()
        {
            //Start your AppHost on TestFixtureSetUp
            appHost = new AppHost()
                .Init()
                .Start(BaseUri);

            connectionGroupService = new ConnectionGroupService();

            // Create recipe connection
            goodConnectionGroup = new ConnectionGroup
            {
                UniqueName = "raspberry-pi3",
                Name = "Name",
                Images =
                {
                    "5821dcc11e9f341d4c6d0994"
                }
            };

            SetupMocks();
        }

        [Test]
        [Category("ConnectionGroup")]
        public void Can_do_CRUD_connectionGroup_to_database()
        {
            var client = new JsonServiceClient(BaseUri);

            // Create request
            var createRequest = new CreateRecipeConnnection
            {
                Name = goodConnectionGroup.Name,
                UniqueName = goodConnectionGroup.UniqueName,
            };

            var createConnectionGroupResponse = client.Post(createRequest);
            createConnectionGroupResponse.ShouldNotBeNull();
            createConnectionGroupResponse.Result.ShouldNotBeNull();
            createConnectionGroupResponse.Result.Id.ShouldNotBeNull();
            createConnectionGroupResponse.Result.Id.ShouldBeNotBeTheSameAs(ObjectId.Empty);

            var createRequest2 = new CreateRecipeConnnection
            {
                Name = goodConnectionGroup.Name + "2",
                UniqueName = goodConnectionGroup.UniqueName + "-2",
            };

            var createConnectionGroupResponse2 = client.Post(createRequest2);
            createConnectionGroupResponse2.ShouldNotBeNull();
            createConnectionGroupResponse2.Result.ShouldNotBeNull();
            createConnectionGroupResponse2.Result.Id.ShouldNotBeNull();
            createConnectionGroupResponse2.Result.Id.ShouldBeNotBeTheSameAs(ObjectId.Empty);

            // Get all
            var getRequests = new GetConnectionGroups
            {
            };

            var getConnectionGroupsResponse = client.Get(getRequests);
            getConnectionGroupsResponse.ShouldNotBeNull();
            getConnectionGroupsResponse.Result.ShouldNotBeNull();
            getConnectionGroupsResponse.Result.Count.ShouldEqual(2);

            // Update
            var updateRequest = new UpdateConnectionGroup
            {
                Id = createConnectionGroupResponse.Result.Id.ToString(),
                Name = createConnectionGroupResponse.Result.Name + "Updated",
                UniqueName = createConnectionGroupResponse.Result.UniqueName + "-updated"
            };

            var updateResponse = client.Put(updateRequest);
            updateResponse.ShouldNotBeNull();
            updateResponse.Result.ShouldNotBeNull();

            // Get by Id
            var getByIdRequest = new GetConnectionGroup
            {
                Id = createConnectionGroupResponse.Result.Id.ToString()
            };

            var getByIdResponse = client.Get(getByIdRequest);
            getByIdResponse.ShouldNotBeNull();
            getByIdResponse.Result.ShouldNotBeNull();
            getByIdResponse.Result.UniqueName.ShouldEqual(createConnectionGroupResponse.Result.UniqueName + "-updated");
            getByIdResponse.Result.Name.ShouldEqual(createConnectionGroupResponse.Result.Name + "Updated");

            // Delete
            var deleteRequest = new DeleteConnectionGroup
            {
                Id = createConnectionGroupResponse.Result.Id.ToString()
            };

            var deleteResponse = client.Delete(deleteRequest);
            deleteResponse.ShouldNotBeNull();
            deleteResponse.Result.ShouldNotBeNull();
            deleteResponse.Result.ShouldEqual(true);
        }

        // After every test, delete all records from db
        [TearDown]
        public void Dispose()
        {
            appHost.Dispose();

            var connectionGroups = Db.Find<ConnectionGroup>(_ => true);

            foreach (var connectionGroup in connectionGroups)
            {
                Db.DeleteOne<ConnectionGroup>(x => x.Id == connectionGroup.Id);
            }
        }
    }
}