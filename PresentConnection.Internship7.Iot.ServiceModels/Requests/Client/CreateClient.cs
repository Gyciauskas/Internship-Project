using ServiceStack;
using PresentConnection.Internship7.Iot.Domain;
using System.Collections.Generic;



namespace PresentConnection.Internship7.Iot.ServiceModels
{
    [Route("/clients", "POST", Summary = "Create client")]
    public class CreateClient
    {
        public string Name { get; set; }
        public List<Subscription> Subscriptions { get; set; }
    }
}
