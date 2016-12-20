﻿using ServiceStack;

namespace PresentConnection.Internship7.Iot.ServiceModels
{
    [Route("/recipeConnections", "POST", Summary = "Create recipe connection")]
    public class CreateRecipeConnnection : IReturn<CreateRecipeConnectionResponse>
    {
        public string Name { get; set; }
        public string UniqueName { get; set; }
    }
}
