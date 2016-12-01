using System.Collections.Generic;
using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.Domain;
using CodeMash.Net;
using MongoDB.Bson;
using MongoDB.Driver;
using PresentConnection.Internship7.Iot.Utils; 


namespace PresentConnection.Internship7.Iot.BusinessImplementation
{
    public class ClientRecipeService : IClientRecipeService
    {
        /// <summary>
        /// Inserts new object into database
        /// </summary>
        /// <param name="clientRecipe">object</param>
        /// <returns></returns>
        public void CreateClientRecipe(ClientRecipe clientRecipe)
        {
            var validator = new ClientRecipeValidator();
            var results = validator.Validate(clientRecipe);
            var validationSucceeded = results.IsValid;

            if (validationSucceeded)
            {
                Db.InsertOne(clientRecipe);
            }
            else
            {
                throw new BusinessException("Cannot create client recipe", results.Errors);
            }
        }
        /// <summary>
        /// Updates object by replacing it with a new one
        /// </summary>
        /// <param name="clientRecipe">object</param>
        public void UpdateClientRecipe(ClientRecipe clientRecipe)
        {
            var validator = new ClientRecipeValidator();
            var results = validator.Validate(clientRecipe);
            var validationSucceeded = results.IsValid;

            if (validationSucceeded)
            {
                Db.FindOneAndReplace(x => x.Id == clientRecipe.Id, clientRecipe);
            }
            else
            {
                throw new BusinessException("Cannot update client recipe", results.Errors);
            }
        }
        /// <summary>
        /// deletes object from database
        /// </summary>
        /// <param name="id">object's id</param>
        /// <returns></returns>
        public bool DeleteClientRecipe(string id)
        {
            var deleteResult = Db.DeleteOne<ClientRecipe>(x => x.Id == ObjectId.Parse(id));
            return deleteResult.DeletedCount == 1;
        }
        /// <summary>
        /// Reads all objects
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        public List<ClientRecipe> GetAllClientRecipes(string clientId = "")
        {
            var filterBuilder = Builders<ClientRecipe>.Filter;
            var filter = filterBuilder.Empty;

            if (!string.IsNullOrEmpty(clientId))
            {
                var findByClientIdFilter = Builders<ClientRecipe>.Filter.Eq(x => x.ClientId, clientId);
                filter = filter & findByClientIdFilter;
            }

            var clientRecipes = Db.Find(filter);
            return clientRecipes;
        }
        /// <summary>
        /// gets 1 object by id
        /// </summary>
        /// <param name="id">object's id</param>
        /// <returns></returns>
        public ClientRecipe GetClientRecipe(string id)
        {
            return Db.FindOneById<ClientRecipe>(id);
        }
    }
}
