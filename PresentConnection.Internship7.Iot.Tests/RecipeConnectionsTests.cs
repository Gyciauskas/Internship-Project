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
    public partial class RecipeConnectionsTests
    {
        [Test]
        [Category("RecipeConnection")]
        public void Can_insert_recipe_connection_to_database()
        {
            // Insert to db
            recipeConnectionService.CreateRecipeConnection(goodRecipeConnection);

            // test result, if passed, than means validated successfully
            recipeConnectionService.ShouldNotBeNull();
        }

        [Test]
        [Category("RecipeConnection")]
        public void Cannot_insert_recipe_connection_to_database_when_name_is_not_provided()
        {
            goodRecipeConnection.Name = string.Empty;

            var exception = typeof(BusinessException).ShouldBeThrownBy(() => recipeConnectionService.CreateRecipeConnection(goodRecipeConnection));
            exception.Message.ShouldEqual("Couldn't create recipe connection");
        }

        [Test]
        [Category("RecipeConnection")]
        public void Cannot_insert_recipe_connection_to_database_when_uniquename_is_not_provided()
        {
            goodRecipeConnection.UniqueName = string.Empty;

            var exception = typeof(BusinessException).ShouldBeThrownBy(() => recipeConnectionService.CreateRecipeConnection(goodRecipeConnection));
            exception.Message.ShouldEqual("Couldn't create recipe connection");
        }

        [Test]
        [Category("RecipeConnection")]
        public void Cannot_insert_recipe_connection_to_database_when_such_uniquename_exist()
        {
            recipeConnectionService.CreateRecipeConnection(goodRecipeConnection);

            var recipeconnection2 = new RecipeConnection
            {
                UniqueName = "abc",
                Name = "abc",
            };

            var exception = typeof(BusinessException).ShouldBeThrownBy(() => recipeConnectionService.CreateRecipeConnection(recipeconnection2));

            var businessException = exception as BusinessException;
            businessException.ShouldNotBeNull();

            businessException?.Errors.SingleOrDefault(error => error.ErrorMessage.Equals("Property  should be unique in database and in correct format !"))
            .ShouldNotBeNull("Received different error message");

            exception.Message.ShouldEqual("Couldn't create recipe connection");
        }

        [Test]
        [Category("RecipeConnection")]
        public void Cannot_insert_recipe_connection_to_database_when_uniquename_is_not_in_correct_format()
        {
            goodRecipeConnection.UniqueName = "Raspberry PI 3";

            var exception = typeof(BusinessException).ShouldBeThrownBy(() => recipeConnectionService.CreateRecipeConnection(goodRecipeConnection));

            var businessException = exception as BusinessException;
            businessException.ShouldNotBeNull();

            businessException?.Errors.SingleOrDefault(error => error.ErrorMessage.Equals("Property  should be unique in database and in correct format !"))
            .ShouldNotBeNull("Received different error message");

            exception.Message.ShouldEqual("Couldn't create recipe connection");
        }

        [Test]
        [Category("RecipeConnection")]
        public void Cannot_insert_recipe_connection_to_database_when_uniquename_is_not_in_correct_format_unique_name_with_upercases()
        {
            goodRecipeConnection.UniqueName = "raspberry-PI-3";

            var exception = typeof(BusinessException).ShouldBeThrownBy(() => recipeConnectionService.CreateRecipeConnection(goodRecipeConnection));

            var businessException = exception as BusinessException;
            businessException.ShouldNotBeNull();

            businessException?.Errors.SingleOrDefault(error => error.ErrorMessage.Equals("Property  should be unique in database and in correct format !"))
            .ShouldNotBeNull("Received different error message");

            exception.Message.ShouldEqual("Couldn't create recipe connection");
        }

        [Test]
        [Category("RecipeConnection")]
        public void Can_get_recipe_connection_by_id()
        {
            // Insert to db
            recipeConnectionService.CreateRecipeConnection(goodRecipeConnection);

            goodRecipeConnection.ShouldNotBeNull();
            goodRecipeConnection.Id.ShouldNotBeNull();

            // now get the same record from db
            var recipeconnFromDB = recipeConnectionService.GetRecipeConnection(goodRecipeConnection.Id.ToString());

            recipeconnFromDB.ShouldNotBeNull();
            recipeconnFromDB.Id.ShouldNotBeNull();
            recipeconnFromDB.Name.ShouldEqual("abc");
        }

        [Test]
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
            recipeConnectionService.CreateRecipeConnection(goodRecipeConnection);
            recipeConnectionService.CreateRecipeConnection(recipeconnection2);

            // get all documents
            var recipeconnections = recipeConnectionService.GetAllRecipeConnections();

            recipeconnections.ShouldBe<List<RecipeConnection>>();
            (recipeconnections.Count > 0).ShouldBeTrue();
        }

        [Test]
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
            recipeConnectionService.CreateRecipeConnection(goodRecipeConnection);
            recipeConnectionService.CreateRecipeConnection(recipeconnection2);

            goodRecipeConnection.ShouldNotBeNull();
            goodRecipeConnection.Id.ShouldNotBeNull();
            recipeconnection2.ShouldNotBeNull();
            recipeconnection2.Id.ShouldNotBeNull();

            // get document
            var recipeconnections = recipeConnectionService.GetAllRecipeConnections(goodRecipeConnection.Name);

            recipeconnections.ShouldBe<List<RecipeConnection>>();
            recipeconnections.Count.ShouldEqual(1);
        }

        [Test]
        [Category("RecipeConnection")]
        public void Can_update_recipe_connection_to_database()
        {
            // Insert to db
            recipeConnectionService.CreateRecipeConnection(goodRecipeConnection);

            goodRecipeConnection.ShouldNotBeNull();
            goodRecipeConnection.Id.ShouldNotBeNull();

            // update
            goodRecipeConnection.Name = "def";
            recipeConnectionService.UpdateRecipeConnection(goodRecipeConnection);

            // get the same document from db and check value
            var recipeconnFromdb = recipeConnectionService.GetRecipeConnection(goodRecipeConnection.Id.ToString());
            recipeconnFromdb.ShouldNotBeNull();
            recipeconnFromdb.Name.ShouldEqual("def");
        }

        [Test]
        [Category("RecipeConnection")]
        public void Can_delete_recipe_connection_from_database()
        {
            // Insert to db
            recipeConnectionService.CreateRecipeConnection(goodRecipeConnection);

            goodRecipeConnection.ShouldNotBeNull();
            goodRecipeConnection.Id.ShouldNotBeNull();

            // delete the same record
            recipeConnectionService.DeleteRecipeConnection(goodRecipeConnection.Id.ToString());

            // try get deleted document
            var recipeconnFromdb = recipeConnectionService.GetRecipeConnection(goodRecipeConnection.Id.ToString());

            recipeconnFromdb.ShouldNotBeNull();
            recipeconnFromdb.Id.ShouldEqual(ObjectId.Empty);
        }
    }
}
