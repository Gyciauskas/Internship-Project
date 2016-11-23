using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeMash.Net;

namespace PresentConnection.Internship7.Iot.Domain
{
    [CollectionName("Clients")]
    public class Client : EntityBase
    {
        public Client()
        {
            Invoices = new List<Invoice>();
            Subscriptions = new List<Subscription>();
        }

        public string UserId { get; set; }
        public List<Invoice> Invoices { get; set; }
        public List<Subscription> Subscriptions { get; set; }
    }
}
