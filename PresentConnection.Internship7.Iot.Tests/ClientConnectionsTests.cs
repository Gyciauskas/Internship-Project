using PresentConnection.Internship7.Iot.Domain;
using MongoDB.Bson;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using PresentConnection.Internship7.Iot.Utils;
using PresentConnection.Internship7.Iot.BusinessContracts;
using BusinessImplementation;
using CodeMash.Net;

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
        public void Can_insert_client_connection_to_database()
        {
            // Create client
            var clientconnection = new ClientConnection()
            {
                ClientId = "1",
                ConnectionId = "A",
                IsDefault = true
            };

            // This is for testing purpose created object
            ClientConfiguration clientconfiguration = new ClientConfiguration() { IPaddress = "0.0.0.1" };

            clientconnection.Configuration["1"] = clientconfiguration;

            // Insert to db
            clientconnservice.CreateClientConnection(clientconnection, "1");

            // test result, if passed, than means validated successfully
            clientconnection.ShouldNotBeNull();
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.ClientConnection")]
        public void Cannot_insert_when_code_user_wants_to_compromise_data_and_pass_different_client_id()
        {
            var clientconnection = new ClientConnection()
            {
                ClientId = "OtherClientId",
                ConnectionId = "A",
                IsDefault = true
            };

            // This is for testing purpose created object
            ClientConfiguration clientconfiguration = new ClientConfiguration() { IPaddress = "0.0.0.1" };

            var exception = typeof(BusinessException).ShouldBeThrownBy(() => clientconnservice.CreateClientConnection(clientconnection, "OtherClientId2"));

            var businessException = exception as BusinessException;
            businessException.ShouldNotBeNull();

            businessException?.Errors.SingleOrDefault(error => error.ErrorMessage.Equals("Access is denied"))
                .ShouldNotBeNull("Received different error message");

            exception.Message.ShouldEqual("Cannot create client connection");
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.ClientConnection")]
        public void Cannot_insert_client_connection_to_database_when_client_id_is_not_provided()
        {
            // Create client
            var clientconnection = new ClientConnection()
            {
                ClientId = "",
                ConnectionId = "A",
                IsDefault = true
            };

            // This is for testing purpose created object
            ClientConfiguration clientconfiguration = new ClientConfiguration() { IPaddress = "1.0.0.1" };

            // Insert to db, it should fail
            typeof(BusinessException).ShouldBeThrownBy(() => clientconnservice.CreateClientConnection(clientconnection, "15"));
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.ClientConnection")]
        public void Cannot_insert_client_connection_to_database_when_connection_id_is_not_provided()
        {
            // Create client
            var clientconnection = new ClientConnection()
            {
                ClientId = "1",
                ConnectionId = "",
                IsDefault = true
            };

            // This is for testing purpose created object
            ClientConfiguration clientconfigurations = new ClientConfiguration() { IPaddress = "1.0.0.1" };

            // Insert to db, it should fail
            typeof(BusinessException).ShouldBeThrownBy(() => clientconnservice.CreateClientConnection(clientconnection, "1"));
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.ClientConnection")]
        public void Can_get_client_connection_by_id()
        {
            // Create client
            var clientconnection = new ClientConnection()
            {
                ClientId = "1",
                ConnectionId = "A",
                IsDefault = true
            };

            // This is for testing purpose created object
            ClientConfiguration clientconfiguration = new ClientConfiguration() { IPaddress = "0.0.0.1" };

            clientconnection.Configuration["1"] = clientconfiguration;

            // Insert to db
            clientconnservice.CreateClientConnection(clientconnection, "1");

            clientconnection.ShouldNotBeNull();
            clientconnection.Id.ShouldNotBeNull();

            // Get the same document from db and check values
            var clientIdFromdb = clientconnservice.GetClientConnection(clientconnection.Id.ToString(), "1");

            clientIdFromdb.ShouldNotBeNull();
            clientIdFromdb.Id.ShouldNotBeNull();
            clientIdFromdb.ClientId.ShouldEqual("1");
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.ClientConnection")]
        public void Can_get_all_client_connections()
        {
            // Create clients
            var clientconnection1 = new ClientConnection()
            {
                ClientId = "1",
                ConnectionId = "A",
                IsDefault = true
            };

            // This is for testing purpose created object
            ClientConfiguration clientconfiguration = new ClientConfiguration() { IPaddress = "0.0.0.1" };

            clientconnection1.Configuration["1"] = clientconfiguration;

            var clientconnection2 = new ClientConnection()
            {
                ClientId = "2",
                ConnectionId = "B",
                IsDefault = true
            };

            clientconfiguration = new ClientConfiguration() { IPaddress = "0.0.0.2" };

            clientconnection2.Configuration["2"] = clientconfiguration;

            // Insert to db
            clientconnservice.CreateClientConnection(clientconnection1, "1");
            clientconnection1.ShouldNotBeNull();
            clientconnection1.Id.ShouldNotBeNull();

            clientconnservice.CreateClientConnection(clientconnection2, "2");
            clientconnection2.ShouldNotBeNull();
            clientconnection2.Id.ShouldNotBeNull();

            // Get inserted documents
            var clientconnections = clientconnservice.GetClientConnections("1", "1");

            clientconnections.ShouldBe<List<ClientConnection>>();
            (clientconnections.Count > 0).ShouldBeTrue();
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.ClientConnection")]
        public void Can_get_all_client_connections_by_clientId()
        {
            // Create clients
            var clientconnection1 = new ClientConnection()
            {
                ClientId = "1",
                ConnectionId = "A",
                IsDefault = true
            };

            // This is for testing purpose created object
            ClientConfiguration clientconfiguration = new ClientConfiguration() { IPaddress = "0.0.0.1" };

            clientconnection1.Configuration["1"] = clientconfiguration;

            var clientconnection2 = new ClientConnection()
            {
                ClientId = "2",
                ConnectionId = "B",
                IsDefault = true
            };

            clientconfiguration = new ClientConfiguration() { IPaddress = "0.0.0.2" };

            clientconnection2.Configuration["2"] = clientconfiguration;

            var clientconnection3 = new ClientConnection()
            {
                ClientId = "3",
                ConnectionId = "C",
                IsDefault = false
            };

            clientconfiguration = new ClientConfiguration() { IPaddress = "0.0.0.3" };

            clientconnection3.Configuration["3"] = clientconfiguration;

            // Insert to db
            clientconnservice.CreateClientConnection(clientconnection1, "1");
            clientconnection1.ShouldNotBeNull();
            clientconnection1.Id.ShouldNotBeNull();

            clientconnservice.CreateClientConnection(clientconnection2, "2");
            clientconnection2.ShouldNotBeNull();
            clientconnection2.Id.ShouldNotBeNull();

            clientconnservice.CreateClientConnection(clientconnection3, "3");
            clientconnection3.ShouldNotBeNull();
            clientconnection3.Id.ShouldNotBeNull();

            // Get inserted documents and test 
            var clientconnections = clientconnservice.GetClientConnections("1", "1");

            clientconnections.ShouldBe<List<ClientConnection>>();
            clientconnections.Count.ShouldEqual(1);
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.ClientConnection")]
        public void Cannot_get_other_client_connections()
        {
            // Create clients
            var clientconnection1 = new ClientConnection()
            {
                ClientId = "1",
                ConnectionId = "A",
                IsDefault = true
            };

            // This is for testing purpose created object
            ClientConfiguration clientconfiguration = new ClientConfiguration() { IPaddress = "0.0.0.1" };

            clientconnection1.Configuration["1"] = clientconfiguration;

            var clientconnection2 = new ClientConnection()
            {
                ClientId = "2",
                ConnectionId = "B",
                IsDefault = true
            };

            clientconfiguration = new ClientConfiguration() { IPaddress = "0.0.0.2" };

            clientconnection2.Configuration["2"] = clientconfiguration;

            var clientconnection3 = new ClientConnection()
            {
                ClientId = "3",
                ConnectionId = "C",
                IsDefault = false
            };

            clientconfiguration = new ClientConfiguration() { IPaddress = "0.0.0.3" };

            clientconnection3.Configuration["3"] = clientconfiguration;

            // Insert to db
            clientconnservice.CreateClientConnection(clientconnection1, "1");
            clientconnection1.ShouldNotBeNull();
            clientconnection1.Id.ShouldNotBeNull();

            clientconnservice.CreateClientConnection(clientconnection2, "2");
            clientconnection2.ShouldNotBeNull();
            clientconnection2.Id.ShouldNotBeNull();

            clientconnservice.CreateClientConnection(clientconnection3, "3");
            clientconnection3.ShouldNotBeNull();
            clientconnection3.Id.ShouldNotBeNull();

            var exception = typeof(BusinessException).ShouldBeThrownBy(() => clientconnservice.GetClientConnections("3", "3a"));

            var businessException = exception as BusinessException;
            businessException.ShouldNotBeNull();

            businessException?.Errors.SingleOrDefault(error => error.ErrorMessage.Equals("Access is denied"))
                .ShouldNotBeNull("Received different error message");

            exception.Message.ShouldEqual("You don't have permissions to get client connections");
        }
        
        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.ClientConnection")]
        public void Can_update_client_connection_to_database()
        {
            // Create clients
            var clientconnection = new ClientConnection()
            {
                ClientId = "1",
                ConnectionId = "A",
                IsDefault = true
            };

            // This is for testing purpose created object
            ClientConfiguration clientconfiguration = new ClientConfiguration() { IPaddress = "0.0.0.1" };

            clientconnection.Configuration["1"] = clientconfiguration;

            // Insert to db
            clientconnservice.CreateClientConnection(clientconnection, "1");
            clientconnection.ShouldNotBeNull();
            clientconnection.Id.ShouldNotBeNull();

            // update document
            clientconnection.ConnectionId = "B";
            clientconnservice.UpdateClientConnection(clientconnection, "1");

            // Get the same document from db and check values
            var clientIdFromdb = clientconnservice.GetClientConnection(clientconnection.Id.ToString(), "1");
            clientIdFromdb.ShouldNotBeNull();
            clientIdFromdb.ConnectionId.ShouldEqual("B");
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.ClientConnection")]
        public void Cannot_update_other_client_connection()
        {
            var clientconnection = new ClientConnection()
            {
                ClientId = "1",
                ConnectionId = "A",
                IsDefault = true
            };

            // This is for testing purpose created object
            ClientConfiguration clientconfiguration = new ClientConfiguration() { IPaddress = "0.0.0.1" };

            clientconnection.Configuration["1"] = clientconfiguration;

            clientconnservice.CreateClientConnection(clientconnection, "1");
            clientconnection.ShouldNotBeNull();
            clientconnection.Id.ShouldNotBeNull();

            var clientconnectionCompromised = new ClientConnection()
            {
                Id = clientconnection.Id,
                ClientId = "1",
                ConnectionId = "A",
                IsDefault = false
            };

            var exception = typeof(BusinessException).ShouldBeThrownBy(() => clientconnservice.UpdateClientConnection(clientconnectionCompromised, "2"));

            var businessException = exception.InnerException as BusinessException;
            businessException.ShouldNotBeNull();

            businessException?.Errors.SingleOrDefault(error => error.ErrorMessage.Equals("Access is denied"))
                .ShouldNotBeNull("Received different error message");

            exception.Message.ShouldEqual("You don't have permissions to update this client connection");
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.ClientConnection")]
        public void Can_delete_client_connection_from_database()
        {
            // Create clients
            var clientconnection = new ClientConnection()
            {
                ClientId = "1",
                ConnectionId = "A",
                IsDefault = true
            };

            // This is for testing purpose created object
            ClientConfiguration clientconfiguration = new ClientConfiguration() { IPaddress = "0.0.0.1" };

            clientconnection.Configuration["1"] = clientconfiguration;

            // Insert to db
            clientconnservice.CreateClientConnection(clientconnection, "1");
            clientconnection.ShouldNotBeNull();
            clientconnection.Id.ShouldNotBeNull();

            // delete document
            var isDeleted = clientconnservice.DeleteClientConnection(clientconnection.Id.ToString(), "1");
            isDeleted.ShouldEqual(true);
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.ClientConnection")]
        public void Cannot_delete_other_client_connection()
        {
            var clientconnection = new ClientConnection()
            {
                ClientId = "1",
                ConnectionId = "A",
                IsDefault = true
            };

            // This is for testing purpose created object
            ClientConfiguration clientconfiguration = new ClientConfiguration() { IPaddress = "0.0.0.1" };

            clientconnection.Configuration["1"] = clientconfiguration;

            // Somehow I stole Id
            var exception = typeof(BusinessException).ShouldBeThrownBy(() => clientconnservice.DeleteClientConnection(clientconnection.Id.ToString(), "Client2"));

            var businessException = exception.InnerException as BusinessException;
            businessException.ShouldNotBeNull();

            businessException?.Errors.SingleOrDefault(error => error.ErrorMessage.Equals("Access is denied"))
                .ShouldNotBeNull("Received different error message");

            exception.Message.ShouldEqual("You don't have permissions to delete this client connection");
        }

        // After every test, delete all records from db
        [TearDown]
        public void Dispose()
        {
            var clientConnections = Db.Find<ClientConnection>(_ => true);
            
            foreach (var clientConnection in clientConnections)
            {
                Db.DeleteOne<ClientConnection>(x => x.Id == clientConnection.Id);
            }
        }
    }
}


