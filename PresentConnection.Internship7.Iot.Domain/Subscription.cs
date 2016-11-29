using System;

namespace PresentConnection.Internship7.Iot.Domain
{
    public class Subscription
    {
        public Subscription()
        {
            CreatedOn = DateTime.Now;
            SubscriptionSpan = SubscriptionSpan.Monthly;
            SubscriptionType = SubscriptionType.Free;
        }

        public DateTime CreatedOn { get; set; }
        public SubscriptionType SubscriptionType { get; set; }
        public SubscriptionSpan SubscriptionSpan { get; set; }
    }
}
