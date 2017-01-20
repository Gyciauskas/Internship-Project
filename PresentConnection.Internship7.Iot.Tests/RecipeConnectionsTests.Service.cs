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
using System.IO;
using System.Configuration;
using System;

namespace PresentConnection.Internship7.Iot.Tests
{
    [TestFixture]
    public partial class RecipeConnectionsTests : TestsBase
    {
        private ServiceStackHost appHost;
        private IRecipeConnectionService recipeConnectionService;
        private IRecipeConnectionService recipeConnectionServiceMock;

        private RecipeConnection goodRecipeConnection;
        private string testImagePath;
        private byte[] imageBytes;
        private string imageDir;

        private void SetupMocks()
        {
            recipeConnectionServiceMock = Substitute.For<IRecipeConnectionService>();

            // when pass any parameter (Id) to recipeConnectionService.GetRecipeConnection, return goodRecipeConnection with passed id
            recipeConnectionServiceMock.GetRecipeConnection(Arg.Any<string>()).Returns(info =>
            {
                goodRecipeConnection.Id = ObjectId.Parse(info.ArgAt<string>(0));
                return goodRecipeConnection;
            });

            // when we are trying get recipeConnections, just return generated list
            recipeConnectionServiceMock.GetAllRecipeConnections(string.Empty).Returns(info =>
            {
                var list = new List<RecipeConnection>();

                goodRecipeConnection.Id = ObjectId.GenerateNewId();
                list.Add(goodRecipeConnection);

                goodRecipeConnection.Id = ObjectId.GenerateNewId();
                goodRecipeConnection.Name = "Object1";
                list.Add(goodRecipeConnection);

                goodRecipeConnection.Id = ObjectId.GenerateNewId();
                goodRecipeConnection.Name = "Object2";
                list.Add(goodRecipeConnection);

                return list;
            });

            // when we are trying get recipeConnections by name, just generate list and filter by name
            recipeConnectionServiceMock.GetAllRecipeConnections(Arg.Any<string>()).Returns(info =>
            {
                var list = new List<RecipeConnection>();

                goodRecipeConnection.Id = ObjectId.GenerateNewId();
                list.Add(goodRecipeConnection);

                goodRecipeConnection.Id = ObjectId.GenerateNewId();
                goodRecipeConnection.Name = "Object1";
                list.Add(goodRecipeConnection);

                goodRecipeConnection.Id = ObjectId.GenerateNewId();
                goodRecipeConnection.Name = "Object2";
                list.Add(goodRecipeConnection);

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

            recipeConnectionService = new RecipeConnectionService();

            // Create recipe connection
            goodRecipeConnection = new RecipeConnection
            {
                Name = "abc",
                UniqueName = "abc"
            };

            // This maked because of uploaded picture to tests project, Images folder.
            string projectPath = Environment.CurrentDirectory;
            testImagePath = Directory.EnumerateFiles(projectPath + "\\PresentConnection.Internship7.Iot.Tests\\Images".MapHostAbsolutePath()).Last();
            imageDir = ConfigurationManager.AppSettings["ImagesPath"].MapHostAbsolutePath();
            imageBytes = File.ReadAllBytes(testImagePath);

            SetupMocks();
        }

        [Test]
        [Category("RecipeConnection")]
        public void Can_do_CRUD_recipeConnection_to_database()
        {
            var client = new JsonServiceClient(BaseUri);

            // Create request
            var createRequest = new CreateRecipeConnnection
            {
                Name = goodRecipeConnection.Name,
                FileName = "computer-case-fan.png",
                Image = imageBytes
            };

            var createRecipeConnectionResponse = client.Post(createRequest);
            createRecipeConnectionResponse.ShouldNotBeNull();
            createRecipeConnectionResponse.Result.ShouldNotBeNull();
            createRecipeConnectionResponse.Result.ShouldBeNotBeTheSameAs(ObjectId.Empty);

            var createRequest2 = new CreateRecipeConnnection
            {
                Name = goodRecipeConnection.Name + "2",
                FileName = "computer-case-fan.png",
                Image = imageBytes
            };

            var createRecipeConnectionResponse2 = client.Post(createRequest2);
            createRecipeConnectionResponse2.ShouldNotBeNull();
            createRecipeConnectionResponse2.Result.ShouldNotBeNull();
            createRecipeConnectionResponse2.Result.ShouldBeNotBeTheSameAs(ObjectId.Empty);

            // Get all
            var getRequests = new GetRecipeConnections
            {
                Name = string.Empty
            };

            var getRecipeConnectionsResponse = client.Get(getRequests);
            getRecipeConnectionsResponse.ShouldNotBeNull();
            getRecipeConnectionsResponse.Result.ShouldNotBeNull();
            getRecipeConnectionsResponse.Result.Count.ShouldEqual(2);

            // Get by name
            var getByNameRequest = new GetRecipeConnections
            {
                Name = goodRecipeConnection.Name + "2"
            };

            var getByNameResponse = client.Get(getByNameRequest);
            getByNameResponse.ShouldNotBeNull();
            getByNameResponse.Result.ShouldNotBeNull();
            getByNameResponse.Result.Count.ShouldEqual(1);

            // Update
            var updateRequest = new UpdateRecipeConnection
            {
                Name = createRequest.Name + "+Updated",
                UniqueName = createRequest.Name.Trim().ToLower() + "-updated",
                Id = createRecipeConnectionResponse.Result,
            };

            var updateResponse = client.Put(updateRequest);
            updateResponse.ShouldNotBeNull();
            updateResponse.Result.ShouldNotBeNull();

            // Get by Id
            var getByIdRequest = new GetRecipeConnection
            {
                Id = createRecipeConnectionResponse.Result
            };

            var getByIdResponse = client.Get(getByIdRequest);
            getByIdResponse.ShouldNotBeNull();
            getByIdResponse.Result.ShouldNotBeNull();
            getByIdResponse.Result.UniqueName.ShouldEqual(createRequest.Name.Trim().ToLower() + "-updated");
            getByIdResponse.Result.Name.ShouldEqual(createRequest.Name + "+Updated");

            // Delete
            var deleteRequest = new DeleteRecipeConnection
            {
                Id = createRecipeConnectionResponse.Result
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

            var recipeConnections = Db.Find<RecipeConnection>(_ => true);

            foreach (var recipeConnection in recipeConnections)
            {
                Db.DeleteOne<RecipeConnection>(x => x.Id == recipeConnection.Id);
            }
        }
    }
}
