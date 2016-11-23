using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using NUnit.Framework;
using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.BusinessImplementation;
using PresentConnection.Internship7.Iot.Domain;
using PresentConnection.Internship7.Iot.Utils;

namespace PresentConnection.Internship7.Iot.Tests
{
    [TestFixture]
    public class ClientsTests
    {
        private IClientService clientService;

        [SetUp]
        public void SetUp()
        {
            clientService = new ClientService();
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.Client")]
        public void Can_insert_client_to_database()
        {
            var client = new Client()
            {
                UserId = "UserID",
                Subscriptions = 
                {
                    new Subscription()
                    {
                        CreatedOn = new DateTime(2014, 6, 14, 6, 32, 0),
                        SubscriptionType = SubscriptionType.Enterprise,
                        SubscriptionSpan = SubscriptionSpan.Monthly
                    },
                    new Subscription()
                    {
                        CreatedOn = new DateTime(2016, 6, 14, 6, 32, 0),
                        SubscriptionType = SubscriptionType.Enterprise,
                        SubscriptionSpan = SubscriptionSpan.Annual
                    }
                }
            };
            clientService.CreateClient(client);

            client.ShouldNotBeNull();
            client.Id.ShouldNotBeNull();
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.Client")]
        public void Cannot_insert_client_to_database_when_userid_is_not_provided()
        {
            var client = new Client()
            {
                UserId = "",
                Subscriptions = 
                {
                    new Subscription()
                    {
                        CreatedOn = new DateTime(2014, 6, 14, 6, 32, 0),
                        SubscriptionType = SubscriptionType.Enterprise,
                        SubscriptionSpan = SubscriptionSpan.Monthly
                    },
                    new Subscription()
                    {
                        CreatedOn = new DateTime(2016, 6, 14, 6, 32, 0),
                        SubscriptionType = SubscriptionType.Enterprise,
                        SubscriptionSpan = SubscriptionSpan.Annual
                    }
                }
            };
            typeof(BusinessException).ShouldBeThrownBy(() => clientService.CreateClient(client));     
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.Client")]
        public void Cannot_insert_client_to_database_when_subscription_is_not_provided()
        {
            var client = new Client()
            {
                UserId = "UserID",
                Subscriptions = {}
            };
            typeof(BusinessException).ShouldBeThrownBy(() => clientService.CreateClient(client));
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.Client")]
        public void Can_get_client_by_id()
        {
            var client = new Client()
            {
                UserId = "UserID",
                Subscriptions =
                {
                    new Subscription()
                    {
                        CreatedOn = new DateTime(2014, 6, 14, 6, 32, 0),
                        SubscriptionType = SubscriptionType.Enterprise,
                        SubscriptionSpan = SubscriptionSpan.Monthly
                    },
                    new Subscription()
                    {
                        CreatedOn = new DateTime(2016, 6, 14, 6, 32, 0),
                        SubscriptionType = SubscriptionType.Enterprise,
                        SubscriptionSpan = SubscriptionSpan.Annual
                    }
                }
            };
            clientService.CreateClient(client);

            client.ShouldNotBeNull();
            client.Id.ShouldNotBeNull();

            var clientFromDb = clientService.GetClient(client.Id.ToString());
            clientFromDb.ShouldNotBeNull();
            clientFromDb.Id.ShouldNotBeNull();
            clientFromDb.UserId.ShouldEqual("UserID");
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.Client")]
        public void Can_get_all_clients()
        {
            var client1 = new Client()
            {
                UserId = "UserID1",
                Subscriptions =
                {
                    new Subscription()
                    {
                        CreatedOn = new DateTime(2014, 6, 14, 6, 32, 0),
                        SubscriptionType = SubscriptionType.Enterprise,
                        SubscriptionSpan = SubscriptionSpan.Monthly
                    }
                }
            };
            var client2 = new Client()
            {
                UserId = "UserID2",
                Subscriptions =
                {
                    new Subscription()
                    {
                        CreatedOn = new DateTime(2016, 6, 14, 6, 32, 0),
                        SubscriptionType = SubscriptionType.Enterprise,
                        SubscriptionSpan = SubscriptionSpan.Annual
                    }
                }
            };
            clientService.CreateClient(client1);
            client1.ShouldNotBeNull();
            client1.Id.ShouldNotBeNull();

            clientService.CreateClient(client2);
            client2.ShouldNotBeNull();
            client2.Id.ShouldNotBeNull();

            var clients = clientService.GetAllClients();

            clients.ShouldBe<List<Client>>();
            (clients.Count > 0).ShouldBeTrue();
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.Client")]
        public void Can_update_client_to_database()
        {
            var client = new Client()
            {
                UserId = "UserID",
                Subscriptions =
                {
                    new Subscription()
                    {
                        CreatedOn = new DateTime(2014, 6, 14, 6, 32, 0),
                        SubscriptionType = SubscriptionType.Enterprise,
                        SubscriptionSpan = SubscriptionSpan.Monthly
                    },
                    new Subscription()
                    {
                        CreatedOn = new DateTime(2016, 6, 14, 6, 32, 0),
                        SubscriptionType = SubscriptionType.Enterprise,
                        SubscriptionSpan = SubscriptionSpan.Annual
                    }
                }
            };
            clientService.CreateClient(client);

            client.ShouldNotBeNull();
            client.Id.ShouldNotBeNull();

            client.UserId = "EditedUserID";
            clientService.UpdateClient(client);

            var clientFromDb = clientService.GetClient(client.Id.ToString());
            clientFromDb.ShouldNotBeNull();
            clientFromDb.UserId.ShouldEqual("EditedUserID");
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.Client")]
        public void Can_delete_client_from_database()
        {
            var client = new Client()
            {
                UserId = "UserID",
                Subscriptions =
                {
                    new Subscription()
                    {
                        CreatedOn = new DateTime(2014, 6, 14, 6, 32, 0),
                        SubscriptionType = SubscriptionType.Enterprise,
                        SubscriptionSpan = SubscriptionSpan.Monthly
                    },
                    new Subscription()
                    {
                        CreatedOn = new DateTime(2016, 6, 14, 6, 32, 0),
                        SubscriptionType = SubscriptionType.Enterprise,
                        SubscriptionSpan = SubscriptionSpan.Annual
                    }
                }
            };
            clientService.CreateClient(client);

            client.ShouldNotBeNull();
            client.Id.ShouldNotBeNull();

            clientService.DeleteClient(client.Id.ToString());
            var clientFromDb = clientService.GetClient(client.Id.ToString());

            clientFromDb.ShouldNotBeNull();
            clientFromDb.Id.ShouldEqual(ObjectId.Empty);
        }

        [TearDown]
        public void Dispose()
        {
            var clients = clientService.GetAllClients();
            foreach (var client in clients)
            {
                clientService.DeleteClient(client.Id.ToString());
            }
        }
    }
}
