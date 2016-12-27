﻿using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.Domain;
using PresentConnection.Internship7.Iot.ServiceModels;
using ServiceStack;

namespace PresentConnection.Internship7.Iot.Services
{
    public class UpdateClientRecipeService : ServiceBase
    {
        public IClientRecipeService ClientRecipeService { get; set; }

        public UpdateClientRecipeResponse Any(UpdateClientRecipe request)
        {
            var response = new UpdateClientRecipeResponse();
            
            var clientRecipe = ClientRecipeService.GetClientRecipe(request.Id, UserSession.UserAuthId);

            clientRecipe = clientRecipe?.PopulateWith(request);

            ClientRecipeService.UpdateClientRecipe(clientRecipe, UserSession.UserAuthId);

            response.Result = clientRecipe;
            return response;
        }
    }
}
