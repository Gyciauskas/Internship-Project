using PresentConnection.Internship7.Iot.Domain;
using MongoDB.Bson;
using NUnit.Framework;
using System.Collections.Generic;
using PresentConnection.Internship7.Iot.Utils;
using PresentConnection.Internship7.Iot.BusinessContracts;
using BusinessImplementation;

namespace PresentConnection.Internship7.Iot.Tests
{
    /// <summary>
    /// This is for testing purpose created class
    /// </summary>
    class ClientConfiguration
    {
        public string IPaddress { get; set; }
    }

    public class ClientConnectionsTests
    {
        private IClientConnectionService clientconnservice;

        [SetUp]
        public void SetUp()
        {
            clientconnservice = new ClientConnectionService();
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.ClientConnection")]
        public void Can_insert_clientconnections_to_database()
        {
            // Create client
            var clientconnection = new ClientConnection()
            {
                ClientId = "1",
                ConnectionId = "A",
                IsDefault = true,
            };

            // This is for testing purpose created object
            ClientConfiguration clientconfiguration = new ClientConfiguration() { IPaddress = "0.0.0.1" };

            clientconnection.Configuration["1"] = clientconfiguration;

            // Insert to db
            clientconnservice.CreateClientConnection(clientconnection);

            // test result, if passed, than means validated successfully
            clientconnection.ShouldNotBeNull();
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.ClientConnection")]
        public void Cannot_insert_clientconnection_to_database_when_clientId_is_not_provided()
        {
            // Create client
            var clientconnection = new ClientConnection()
            {
                ClientId = "",
                ConnectionId = "A",
                IsDefault = true,
            };

            // This is for testing purpose created object
            ClientConfiguration clientconfiguration = new ClientConfiguration() { IPaddress = "1.0.0.1" };

            // Insert to db, it should fail
            typeof(BusinessException).ShouldBeThrownBy(() => clientconnservice.CreateClientConnection(clientconnection));
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.ClientConnection")]
        public void Cannot_insert_clientconnection_to_database_when_connectionId_is_not_provided()
        {
            // Create client
            var clientconnection = new ClientConnection()
            {
                ClientId = "1",
                ConnectionId = "",
                IsDefault = true,
            };

            // This is for testing purpose created object
            ClientConfiguration clientconfigurations = new ClientConfiguration() { IPaddress = "1.0.0.1" };

            // Insert to db, it should fail
            typeof(BusinessException).ShouldBeThrownBy(() => clientconnservice.CreateClientConnection(clientconnection));
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.ClientConnection")]
        public void Can_get_clientconnection_by_id()
        {
            // Create client
            var clientconnection = new ClientConnection()
            {
                ClientId = "1",
                ConnectionId = "A",
                IsDefault = true,
            };

            // This is for testing purpose created object
            ClientConfiguration clientconfiguration = new ClientConfiguration() { IPaddress = "0.0.0.1" };

            clientconnection.Configuration["1"] = clientconfiguration;

            // Insert to db
            clientconnservice.CreateClientConnection(clientconnection);

            clientconnection.ShouldNotBeNull();
            clientconnection.Id.ShouldNotBeNull();

            // Get the same document from db and check values
            var clientIdFromdb = clientconnservice.GetClientConnection(clientconnection.Id.ToString());

            clientIdFromdb.ShouldNotBeNull();
            clientIdFromdb.Id.ShouldNotBeNull();
            clientIdFromdb.ClientId.ShouldEqual("1");
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.ClientConnection")]
        public void Can_get_all_clientconnections()
        {
            // Create clients
            var clientconnection1 = new ClientConnection()
            {
                ClientId = "1",
                ConnectionId = "A",
                IsDefault = true,
            };

            // This is for testing purpose created object
            ClientConfiguration clientconfiguration = new ClientConfiguration() { IPaddress = "0.0.0.1" };

            clientconnection1.Configuration["1"] = clientconfiguration;

            var clientconnection2 = new ClientConnection()
            {
                ClientId = "2",
                ConnectionId = "B",
                IsDefault = true,
            };

            clientconfiguration = new ClientConfiguration() { IPaddress = "0.0.0.2" };

            clientconnection2.Configuration["2"] = clientconfiguration;

            // Insert to db
            clientconnservice.CreateClientConnection(clientconnection1);
            clientconnection1.ShouldNotBeNull();
            clientconnection1.Id.ShouldNotBeNull();

            clientconnservice.CreateClientConnection(clientconnection2);
            clientconnection2.ShouldNotBeNull();
            clientconnection2.Id.ShouldNotBeNull();

            // Get inserted documents
            var clientconnections = clientconnservice.GetAllClientConnections();

            clientconnections.ShouldBe<List<ClientConnection>>();
            (clientconnections.Count > 0).ShouldBeTrue();
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.ClientConnection")]
        public void Can_get_all_clientconnections_by_clientId()
        {
            // Create clients
            var clientconnection1 = new ClientConnection()
            {
                ClientId = "1",
                ConnectionId = "A",
                IsDefault = true,
            };

            // This is for testing purpose created object
            ClientConfiguration clientconfiguration = new ClientConfiguration() { IPaddress = "0.0.0.1" };

            clientconnection1.Configuration["1"] = clientconfiguration;

            var clientconnection2 = new ClientConnection()
            {
                ClientId = "2",
                ConnectionId = "B",
                IsDefault = true,
            };

            clientconfiguration = new ClientConfiguration() { IPaddress = "0.0.0.2" };

            clientconnection2.Configuration["2"] = clientconfiguration;

            var clientconnection3 = new ClientConnection()
            {
                ClientId = "3",
                ConnectionId = "C",
                IsDefault = false,
            };

            clientconfiguration = new ClientConfiguration() { IPaddress = "0.0.0.3" };

            clientconnection3.Configuration["3"] = clientconfiguration;

            // Insert to db
            clientconnservice.CreateClientConnection(clientconnection1);
            clientconnection1.ShouldNotBeNull();
            clientconnection1.Id.ShouldNotBeNull();

            clientconnservice.CreateClientConnection(clientconnection2);
            clientconnection2.ShouldNotBeNull();
            clientconnection2.Id.ShouldNotBeNull();

            clientconnservice.CreateClientConnection(clientconnection3);
            clientconnection3.ShouldNotBeNull();
            clientconnection3.Id.ShouldNotBeNull();

            // Get inserted documents and test 
            var clientconnections = clientconnservice.GetAllClientConnections("1");

            clientconnections.ShouldBe<List<ClientConnection>>();
            clientconnections.Count.ShouldEqual(1);
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.ClientConnection")]
        public void Can_update_clientconnection_to_database()
        {
            // Create clients
            var clientconnection = new ClientConnection()
            {
                ClientId = "1",
                ConnectionId = "A",
                IsDefault = true,
            };

            // This is for testing purpose created object
            ClientConfiguration clientconfiguration = new ClientConfiguration() { IPaddress = "0.0.0.1" };

            clientconnection.Configuration["1"] = clientconfiguration;

            // Insert to db
            clientconnservice.CreateClientConnection(clientconnection);
            clientconnection.ShouldNotBeNull();
            clientconnection.Id.ShouldNotBeNull();

            // update document
            clientconnection.ClientId = "2";
            clientconnservice.UpdateClientConnection(clientconnection);

            // Get the same document from db and check values
            var clientIdFromdb = clientconnservice.GetClientConnection(clientconnection.Id.ToString());
            clientIdFromdb.ShouldNotBeNull();
            clientIdFromdb.ClientId.ShouldEqual("2");
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.ClientConnection")]
        public void Can_delete_clientconnection_from_database()
        {
            // Create clients
            var clientconnection = new ClientConnection()
            {
                ClientId = "1",
                ConnectionId = "A",
                IsDefault = true,
            };

            // This is for testing purpose created object
            ClientConfiguration clientconfiguration = new ClientConfiguration() { IPaddress = "0.0.0.1" };

            clientconnection.Configuration["1"] = clientconfiguration;

            // Insert to db
            clientconnservice.CreateClientConnection(clientconnection);
            clientconnection.ShouldNotBeNull();
            clientconnection.Id.ShouldNotBeNull();

            // delete document
            var result = clientconnservice.DeleteClientConnection(clientconnection.Id.ToString());
            result.ShouldEqual(true);

            // Get item from db and check if name was updated
            var clientconnFromDb = clientconnservice.GetClientConnection(clientconnection.Id.ToString());

            // issue with CodeMash library. Should return null when not found - not default object
            clientconnFromDb.ShouldNotBeNull();
            clientconnFromDb.Id.ShouldEqual(ObjectId.Empty);
        }

        // After every test, delete all records from db
        [TearDown]
        public void Dispose()
        {
            var clientconnections = clientconnservice.GetAllClientConnections();
            foreach (var clientconnection in clientconnections)
            {
                clientconnservice.DeleteClientConnection(clientconnection.Id.ToString());
            }
        }
    }
}


