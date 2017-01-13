﻿using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.Domain;
using PresentConnection.Internship7.Iot.ServiceModels;
using ServiceStack;

namespace PresentConnection.Internship7.Iot.Services
{
    public class CreateRecipeConnectionService : ServiceBase
    {
        public IRecipeConnectionService RecipeConnectionService { get; set; }

        public CreateRecipeConnectionResponse Any(CreateRecipeConnnection request)
        {
            var response = new CreateRecipeConnectionResponse();

            var recipeConnection = new RecipeConnection
            {
                Name = request.Name,
                UniqueName = request.UniqueName
            };

            RecipeConnectionService.CreateRecipeConnection(recipeConnection);

            var cacheKey = CacheKeys.RecipeConnections.List;
            Request.RemoveFromCache(Cache, cacheKey);

            response.Result = recipeConnection;
            return response;
        }
    }
}
