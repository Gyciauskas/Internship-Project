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
                RecipeId = "1",
                ClientId = "1",
            };
            clientRecipeService.CreateClientRecipe(clientR);

            clientR.ShouldNotBeNull();
        }
        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.ClientRecipes")]
        public void Cannot_insert_client_recipes_to_database_when_recipeId_is_not_provided()
        {
            var clientR = new ClientRecipe()
            {
                RecipeId = "",
                ClientId = "1",
            };

            typeof(BusinessException).ShouldBeThrownBy(() => clientRecipeService.CreateClientRecipe(clientR));
        }
        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.ClientRecipes")]
        public void Cannot_insert_client_recipes_to_database_when_clientId_is_not_provided()
        {
            var clientR = new ClientRecipe()
            {
                RecipeId = "1",
                ClientId = "",
            };

            typeof(BusinessException).ShouldBeThrownBy(() => clientRecipeService.CreateClientRecipe(clientR));
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.ClientRecipes")]
        public void Can_get_ClientRecipe_by_id()
        {
            var clientR = new ClientRecipe()
            {
                RecipeId = "1",
                ClientId = "1",
            };

            clientRecipeService.CreateClientRecipe(clientR);

            clientR.ShouldNotBeNull();
            clientR.Id.ShouldNotBeNull();

            var clientRecipeFromDb = clientRecipeService.GetClientRecipe(clientR.Id.ToString());
            clientRecipeFromDb.ShouldNotBeNull();
            clientRecipeFromDb.Id.ShouldNotBeNull();
            clientRecipeFromDb.RecipeId.ShouldEqual("1");

        }
        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.ClientRecipes")]
        public void Can_get_all_ClientRecipes()
        {
            var clientR = new ClientRecipe()
            {
                RecipeId = "1",
                ClientId = "1",
            };

            var clientR2 = new ClientRecipe()
            {
                RecipeId = "2",
                ClientId = "2",
            };

            clientRecipeService.CreateClientRecipe(clientR);
            clientR.ShouldNotBeNull();
            clientR.Id.ShouldNotBeNull();


            clientRecipeService.CreateClientRecipe(clientR2);
            clientR2.ShouldNotBeNull();
            clientR2.Id.ShouldNotBeNull();



            var clientRecipes = clientRecipeService.GetAllClientRecipes();

            clientRecipes.ShouldBe<List<ClientRecipe>>();
            (clientRecipes.Count > 0).ShouldBeTrue();
        }
        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.ClientRecipes")]
        public void Can_get_all_clientRecipes_by_recipeId()
        {
            var clientR = new ClientRecipe()
            {
                RecipeId = "1",
                ClientId = "1",
            };

            var clientR2 = new ClientRecipe()
            {
                RecipeId = "2",
                ClientId = "2",
            };

            var clientR3 = new ClientRecipe()
            {
                RecipeId = "3",
                ClientId = "3",
            };


            clientRecipeService.CreateClientRecipe(clientR);
            clientR.ShouldNotBeNull();
            clientR.Id.ShouldNotBeNull();

            clientRecipeService.CreateClientRecipe(clientR2);
            clientR2.ShouldNotBeNull();
            clientR2.Id.ShouldNotBeNull();

            clientRecipeService.CreateClientRecipe(clientR3);
            clientR3.ShouldNotBeNull();
            clientR3.Id.ShouldNotBeNull();


            var clientRecipes = clientRecipeService.GetAllClientRecipes("2");

            clientRecipes.ShouldBe<List<ClientRecipe>>();
            (clientRecipes.Count > 0).ShouldBeTrue();
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.ClientRecipes")]
        public void Can_update_ClientRecipe_to_database()
        {
            var clientR = new ClientRecipe()
            {
                RecipeId = "1",
                ClientId = "1",
            };

            clientRecipeService.CreateClientRecipe(clientR);

            clientR.ShouldNotBeNull();
            clientR.Id.ShouldNotBeNull();

            // Update name and send update to db
            clientR.RecipeId = "2";
            clientRecipeService.UpdateClientRecipe(clientR);

            // Get item from db and check if name was updated
            var clientRecipeFromDb = clientRecipeService.GetClientRecipe(clientR.Id.ToString());
            clientRecipeFromDb.ShouldNotBeNull();
            clientRecipeFromDb.Id.ShouldNotBeNull();
            clientRecipeFromDb.RecipeId.ShouldEqual("2");
        }
        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.ClientRecipes")]
        public void Can_delete_client_recipe_from_database()
        {
            var clientR = new ClientRecipe()
            {
                RecipeId = "1",
                ClientId = "1",
            };

            // First insert manufacturer to db
            clientR.ShouldNotBeNull();
            clientR.Id.ShouldNotBeNull();

            // Delete manufacturer from db
            clientRecipeService.DeleteClientRecipe(clientR.Id.ToString());

            // Get item from db and check if name was updated
            var clientRecipeFromDb = clientRecipeService.GetClientRecipe(clientR.Id.ToString());

            // issue with CodeMash library. Should return null when not found - not default object
            clientRecipeFromDb.ShouldNotBeNull();
            clientRecipeFromDb.Id.ShouldEqual(ObjectId.Empty);
        }


        [TearDown]
        public void Dispose()
        {
            var clientRecipes = clientRecipeService.GetAllClientRecipes();
            foreach (var client in clientRecipes)
            {
                clientRecipeService.DeleteClientRecipe(client.Id.ToString());
            }
        }

    }
}
