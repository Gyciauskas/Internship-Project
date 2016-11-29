using System.Collections.Generic;
using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.Domain;
using CodeMash.Net;
using FluentValidation.Results;
using MongoDB.Bson;
using MongoDB.Driver;
using PresentConnection.Internship7.Iot.Utils;
using PresentConnection.Internship7.Iot.Domain.Validators;
using System;

namespace PresentConnection.Internship7.Iot.BusinessImplementation
{
    public class RecipeService : IRecipeService
    {
        public string CreateRecipe(Recipe recipe)
        {
            RecipeValidator validator = new RecipeValidator();
            ValidationResult results = validator.Validate(recipe);

            bool validationSucceeded = results.IsValid;

            if (validationSucceeded)
            {
                Db.InsertOne(recipe);
                return recipe.Id.ToString();
            }
            else
            {
                throw new BusinessException("Cannot create recipe", results.Errors);
            }
        }

        public void UpdateRecipe(Recipe recipe)
        {
            RecipeValidator validator = new RecipeValidator();
            ValidationResult results = validator.Validate(recipe);

            bool validationSucceeded = results.IsValid;

            if (validationSucceeded)
            {
                Db.FindOneAndReplace(x => x.Id == recipe.Id, recipe);
            }
            else
            {
                throw new BusinessException("Cannot update recipe", results.Errors);
            }
        }

        public bool DeleteRecipe(string id)
        {
            var deleteResult = Db.DeleteOne<Recipe>(x => x.Id == ObjectId.Parse(id));
            return deleteResult.DeletedCount == 1;
        }

        public List<Recipe> GetAllRecipes(string name = "")
        {
            var filterBuilder = Builders<Recipe>.Filter;
            var filter = filterBuilder.Empty;

            if (!string.IsNullOrEmpty(name))
            {
                var findByNameFilter = Builders<Recipe>.Filter.Eq(x => x.Name, name);
                filter = filter & findByNameFilter;
            }

            var recipes = Db.Find(filter);
            return recipes;
        }

        public Recipe GetRecipe(string id)
        {
            return Db.FindOneById<Recipe>(id);
        }
    }
}
