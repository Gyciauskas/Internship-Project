using PresentConnection.Internship7.Iot.Domain;
using System.Collections.Generic;

namespace PresentConnection.Internship7.Iot.ServiceModels
{
    public class GetRecipesResponse
    {
        public GetRecipesResponse()
        {
            Recipes = new List<Recipe>();
        }

        public List<Recipe> Recipes { get; set; }
    }
}
