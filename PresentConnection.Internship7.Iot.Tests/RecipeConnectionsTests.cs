using CodeMash.Net;
using MongoDB.Bson;
using NUnit.Framework;
using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.BusinessImplementation;
using PresentConnection.Internship7.Iot.Domain;
using PresentConnection.Internship7.Iot.Utils;
using System.Collections.Generic;
using System.Linq;

namespace PresentConnection.Internship7.Iot.Tests
{
    [TestFixture]
    public class RecipeConnectionsTests
    {
        private IRecipeConnectionService recipeconnService;
        private RecipeConnection recipeconnection;

        [SetUp]
        public void SetUp()
        {
            recipeconnService = new RecipeConnectionService();

            // Create recipe connection
            recipeconnection = new RecipeConnection
            {
                Name = "abc",
                UniqueName = "123"
            };
        }

        [Test]
        [Category("IntegrationTests")]
        [Category("RecipeConnection")]
        public void Can_insert_recipe_connection_to_database()
        {
            // Insert to db
            recipeconnService.CreateRecipeConnection(recipeconnection);

            // test result, if passed, than means validated successfully
            recipeconnService.ShouldNotBeNull();
        }

        [Test]
        [Category("IntegrationTests")]
        [Category("RecipeConnection")]
        public void Cannot_insert_recipe_connection_to_database_when_name_is_not_provided()
        {
            recipeconnection.Name = string.Empty;

            var exception = typeof(BusinessException).ShouldBeThrownBy(() => recipeconnService.CreateRecipeConnection(recipeconnection));
            exception.Message.ShouldEqual("Couldn't create recipe connection");
        }

        [Test]
        [Category("IntegrationTests")]
        [Category("RecipeConnection")]
        public void Cannot_insert_recipe_connection_to_database_when_uniquename_is_not_provided()
        {
            recipeconnection.UniqueName = string.Empty;

            var exception = typeof(BusinessException).ShouldBeThrownBy(() => recipeconnService.CreateRecipeConnection(recipeconnection));
            exception.Message.ShouldEqual("Couldn't create recipe connection");
        }

        [Test]
        [Category("IntegrationTests")]
        [Category("RecipeConnection")]
        public void Cannot_insert_recipe_connection_to_database_when_such_uniquename_exist()
        {
            recipeconnService.CreateRecipeConnection(recipeconnection);

            var recipeconnection2 = new RecipeConnection
            {
                UniqueName = "123",
                Name = "abc",
            };

            var exception = typeof(BusinessException).ShouldBeThrownBy(() => recipeconnService.CreateRecipeConnection(recipeconnection2));

            var businessException = exception as BusinessException;
            businessException.ShouldNotBeNull();

            businessException?.Errors.SingleOrDefault(error => error.ErrorMessage.Equals("Property  should be unique in database and in correct format !"))
            .ShouldNotBeNull("Received different error message");

            exception.Message.ShouldEqual("Couldn't create recipe connection");
        }

        [Test]
        [Category("IntegrationTests")]
        [Category("RecipeConnection")]
        public void Cannot_insert_recipe_connection_to_database_when_uniquename_is_not_in_correct_format()
        {
            recipeconnection.UniqueName = "Raspberry PI 3";

            var exception = typeof(BusinessException).ShouldBeThrownBy(() => recipeconnService.CreateRecipeConnection(recipeconnection));

            var businessException = exception as BusinessException;
            businessException.ShouldNotBeNull();

            businessException?.Errors.SingleOrDefault(error => error.ErrorMessage.Equals("Property  should be unique in database and in correct format !"))
            .ShouldNotBeNull("Received different error message");

            exception.Message.ShouldEqual("Couldn't create recipe connection");
        }

        [Test]
        [Category("IntegrationTests")]
        [Category("RecipeConnection")]
        public void Cannot_insert_recipe_connection_to_database_when_uniquename_is_not_in_correct_format_unique_name_with_upercases()
        {
            recipeconnection.UniqueName = "raspberry-PI-3";

            var exception = typeof(BusinessException).ShouldBeThrownBy(() => recipeconnService.CreateRecipeConnection(recipeconnection));

            var businessException = exception as BusinessException;
            businessException.ShouldNotBeNull();

            businessException?.Errors.SingleOrDefault(error => error.ErrorMessage.Equals("Property  should be unique in database and in correct format !"))
            .ShouldNotBeNull("Received different error message");

            exception.Message.ShouldEqual("Couldn't create recipe connection");
        }

