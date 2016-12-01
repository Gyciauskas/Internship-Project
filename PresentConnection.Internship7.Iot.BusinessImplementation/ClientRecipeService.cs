using System.Collections.Generic;
using System.Linq;
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
        public void CreateClientRecipe(ClientRecipe clientRecipe, string responsibleClientId)
        {
            var validator = new ClientRecipeValidator(responsibleClientId);
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
        public void UpdateClientRecipe(ClientRecipe clientRecipe, string responsibleClientId)
        {
            try
            {
                GetClientRecipe(clientRecipe.Id.ToString(), responsibleClientId);
            }
            catch (BusinessException e)
            {
                throw new BusinessException("You don't have permissions to update this client recipe", e);
            }

            var validator = new ClientRecipeValidator(responsibleClientId);
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
        public bool DeleteClientRecipe(string id, string responsibleClientId)
        {
            try
            {
                GetClientRecipe(id, responsibleClientId);
            }
            catch (BusinessException e)
            {
                throw new BusinessException("You don't have permissions to delete this client recipe", e);
            }

            var deleteResult = Db.DeleteOne<ClientRecipe>(x => x.Id == ObjectId.Parse(id));
            return deleteResult.DeletedCount == 1;
        }
        /// <summary>
        /// Reads all objects
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        public List<ClientRecipe> GetAllClientRecipes(string clientId, string responsibleClientId)
        {
            var filterBuilder = Builders<ClientRecipe>.Filter;
            var filter = filterBuilder.Empty;

            if (!string.IsNullOrEmpty(clientId))
            {
                var findByClientIdFilter = Builders<ClientRecipe>.Filter.Eq(x => x.ClientId, clientId);
                filter = filter & findByClientIdFilter;
            }

            var clientRecipes = Db.Find(filter);

            var validator = new RecordsPermissionValidator(responsibleClientId);
            var results = validator.Validate(clientRecipes.Cast<IEntityWithSensitiveData>().ToList());
            var validationSucceeded = results.IsValid;

            if (validationSucceeded)
            {
                return clientRecipes;
            }
            throw new BusinessException("You don't have permissions to get client recipes", results.Errors);
        }
        /// <summary>
        /// gets 1 object by id
        /// </summary>
        /// <param name="id">object's id</param>
        /// <returns></returns>
        public ClientRecipe GetClientRecipe(string id, string responsibleClientId)
        {
            var clientRecipe = Db.FindOneById<ClientRecipe>(id);
            if (clientRecipe == null)
            {
                return null;
            }
            var validator = new RecordPermissionValidator(responsibleClientId);
            var results = validator.Validate(clientRecipe);
            var validationSucceeded = results.IsValid;

            if (validationSucceeded)
            {
                return clientRecipe;
            }
            throw new BusinessException("You don't have permissions to get client recipe", results.Errors);

        }
    }
}
