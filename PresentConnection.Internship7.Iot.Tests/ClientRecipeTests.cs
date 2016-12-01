using MongoDB.Bson;
using NUnit.Framework;
using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.BusinessImplementation;
using PresentConnection.Internship7.Iot.Domain;
using System.Collections.Generic;
using System.Linq;
using CodeMash.Net;
using PresentConnection.Internship7.Iot.Utils;

namespace PresentConnection.Internship7.Iot.Tests
{
    [TestFixture]
    public class ClientRecipeTests
    {
        private IClientRecipeService clientRecipeService;

        [SetUp]
        public void SetUp()
        {
            clientRecipeService = new ClientRecipeService();
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.ClientRecipes")]
        public void Can_insert_client_recipe_to_database()
        {
            var clientR = new ClientRecipe()
            {
                RecipeId = "a",
                ClientId = "1"
            };
            clientRecipeService.CreateClientRecipe(clientR, "1");

            clientR.ShouldNotBeNull();
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.ClientDevice")]
        public void Cannot_insert_when_code_user_wants_to_compromise_data_and_pass_different_client_id()
        {
            var clientR = new ClientRecipe()
            {
                RecipeId = "a",
                ClientId = "OtherClientId"
            };

            var exception = typeof(BusinessException).ShouldBeThrownBy(() => clientRecipeService.CreateClientRecipe(clientR, "OtherClientId2"));

            var businessException = exception as BusinessException;
            businessException.ShouldNotBeNull();

            businessException?.Errors.SingleOrDefault(error => error.ErrorMessage.Equals("Access is denied"))
                .ShouldNotBeNull("Received different error message");

            exception.Message.ShouldEqual("Cannot create client recipe");
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.ClientRecipes")]
        public void Cannot_insert_client_recipe_to_database_when_recipeId_is_not_provided()
        {
            var clientR = new ClientRecipe()
            {
                RecipeId = "",
                ClientId = "1"
            };

            typeof(BusinessException).ShouldBeThrownBy(() => clientRecipeService.CreateClientRecipe(clientR, "1"));
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.ClientRecipes")]
        public void Cannot_insert_client_recipe_to_database_when_clientId_is_not_provided()
        {
            var clientR = new ClientRecipe()
            {
                RecipeId = "a",
                ClientId = ""
            };

            typeof(BusinessException).ShouldBeThrownBy(() => clientRecipeService.CreateClientRecipe(clientR, "1"));
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.ClientRecipes")]
        public void Can_get_client_recipe_by_id()
        {
            var clientR = new ClientRecipe()
            {
                RecipeId = "a",
                ClientId = "1"
            };

            clientRecipeService.CreateClientRecipe(clientR, "1");

            clientR.ShouldNotBeNull();
            clientR.Id.ShouldNotBeNull();

            var clientRecipeFromDb = clientRecipeService.GetClientRecipe(clientR.Id.ToString(), "1");
            clientRecipeFromDb.ShouldNotBeNull();
            clientRecipeFromDb.Id.ShouldNotBeNull();
            clientRecipeFromDb.RecipeId.ShouldEqual("a");
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.ClientRecipes")]
        public void Can_get_all_client_recipes()
        {
            var clientR = new ClientRecipe()
            {
                RecipeId = "a",
                ClientId = "1"
            };

            var clientR2 = new ClientRecipe()
            {
                RecipeId = "b",
                ClientId = "2"
            };

            clientRecipeService.CreateClientRecipe(clientR, "1");
            clientR.ShouldNotBeNull();
            clientR.Id.ShouldNotBeNull();


            clientRecipeService.CreateClientRecipe(clientR2, "2");
            clientR2.ShouldNotBeNull();
            clientR2.Id.ShouldNotBeNull();



            var clientRecipes = clientRecipeService.GetAllClientRecipes("1", "1");

            clientRecipes.ShouldBe<List<ClientRecipe>>();
            (clientRecipes.Count > 0).ShouldBeTrue();
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.ClientRecipes")]
        public void Can_get_all_client_recipes_by_clientId()
        {
            var clientR = new ClientRecipe()
            {
                RecipeId = "a",
                ClientId = "1"
            };

            var clientR2 = new ClientRecipe()
            {
                RecipeId = "b",
                ClientId = "2"
            };

            var clientR3 = new ClientRecipe()
            {
                RecipeId = "c",
                ClientId = "3"
            };


            clientRecipeService.CreateClientRecipe(clientR, "1");
            clientR.ShouldNotBeNull();
            clientR.Id.ShouldNotBeNull();

            clientRecipeService.CreateClientRecipe(clientR2, "2");
            clientR2.ShouldNotBeNull();
            clientR2.Id.ShouldNotBeNull();

            clientRecipeService.CreateClientRecipe(clientR3, "3");
            clientR3.ShouldNotBeNull();
            clientR3.Id.ShouldNotBeNull();


            var clientRecipes = clientRecipeService.GetAllClientRecipes("2", "2");

            clientRecipes.ShouldBe<List<ClientRecipe>>();
            (clientRecipes.Count > 0).ShouldBeTrue();
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.ClientRecipes")]
        public void Cannot_get_other_client_recipes()
        {
            var clientR = new ClientRecipe()
            {
                RecipeId = "a",
                ClientId = "1"
            };

            var clientR2 = new ClientRecipe()
            {
                RecipeId = "b",
                ClientId = "2"
            };

            var clientR3 = new ClientRecipe()
            {
                RecipeId = "c",
                ClientId = "3"
            };

            clientRecipeService.CreateClientRecipe(clientR, "1");
            clientR.ShouldNotBeNull();
            clientR.Id.ShouldNotBeNull();

            clientRecipeService.CreateClientRecipe(clientR2, "2");
            clientR2.ShouldNotBeNull();
            clientR2.Id.ShouldNotBeNull();

            clientRecipeService.CreateClientRecipe(clientR3, "3");
            clientR3.ShouldNotBeNull();
            clientR3.Id.ShouldNotBeNull();

            var exception = typeof(BusinessException).ShouldBeThrownBy(() => clientRecipeService.GetAllClientRecipes("1", "12"));

            var businessException = exception as BusinessException;
            businessException.ShouldNotBeNull();

            businessException?.Errors.SingleOrDefault(error => error.ErrorMessage.Equals("Access is denied"))
                .ShouldNotBeNull("Received different error message");

            exception.Message.ShouldEqual("You don't have permissions to get client recipes");
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.ClientRecipes")]
        public void Can_update_client_recipe_to_database()
        {
            var clientR = new ClientRecipe()
            {
                RecipeId = "a",
                ClientId = "1"
            };

            clientRecipeService.CreateClientRecipe(clientR, "1");

            clientR.ShouldNotBeNull();
            clientR.Id.ShouldNotBeNull();

            // Update name and send update to db
            clientR.RecipeId = "b";
            clientRecipeService.UpdateClientRecipe(clientR, "1");

            // Get item from db and check if name was updated
            var clientRecipeFromDb = clientRecipeService.GetClientRecipe(clientR.Id.ToString(), "1");
            clientRecipeFromDb.ShouldNotBeNull();
            clientRecipeFromDb.Id.ShouldNotBeNull();
            clientRecipeFromDb.RecipeId.ShouldEqual("b");
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.ClientRecipes")]
        public void Cannot_update_other_client_recipe()
        {
            var clientR = new ClientRecipe()
            {
                RecipeId = "a",
                ClientId = "1"
            };

            clientRecipeService.CreateClientRecipe(clientR, "1");

            clientR.ShouldNotBeNull();
            clientR.Id.ShouldNotBeNull();

            var clientRecipeCompromised = new ClientRecipe()
            {
                Id = clientR.Id,
                RecipeId = "a",
                ClientId = "1"
            };

            var exception = typeof(BusinessException).ShouldBeThrownBy(() => clientRecipeService.UpdateClientRecipe(clientRecipeCompromised, "2"));

            var businessException = exception.InnerException as BusinessException;
            businessException.ShouldNotBeNull();

            businessException?.Errors.SingleOrDefault(error => error.ErrorMessage.Equals("Access is denied"))
                .ShouldNotBeNull("Received different error message");

            exception.Message.ShouldEqual("You don't have permissions to update this client recipe");
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.ClientRecipes")]
        public void Can_delete_client_recipe_from_database()
        {
            var clientR = new ClientRecipe()
            {
                RecipeId = "a",
                ClientId = "1"
            };

            // First insert manufacturer to db
            clientRecipeService.CreateClientRecipe(clientR, "1");
            clientR.ShouldNotBeNull();
            clientR.Id.ShouldNotBeNull();

            // Delete manufacturer from db
            var isDeleted = clientRecipeService.DeleteClientRecipe(clientR.Id.ToString(), "1");
            isDeleted.ShouldEqual(true);
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.ClientRecipes")]
        public void Cannot_delete_other_client_recipe()
        {
            var clientR = new ClientRecipe()
            {
                RecipeId = "a",
                ClientId = "1"
            };

            // First insert manufacturer to db
            clientRecipeService.CreateClientRecipe(clientR, "1");
            clientR.ShouldNotBeNull();
            clientR.Id.ShouldNotBeNull();

            // Somehow I stole Id
            var exception = typeof(BusinessException).ShouldBeThrownBy(() => clientRecipeService.DeleteClientRecipe(clientR.Id.ToString(), "2"));

            var businessException = exception.InnerException as BusinessException;
            businessException.ShouldNotBeNull();

            businessException?.Errors.SingleOrDefault(error => error.ErrorMessage.Equals("Access is denied"))
                .ShouldNotBeNull("Received different error message");

            exception.Message.ShouldEqual("You don't have permissions to delete this client recipe");
        }

        [TearDown]
        public void Dispose()
        {
            var clientRecipes = Db.Find<ClientRecipe>(_ => true);
            foreach (var clientRecipe in clientRecipes)
            {
                Db.DeleteOne<ClientRecipe>(x => x.Id == clientRecipe.Id);
            }
        }

    }
}