        [Test]
        [Category("IntegrationTests")]
        [Category("RecipeConnection")]
        public void Can_get_recipe_connection_by_id()
        {
            // Insert to db
            recipeconnService.CreateRecipeConnection(recipeconnection);

            recipeconnection.ShouldNotBeNull();
            recipeconnection.Id.ShouldNotBeNull();

            // now get the same record from db
            var recipeconnFromDB = recipeconnService.GetRecipeConnection(recipeconnection.Id.ToString());

            recipeconnFromDB.ShouldNotBeNull();
            recipeconnFromDB.Id.ShouldNotBeNull();
            recipeconnFromDB.Name.ShouldEqual("abc");
        }

        [Test]
        [Category("IntegrationTests")]
        [Category("RecipeConnection")]
        public void Can_get_all_recipe_connections()
        {
            // Create another recipe connection
            RecipeConnection recipeconnection2 = new RecipeConnection
            {
                Name = "def",
                UniqueName = "456"
            };

            // |Insert to db
            recipeconnService.CreateRecipeConnection(recipeconnection);
            recipeconnService.CreateRecipeConnection(recipeconnection2);

            // get all documents
            var recipeconnections = recipeconnService.GetAllRecipeConnections();

            recipeconnections.ShouldBe<List<RecipeConnection>>();
            (recipeconnections.Count > 0).ShouldBeTrue();
        }

        [Test]
        [Category("IntegrationTests")]
        [Category("RecipeConnection")]
        public void Can_get_all_recipe_connections_by_name()
        {
            // Create another recipe connection
            RecipeConnection recipeconnection2 = new RecipeConnection
            {
                Name = "def",
                UniqueName = "456"
            };

            // |Insert to db
            recipeconnService.CreateRecipeConnection(recipeconnection);
            recipeconnService.CreateRecipeConnection(recipeconnection2);

            recipeconnection.ShouldNotBeNull();
            recipeconnection.Id.ShouldNotBeNull();
            recipeconnection2.ShouldNotBeNull();
            recipeconnection2.Id.ShouldNotBeNull();

            // get document
            var recipeconnections = recipeconnService.GetAllRecipeConnections(recipeconnection.Name);

            recipeconnections.ShouldBe<List<RecipeConnection>>();
            recipeconnections.Count.ShouldEqual(1);
        }

        [Test]
        [Category("IntegrationTests")]
        [Category("RecipeConnection")]
        public void Can_update_recipe_connection_to_database()
        {
            // Insert to db
            recipeconnService.CreateRecipeConnection(recipeconnection);

            recipeconnection.ShouldNotBeNull();
            recipeconnection.Id.ShouldNotBeNull();

            // update
            recipeconnection.Name = "def";
            recipeconnService.UpdateRecipeConnection(recipeconnection);

            // get the same document from db and check value
            var recipeconnFromdb = recipeconnService.GetRecipeConnection(recipeconnection.Id.ToString());
            recipeconnFromdb.ShouldNotBeNull();
            recipeconnFromdb.Name.ShouldEqual("def");
        }

        [Test]
        [Category("IntegrationTests")]
        [Category("RecipeConnection")]
        public void Can_delete_recipe_connection_from_database()
        {
            // Insert to db
            recipeconnService.CreateRecipeConnection(recipeconnection);

            recipeconnection.ShouldNotBeNull();
            recipeconnection.Id.ShouldNotBeNull();

            // delete the same record
            recipeconnService.DeleteRecipeConnection(recipeconnection.Id.ToString());

            // try get deleted document
            var recipeconnFromdb = recipeconnService.GetRecipeConnection(recipeconnection.Id.ToString());

            recipeconnFromdb.ShouldNotBeNull();
            recipeconnFromdb.Id.ShouldEqual(ObjectId.Empty);
        }

        // After every test, delete all records from db
        [TearDown]
        public void Dispose()
        {
            var recipeConnections = Db.Find<RecipeConnection>(_ => true);

            foreach (var recipeConnection in recipeConnections)
            {
                Db.DeleteOne<RecipeConnection>(x => x.Id == recipeConnection.Id);
            }
        }
    }
}
