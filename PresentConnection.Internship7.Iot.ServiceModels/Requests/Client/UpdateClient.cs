using ServiceStack;
using PresentConnection.Internship7.Iot.Domain;
using System.Collections.Generic;

namespace PresentConnection.Internship7.Iot.ServiceModels
{
    [Route("/clients/{Id}", "PUT", Summary = "Update client")]
    public class UpdateClient
    {
        public string Name { get; set; }
        public List<Subscription> Subscriptions { get; set; }
        public string Id { get; set; }
    }
}
