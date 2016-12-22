using PresentConnection.Internship7.Iot.Domain;
using System.Collections.Generic;

namespace PresentConnection.Internship7.Iot.ServiceModels
{
    public class GetClientRecipesResponse
    {
        public GetClientRecipesResponse()
        {
            ClientRecipes = new List<ClientRecipe>();
        }
        public List<ClientRecipe> ClientRecipes { get; set; }      
    }
}