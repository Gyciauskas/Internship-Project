using ServiceStack;
using PresentConnection.Internship7.Iot.Domain;
using System.Collections.Generic;



namespace PresentConnection.Internship7.Iot.ServiceModels
{
    [Route("/clients", "POST", Summary = "Create client")]
    public class CreateClient : IReturn<CreateClientResponse>
    {
        public CreateClient()
        {
            Subscriptions = new List<Subscription>();
        }

        public string Name { get; set; }
        public List<Subscription> Subscriptions { get; set; }
    }
}
