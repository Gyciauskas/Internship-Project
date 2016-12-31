using System.Collections.Generic;
using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.Domain;
using CodeMash.Net;
using PresentConnection.Internship7.Iot.Utils;
using MongoDB.Bson;
using MongoDB.Driver;

namespace PresentConnection.Internship7.Iot.BusinessImplementation
{
    public class RecipeConnectionService : IRecipeConnectionService
    {
        public void CreateRecipeConnection(RecipeConnection recipeconnection)
        {
            var validator = new RecipeConnectionValidator();
            var results = validator.Validate(recipeconnection);
            var validationSucceeded = results.IsValid;

            if (validationSucceeded)
            {
                Db.InsertOne(recipeconnection);
            }
            else
            {
                throw new BusinessException("Couldn't create recipe connection", results.Errors);
            }
        }

        public bool DeleteRecipeConnection(string id)
        {
            var deleteResult = Db.DeleteOne<RecipeConnection>(x => x.Id == ObjectId.Parse(id));

            return deleteResult.DeletedCount == 1;
        }

        public List<RecipeConnection> GetAllRecipeConnections(string name = "")
        {
            var filterBuilder = Builders<RecipeConnection>.Filter;
            var filter = filterBuilder.Empty;

            if (!string.IsNullOrEmpty(name))
            {
                var findByNameFilter = Builders<RecipeConnection>.Filter.Eq(x => x.Name, name);
                filter = filter & findByNameFilter;
            }

            var recipeconnections = Db.Find(filter);
            return recipeconnections;
        }

        public RecipeConnection GetRecipeConnection(string id)
        {
            return Db.FindOneById<RecipeConnection>(id);
        }

        public void UpdateRecipeConnection(RecipeConnection recipeconnection)
        {
            var validator = new RecipeConnectionValidator();
            var results = validator.Validate(recipeconnection);
            var validationSucceeded = results.IsValid;

            if (validationSucceeded)
            {
                Db.FindOneAndReplace(x => x.Id == recipeconnection.Id, recipeconnection);
            }
            else
            {
                throw new BusinessException("Couldn't update recipe connection", results.Errors);
            }
        }
    }
}


