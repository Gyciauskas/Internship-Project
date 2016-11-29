using System.Collections.Generic;
using CodeMash.Net;

namespace PresentConnection.Internship7.Iot.Domain
{
    [CollectionName(Statics.Collections.Clients)]
    public class Client : EntityBase
    {
        public Client()
        {
            Invoices = new List<Invoice>();
            Subscriptions = new List<Subscription> { new Subscription() };
        }

        public string UserId { get; set; }
        public List<Invoice> Invoices { get; set; }
        public List<Subscription> Subscriptions { get; set; }
    }
}
