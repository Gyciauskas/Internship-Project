using MongoDB.Bson;
using NSubstitute;
using NUnit.Framework;
using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.BusinessImplementation;
using PresentConnection.Internship7.Iot.Domain;
using PresentConnection.Internship7.Iot.ServiceModels;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentConnection.Internship7.Iot.Tests
{
    [TestFixture]
    public partial class RecipeTests : TestsBase
    {
        private ServiceStackHost appHost;
        private IRecipeService recipeService;
        private Recipe goodRecipe;
        private IRecipeService recipeServiceMock;

        private void SetupMocks()
        {
            recipeServiceMock = Substitute.For<IRecipeService>();

            // when pass any parameter (Id) to recipeService.GetRecipe, return goodRecipe with passed id
            recipeServiceMock.GetRecipe(Arg.Any<string>()).Returns(info =>
            {
                goodRecipe.Id = ObjectId.Parse(info.ArgAt<string>(0));

                return goodRecipe;
            });

            // when we are trying get recipes, just return generated list
            recipeServiceMock.GetAllRecipes(string.Empty).Returns(info =>
            {
                var list = new List<Recipe>();

                goodRecipe.Id = ObjectId.GenerateNewId();
                list.Add(goodRecipe);

                goodRecipe.Id = ObjectId.GenerateNewId();
                goodRecipe.Name = "Object1";
                list.Add(goodRecipe);

                goodRecipe.Id = ObjectId.GenerateNewId();
                goodRecipe.Name = "Object2";
                list.Add(goodRecipe);

                return list;
            });

            // when we are trying get recipes by name, just generate list and filter by name
            recipeServiceMock.GetAllRecipes(Arg.Any<string>()).Returns(info =>
            {
                var list = new List<Recipe>();

                goodRecipe.Id = ObjectId.GenerateNewId();
                list.Add(goodRecipe);

                goodRecipe.Id = ObjectId.GenerateNewId();
                goodRecipe.Name = "Object1";
                list.Add(goodRecipe);

                goodRecipe.Id = ObjectId.GenerateNewId();
                goodRecipe.Name = "Object2";
                list.Add(goodRecipe);

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

            var container = appHost.Container;

            recipeService = new RecipeService();

            goodRecipe = new Recipe
            {
                UniqueName = "raspberry-pi-2",
                Name = "Recipe name",
                Images =
                {
                    "5821dcc11e9f341d4c6d0994"
                },
                Description = "description",
                IsVisible = true
            };

            SetupMocks();
        }

        [Test]
        [Category("Recipe")]
        public void Can_do_CRUD_recipe_to_database()
        {
            var client = new JsonServiceClient(BaseUri);

            // Create
            var createRequest = new CreateRecipe
            {
                Name = goodRecipe.Name,
                UniqueName = goodRecipe.UniqueName,
                Images = goodRecipe.Images
            };

            var createRecipeResponse = client.Post(createRequest);
            createRecipeResponse.ShouldNotBeNull();
            createRecipeResponse.Result.ShouldNotBeNull();
            createRecipeResponse.Result.ShouldNotBeNull();
            createRecipeResponse.Result.ShouldBeNotBeTheSameAs(ObjectId.Empty);

            var createRequest2 = new CreateRecipe
            {
                Name = "raspberry-pi-zero",
                UniqueName = "raspberry-pi-zero-125fv",
                Images = goodRecipe.Images
            };

            var createRecipeResponse2 = client.Post(createRequest2);
            createRecipeResponse2.ShouldNotBeNull();
            createRecipeResponse2.Result.ShouldNotBeNull();
            createRecipeResponse2.Result.ShouldNotBeNull();
            createRecipeResponse2.Result.ShouldBeNotBeTheSameAs(ObjectId.Empty);

            // Get all
            var getRecipesRequest = new GetRecipes
            {
                Name = string.Empty
            };

            var getRecipesResponse = client.Get(getRecipesRequest);
            getRecipesResponse.ShouldNotBeNull();
            getRecipesResponse.Result.ShouldNotBeNull();
            getRecipesResponse.Result.Count.ShouldEqual(2);

            // Get by name
            var getRecipesByNameRequest = new GetRecipes
            {
                Name = goodRecipe.Name
            };

            var getRecipesByNameResponse = client.Get(getRecipesByNameRequest);
            getRecipesByNameResponse.ShouldNotBeNull();
            getRecipesByNameResponse.Result.ShouldNotBeNull();
            getRecipesByNameResponse.Result.Count.ShouldEqual(1);

            // Update
            var updateRecipeRequest = new UpdateRecipe
            {
                Id = createRecipeResponse.Result,
                Name = goodRecipe.Name + "-Updated",
                UniqueName = goodRecipe.UniqueName + "+updated"
            };

            var updateRecipeResponse = client.Put(updateRecipeRequest);
            updateRecipeResponse.ShouldNotBeNull();
            updateRecipeResponse.Result.ShouldNotBeNull();

            // Get by Id
            var getRecipeById = new GetRecipe
            {
                Id = createRecipeResponse.Result
            };

            var getRecipeByIdResponse = client.Get(getRecipeById);
            getRecipeByIdResponse.ShouldNotBeNull();
            getRecipeByIdResponse.Result.Name.ShouldEqual(createRequest.Name + "-Updated");
            getRecipeByIdResponse.Result.UniqueName.ShouldEqual(createRequest.UniqueName + "+updated");

            // Delete 
            var deleteRequest = new DeleteRecipe { Id = getRecipeByIdResponse.Result.Id.ToString() };

            var deleteRequestResponse = client.Delete(deleteRequest);
            deleteRequestResponse.ShouldNotBeNull();
            deleteRequestResponse.Result.ShouldNotBeNull();
            deleteRequestResponse.Result.ShouldEqual(true);
        }

        [TearDown]
        public void Dispose()
        {
            appHost.Dispose();

            var recipes = recipeService.GetAllRecipes();
            foreach (var recipe in recipes)
            {
                recipeService.DeleteRecipe(recipe.Id.ToString());
            }
        }
    }
}
