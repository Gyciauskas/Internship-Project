using MongoDB.Bson;
using NSubstitute;
using NUnit.Framework;
using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.BusinessImplementation;
using PresentConnection.Internship7.Iot.Domain;
using PresentConnection.Internship7.Iot.ServiceModels;
using ServiceStack;
using System.Collections.Generic;
using System.Linq;

namespace PresentConnection.Internship7.Iot.Tests
{
    [TestFixture]
    public partial class CollaboratorTests : TestsBase
    {
        private ServiceStackHost appHost;
        private ICollaboratorService collaboratorService;
        private Collaborator goodCollaborator;
        private ICollaboratorService collaboratorServiceMock;

        private void SetupMocks()
        {
            collaboratorServiceMock = Substitute.For<ICollaboratorService>();

            // when pass any parameter (Id) to collaboratorService.GetCollaborator, return goodCollaborator with passed id
            collaboratorServiceMock.GetCollaborator(Arg.Any<string>()).Returns(info =>
            {
                goodCollaborator.Id = ObjectId.Parse(info.ArgAt<string>(0));
                return goodCollaborator;
            });

            // when we are trying get collaborators, just return generated list
            collaboratorServiceMock.GetAllCollaborators(string.Empty).Returns(info =>
            {
                var list = new List<Collaborator>();

                goodCollaborator.Id = ObjectId.GenerateNewId();
                list.Add(goodCollaborator);

                goodCollaborator.Id = ObjectId.GenerateNewId();
                goodCollaborator.Name = "Object1";
                list.Add(goodCollaborator);

                goodCollaborator.Id = ObjectId.GenerateNewId();
                goodCollaborator.Name = "Object2";
                list.Add(goodCollaborator);

                return list;
            });

            // when we are trying get collaborators by name, just generate list and filter by name
            collaboratorServiceMock.GetAllCollaborators(Arg.Any<string>()).Returns(info =>
            {
                var list = new List<Collaborator>();

                goodCollaborator.Id = ObjectId.GenerateNewId();
                list.Add(goodCollaborator);

                goodCollaborator.Id = ObjectId.GenerateNewId();
                goodCollaborator.Name = "Object1";
                list.Add(goodCollaborator);

                goodCollaborator.Id = ObjectId.GenerateNewId();
                goodCollaborator.Name = "Object2";
                list.Add(goodCollaborator);

                return list.Where(x => x.Name.Contains(info.ArgAt<string>(0))).ToList();
            });
        }

        [SetUp]
        public void SetUp()
        {
            //Start your AppHost on TestFixtureSetUp
            appHost = new AppHost()
                .Init()
                .Start(BaseUri);

            collaboratorService = new CollaboratorService();

            goodCollaborator = new Collaborator
            {
                UserId = "125",
                Email = "TestName125@gmail.com",
                Name = "Some Name"
            };

            SetupMocks();
        }

        [Test]
        [Category("Collaborator")]
        public void Can_do_CRUD_collaborator_to_database()
        {
            var client = new JsonServiceClient(BaseUri);

            // Create request
            var createRequest = new CreateCollaborator
            {
                UserId = goodCollaborator.UserId,
                Email = goodCollaborator.Email,
                Name = goodCollaborator.Name
            };

            var createCollaboratorResponse = client.Post(createRequest);
            createCollaboratorResponse.ShouldNotBeNull();
            createCollaboratorResponse.Result.ShouldNotBeNull();
            createCollaboratorResponse.Result.Id.ShouldNotBeNull();
            createCollaboratorResponse.Result.Id.ShouldBeNotBeTheSameAs(ObjectId.Empty);

            var createRequest2 = new CreateCollaborator
            {
                UserId = "123abc",
                Email = "collaborators@gmail.com",
                Name = "Other name"
            };

            createCollaboratorResponse = client.Post(createRequest2);
            createCollaboratorResponse.ShouldNotBeNull();
            createCollaboratorResponse.Result.ShouldNotBeNull();
            createCollaboratorResponse.Result.Id.ShouldNotBeNull();
            createCollaboratorResponse.Result.Id.ShouldBeNotBeTheSameAs(ObjectId.Empty);

            // Get all
            var getCollaboratorsRequest = new GetCollaborators
            {
                Name = string.Empty
            };

            var getCollaboratorsResponse = client.Get(getCollaboratorsRequest);
            getCollaboratorsResponse.ShouldNotBeNull();
            getCollaboratorsResponse.Result.ShouldNotBeNull();
            getCollaboratorsResponse.Result.Count.ShouldEqual(2);

            // Get all by name
            var getCollaboratorsByNameRequest = new GetCollaborators
            {
                Name = goodCollaborator.Name
            };

            var getCollaboratorsByNameResponse = client.Get(getCollaboratorsByNameRequest);
            getCollaboratorsByNameResponse.ShouldNotBeNull();
            getCollaboratorsByNameResponse.Result.ShouldNotBeNull();
            getCollaboratorsByNameResponse.Result.Count.ShouldEqual(1);

            // Update
            var UpdateCollaboratorRequest = new UpdateCollaborator
            {
                Id = createCollaboratorResponse.Result.Id.ToString(),
                UserId = createCollaboratorResponse.Result.UserId + "Updated",
                Email = createCollaboratorResponse.Result.Email + "-Updated"
            };

            var updateCollaboratorResponse = client.Put(UpdateCollaboratorRequest);
            updateCollaboratorResponse.ShouldNotBeNull();
            updateCollaboratorResponse.Result.ShouldNotBeNull();

            // Get by Id
            var getCollaboratorById = new GetCollaborator
            {
                Id = updateCollaboratorResponse.Result.Id.ToString()
            };

            var getCollaboratorByIdResponse = client.Get(getCollaboratorById);
            getCollaboratorByIdResponse.ShouldNotBeNull();
            getCollaboratorByIdResponse.Result.ShouldNotBeNull();
            getCollaboratorByIdResponse.Result.UserId.ShouldEqual(createCollaboratorResponse.Result.UserId + "Updated");
            getCollaboratorByIdResponse.Result.Email.ShouldEqual(createCollaboratorResponse.Result.Email + "-Updated");

            // Delete 
            var deleteRequest = new DeleteCollaborator
            {
                Id = getCollaboratorByIdResponse.Result.Id.ToString()
            };

            var deleteRequestResponse = client.Delete(deleteRequest);
            deleteRequestResponse.ShouldNotBeNull();
            deleteRequestResponse.Result.ShouldNotBeNull();
            deleteRequestResponse.Result.ShouldEqual(true);
        }

        [TearDown]
        public void Dispose()
        {
            appHost.Dispose();

            var collaborators = collaboratorService.GetAllCollaborators();
            foreach (var collaborator in collaborators)
            {
                collaboratorService.DeleteCollaborator(collaborator.Id.ToString());
            }
        }
    }
}
