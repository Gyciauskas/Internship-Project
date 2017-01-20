using MongoDB.Bson;
using NUnit.Framework;
using System.Linq;
using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.BusinessImplementation;
using PresentConnection.Internship7.Iot.Domain;
using System.Collections.Generic;
using PresentConnection.Internship7.Iot.Utils;

namespace PresentConnection.Internship7.Iot.Tests
{
    [TestFixture]
    public partial class RecipeTests
    {
        [Test]
        [Category("Recipe")]
        public void Can_insert_recipe_to_database()
        {
            recipeService.CreateRecipe(goodRecipe);

            goodRecipe.ShouldNotBeNull();
            goodRecipe.Id.ShouldNotBeNull();
        }

        [Test]
        [Category("Recipe")]
        public void Cannot_insert_recipe_to_database_when_name_is_not_provided()
        {
            goodRecipe.Name = string.Empty;

            var exception = typeof(BusinessException).ShouldBeThrownBy(() => recipeService.CreateRecipe(goodRecipe));
            exception.Message.ShouldEqual("Cannot create recipe");
        }

        [Test]
        [Category("Recipe")]
        public void Cannot_insert_recipe_to_database_when_uniquename_is_not_provided()
        {
            goodRecipe.UniqueName = string.Empty;
 
            var exception = typeof(BusinessException).ShouldBeThrownBy(() => recipeService.CreateRecipe(goodRecipe));
            exception.Message.ShouldEqual("Cannot create recipe"); 
        }

        [Test]
        [Category("Recipe")]
        public void Cannot_insert_recipe_to_database_when_such_uniquename_exist()
        {
            recipeService.CreateRecipe(goodRecipe);
          
            var recipe2 = new Recipe
            {
                UniqueName = "recipe-name",
                Name = "Recipe name",
                Images =
                {
                    "5821dcc11e9f341d4c6d0994"
                }
            };

            var exception = typeof(BusinessException).ShouldBeThrownBy(() => recipeService.CreateRecipe(recipe2));

            var businessException = exception as BusinessException;
            businessException.ShouldNotBeNull();

            businessException?.Errors.SingleOrDefault(error => error.ErrorMessage.Equals("Property  should be unique in database and in correct format !"))
            .ShouldNotBeNull("Received different error message");

            exception.Message.ShouldEqual("Cannot create recipe");
        }

        [Test]
        [Category("Recipe")]
        public void Cannot_insert_recipe_to_database_when_uniquename_is_not_in_correct_format()
        {
            goodRecipe.UniqueName = "Raspberry PI 3";

            var exception = typeof(BusinessException).ShouldBeThrownBy(() => recipeService.CreateRecipe(goodRecipe));

            var businessException = exception as BusinessException;
            businessException.ShouldNotBeNull();

            businessException?.Errors.SingleOrDefault(error => error.ErrorMessage.Equals("Property  should be unique in database and in correct format !"))
            .ShouldNotBeNull("Received different error message");

            exception.Message.ShouldEqual("Cannot create recipe");
        }

        [Test]
        [Category("Recipe")]
        public void Cannot_insert_recipe_to_database_when_uniquename_is_not_in_correct_format_unique_name_with_upercases()
        {
            goodRecipe.UniqueName = "raspberry-PI-3";

            var exception = typeof(BusinessException).ShouldBeThrownBy(() => recipeService.CreateRecipe(goodRecipe));

            var businessException = exception as BusinessException;
            businessException.ShouldNotBeNull();

            businessException?.Errors.SingleOrDefault(error => error.ErrorMessage.Equals("Property  should be unique in database and in correct format !"))
            .ShouldNotBeNull("Received different error message");

            exception.Message.ShouldEqual("Cannot create recipe");
        }

        [Test]
        [Category("Recipe")]
        public void Cannot_insert_recipe_to_database_when_such_image_is_not_provided()
        {
            goodRecipe.Images = null;

            var exception = typeof(BusinessException).ShouldBeThrownBy(() =>recipeService.CreateRecipe(goodRecipe));

            var businessException = exception as BusinessException;
            businessException.ShouldNotBeNull();
            businessException?.Errors.SingleOrDefault(error => error.ErrorMessage.Equals("Property Images should contain at least one item!"))
            .ShouldNotBeNull("Received different error message");

            exception.Message.ShouldEqual("Cannot create recipe");

        }

        [Test]
        [Category("Recipe")]
        public void Can_get_recipe_by_id()
        {
            recipeService.CreateRecipe(goodRecipe);

            goodRecipe.ShouldNotBeNull();
            goodRecipe.Id.ShouldNotBeNull();

            var recipeFromDB = recipeService.GetRecipe(goodRecipe.Id.ToString());
            recipeFromDB.ShouldNotBeNull();
            recipeFromDB.Id.ShouldNotBeNull();
            recipeFromDB.Name.ShouldEqual("Recipe name");
        }

        [Test]
        [Category("Recipe")]
        public void Can_get_all_recipes()
        {
            var goodRecipe1 = new Recipe
            {
                UniqueName = "raspberry-pi-3",
                Name = "Recipe name2",
                Images =
                {
                    "5821dcc11e9f341d4c6d0994"
                },
                Description = "description",
                IsVisible = true
            };

            recipeService.CreateRecipe(goodRecipe);
            recipeService.CreateRecipe(goodRecipe1);

            var recipes = recipeService.GetAllRecipes();

            recipes.ShouldBe<List<Recipe>>();
            (recipes.Count > 0).ShouldBeTrue();
        }

        [Test]
        [Category("Recipe")]
        public void Can_get_all_recipes_by_name()
        {
            var goodRecipe1 = new Recipe
            {
                UniqueName = "raspberry-pi-1",
                Name = "Recipe name2",
                Images =
                {
                    "5821dcc11e9f341d4c6d0994"
                },
                Description = "description",
                IsVisible = true
            };

            var goodRecipe2 = new Recipe
            {
                UniqueName = "raspberry-pi-3",
                Name = "Recipe name3",
                Images =
                {
                    "5821dcc11e9f341d4c6d0994"
                },
                Description = "description",
                IsVisible = true
            };

            recipeService.CreateRecipe(goodRecipe);
            recipeService.CreateRecipe(goodRecipe1);
            recipeService.CreateRecipe(goodRecipe2);

            var recipes = recipeService.GetAllRecipes("Recipe name");

            recipes.ShouldBe<List<Recipe>>();
            recipes.Count.ShouldEqual(1);
        }


        [Test]
        [Category("Recipe")]
        public void Can_update_recipe_to_database()
        {
            recipeService.CreateRecipe(goodRecipe);

            goodRecipe.ShouldNotBeNull();
            goodRecipe.Id.ShouldNotBeNull();

            goodRecipe.Name = "Recipe name4";
            recipeService.UpdateRecipe(goodRecipe);

            var recipeFromDB = recipeService.GetRecipe(goodRecipe.Id.ToString());
            recipeFromDB.ShouldNotBeNull();
            recipeFromDB.Name.ShouldEqual("Recipe name4");
        }

        [Test]
        [Category("Recipe")]
        public void Can_delete_recipe_from_database()
        {
            recipeService.CreateRecipe(goodRecipe);

            goodRecipe.ShouldNotBeNull();
            goodRecipe.Id.ShouldNotBeNull();

            recipeService.DeleteRecipe(goodRecipe.Id.ToString());

            var recipeFromDB = recipeService.GetRecipe(goodRecipe.Id.ToString());

            recipeFromDB.ShouldNotBeNull();
            recipeFromDB.Id.ShouldEqual(ObjectId.Empty);
        }
    }
}
