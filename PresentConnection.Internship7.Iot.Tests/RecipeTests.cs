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
    public class RecipeTests
    {
        private IRecipeService recipeService;

        [SetUp]
        public void SetUp()
        {
            recipeService = new RecipeService();
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.Recipe")]
        public void Can_insert_manufacturer_to_database()
        {
            var recipe = new Recipe()
            {
                UniqueName = "img-save",
                Name = "Saving Image",
                Description = "...",
                IsVisible = true
            };
            recipeService.CreateRecipe(recipe);

            recipe.ShouldNotBeNull();
            recipe.Id.ShouldNotBeNull();
        }


        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.Recipe")]
        public void Cannot_insert_recipe_to_database_when_name_is_not_provided()
        {
            var recipe = new Recipe()
            {
                UniqueName = "",
                Name = "Saving Image",
                Description = "...",
                IsVisible = true
            };

            typeof(BusinessException).ShouldBeThrownBy(() => recipeService.CreateRecipe(recipe));
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.Manufacturer")]
        public void Cannot_insert_recipe_to_database_when_uniquename_is_not_provided()
        {
            var recipe = new Recipe()
            {
                UniqueName = "img-save",
                Name = "",
                Description = "...",
                IsVisible = true
            };

            typeof(BusinessException).ShouldBeThrownBy(() => recipeService.CreateRecipe(recipe));
        }


        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.Recipe")]
        public void Can_get_recipe_by_id()
        {
            var recipe = new Recipe()
            {
                UniqueName = "img-save",
                Name = "Saving Image",
                Description = "...",
                IsVisible = true
            };
            recipeService.CreateRecipe(recipe);

            recipe.ShouldNotBeNull();
            recipe.Id.ShouldNotBeNull();

            var recipeFromDB = recipeService.GetRecipe(recipe.Id.ToString());
            recipeFromDB.ShouldNotBeNull();
            recipeFromDB.Id.ShouldNotBeNull();
            recipeFromDB.Name.ShouldEqual("Saving Image");
        }


        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.Recipe")]
        public void Can_get_all_recipes()
        {
            var recipe1 = new Recipe()
            {
                UniqueName = "img-save",
                Name = "Saving Image",
                Description = "...",
                IsVisible = true
            };

            var recipe2 = new Recipe()
            {
                UniqueName = "jpg-save",
                Name = "Saving Image.jpg",
                Description = "...",
                IsVisible = true
            };

            recipeService.CreateRecipe(recipe1);
            recipe1.ShouldNotBeNull();
            recipe1.Id.ShouldNotBeNull();


            recipeService.CreateRecipe(recipe2);
            recipe2.ShouldNotBeNull();
            recipe2.Id.ShouldNotBeNull();



            var recipes = recipeService.GetAllRecipes();

            recipes.ShouldBe<List<Recipe>>();
            (recipes.Count > 0).ShouldBeTrue();
        }


        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.Recipe")]
        public void Can_get_all_recipes_by_name()
        {
            var recipe1 = new Recipe()
            {
                UniqueName = "img-save",
                Name = "Saving Image",
                Description = "...",
                IsVisible = true
            };

            var recipe2 = new Recipe()
            {
                UniqueName = "jpg-save",
                Name = "Saving Image.jpg",
                Description = "...",
                IsVisible = true
            };

            var recipe3 = new Recipe()
            {
                UniqueName = "image",
                Name = "Image",
                Description = "...",
                IsVisible = true
            };

            recipeService.CreateRecipe(recipe1);
            recipe1.ShouldNotBeNull();
            recipe1.Id.ShouldNotBeNull();


            recipeService.CreateRecipe(recipe2);
            recipe2.ShouldNotBeNull();
            recipe2.Id.ShouldNotBeNull();

            recipeService.CreateRecipe(recipe3);
            recipe3.ShouldNotBeNull();
            recipe3.Id.ShouldNotBeNull();

            var recipes = recipeService.GetAllRecipes("Image");

            recipes.ShouldBe<List<Recipe>>();
            recipes.Count.ShouldEqual(1);
        }


        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.Recipe")]
        public void Can_update_recipe_to_database()
        {
            var recipe = new Recipe()
            {
                UniqueName = "img-save",
                Name = "Saving Image",
                Description = "...",
                IsVisible = true
            };

            recipeService.CreateRecipe(recipe);

            recipe.ShouldNotBeNull();
            recipe.Id.ShouldNotBeNull();

            recipe.Name = "Save Image.jpg";
            recipeService.UpdateRecipe(recipe);

            // Get item from db and check if name was updated
            var recipeFromDB = recipeService.GetRecipe(recipe.Id.ToString());
            recipeFromDB.ShouldNotBeNull();
            recipeFromDB.Name.ShouldEqual("Save Image.jpg");
        }


        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.Recipe")]
        public void Can_delete_recipe_from_database()
        {
            var recipe = new Recipe()
            {
                UniqueName = "img-save",
                Name = "Saving Image",
                Description = "...",
                IsVisible = true
            };

            recipeService.CreateRecipe(recipe);

            // First insert manufacturer to db
            recipe.ShouldNotBeNull();
            recipe.Id.ShouldNotBeNull();

            // Delete manufacturer from db
            recipeService.DeleteRecipe(recipe.Id.ToString());

            // Get item from db and check if name was updated
            var recipeFromDB = recipeService.GetRecipe(recipe.Id.ToString());

            // issue with CodeMash library. Should return null when not found - not default object
            recipeFromDB.ShouldNotBeNull();
            recipeFromDB.Id.ShouldEqual(ObjectId.Empty);
        }


        [TearDown]
        public void Dispose()
        {
            var recipes = recipeService.GetAllRecipes();
            foreach (var recipe in recipes)
            {
                recipeService.DeleteRecipe(recipe.Id.ToString());
            }
        }
    }
}
