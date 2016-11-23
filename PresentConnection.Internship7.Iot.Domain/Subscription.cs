using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentConnection.Internship7.Iot.Domain
{
    public enum SubscriptionType { Free, Paid, Enterprise }
    public enum SubscriptionSpan { Monthly, Annual } 
    public class Subscription
    {
        public DateTime CreatedOn { get; set; }
        public SubscriptionType SubscriptionType { get; set; }
        public SubscriptionSpan SubscriptionSpan { get; set; }
    }
}
