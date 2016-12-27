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
    public class ConnectionsTests
    {
        private IConnectionService connectionService;
        private Connection goodConnection;

        [SetUp]
        public void SetUp()
        {
            connectionService = new ConectionService();
            goodConnection = new Connection
            {
                UniqueName = "raspberry-pi-3",
                Name = "Dropbox",
                Images =
                {
                    "5821dcc11e9f341d4c6d0994"
                },
                Url = "url",
                Description = "description"
            };
        }

        [Test]
        [Category("IntegrationTests")]
        [Category("Connection")]
        public void Can_insert_connection_to_database()
        {
            connectionService.CreateConnection(goodConnection);

            goodConnection.ShouldNotBeNull();
            goodConnection.Id.ShouldNotBeNull();
        }

        [Test]
        [Category("IntegrationTests")]
        [Category("Connection")]
        public void Cannot_insert_connection_to_database_when_uniquename_is_not_provided()
        {
            goodConnection.UniqueName = string.Empty;

            var exception =typeof(BusinessException).ShouldBeThrownBy(() => connectionService.CreateConnection(goodConnection));
            exception.Message.ShouldEqual("Cannot create connection");
        }

        [Test]
        [Category("IntegrationTests")]
        [Category("Connection")]
        public void Cannot_insert_connection_to_database_when_such_uniquename_exist()
        {
            connectionService.CreateConnection(goodConnection);

            var connection2 = new Connection
            {
                UniqueName = "raspberry-pi-3",
                Name = "Dropbox2",
                Images =
                {
                    "5821dcc11e9f341d4c6d0994"
                },
                Url = "url",
                Description = "description"
            };

            var exception = typeof(BusinessException).ShouldBeThrownBy(() => connectionService.CreateConnection(connection2));

            var businessException = exception as BusinessException;
            businessException.ShouldNotBeNull();

            businessException?.Errors.SingleOrDefault(error => error.ErrorMessage.Equals("Property  should be unique in database and in correct format !"))
            .ShouldNotBeNull("Received different error message");

            exception.Message.ShouldEqual("Cannot create connection");
        }

        [Test]
        [Category("IntegrationTests")]
        [Category("Connection")]
        public void Cannot_insert_connection_to_database_when_uniquename_is_not_in_correct_format()
        {
            goodConnection.UniqueName = "Raspberry PI 3";

            var exception = typeof(BusinessException).ShouldBeThrownBy(() => connectionService.CreateConnection(goodConnection));

            var businessException = exception as BusinessException;
            businessException.ShouldNotBeNull();

            businessException?.Errors.SingleOrDefault(error => error.ErrorMessage.Equals("Property  should be unique in database and in correct format !"))
            .ShouldNotBeNull("Received different error message");

            exception.Message.ShouldEqual("Cannot create connection");
        }

        [Test]
        [Category("IntegrationTests")]
        [Category("Connection")]
        public void Cannot_insert_connection_to_database_when_uniquename_is_not_in_correct_format_unique_name_with_upercases()
        {
            goodConnection.UniqueName = "raspberry-PI-3";

            var exception = typeof(BusinessException).ShouldBeThrownBy(() => connectionService.CreateConnection(goodConnection));

            var businessException = exception as BusinessException;
            businessException.ShouldNotBeNull();

            businessException?.Errors.SingleOrDefault(error => error.ErrorMessage.Equals("Property  should be unique in database and in correct format !"))
            .ShouldNotBeNull("Received different error message");

            exception.Message.ShouldEqual("Cannot create connection");
        }

        [Test]
        [Category("IntegrationTests")]
        [Category("Connection")]
        public void Cannot_update_connection_to_database_when_such_uniquename_already_exist()
        {
            connectionService.CreateConnection(goodConnection);

            var connection = new Connection
            {
                UniqueName = "raspberry-pi-2",
                Name = "Dropbox2",
                Images =
                {
                    "5821dcc11e9f341d4c6d0994"
                },
                Url = "url",
                Description = "description"
            };
            connectionService.CreateConnection(connection);

            connection.ShouldNotBeNull();
            connection.Id.ShouldNotBeNull();

            connection.UniqueName = "raspberry-pi-3";

            var exception = typeof(BusinessException).ShouldBeThrownBy(() => connectionService.UpdateConnection(connection));

            var businessException = exception as BusinessException;
            businessException.ShouldNotBeNull();

            businessException?.Errors.SingleOrDefault(error => error.ErrorMessage.Equals("Property  should be unique in database and in correct format !"))
            .ShouldNotBeNull("Received different error message");

            exception.Message.ShouldEqual("Cannot update connection");
        }

        [Test]
        [Category("IntegrationTests")]
        [Category("Connection")]
        public void Cannot_insert_connection_to_database_when_name_is_not_provided()
        {
            goodConnection.Name = string.Empty;

            var exception = typeof(BusinessException).ShouldBeThrownBy(() => connectionService.CreateConnection(goodConnection));
            exception.Message.ShouldEqual("Cannot create connection");
        }

        [Test]
        [Category("IntegrationTests")]
        [Category("Connection")]
        public void Cannot_insert_connection_to_database_when_image_is_not_provided()
        {
            goodConnection.Images = null;
            var exception =  typeof(BusinessException).ShouldBeThrownBy(() => connectionService.CreateConnection(goodConnection));

            var businessException = exception as BusinessException;
            businessException.ShouldNotBeNull();
            businessException?.Errors.SingleOrDefault(error => error.ErrorMessage.Equals("Property Images should contain at least one item!"))
            .ShouldNotBeNull("Received different error message");

            exception.Message.ShouldEqual("Cannot create connection");
        }

        [Test]
        [Category("IntegrationTests")]
        [Category("Connection")]
        public void Cannot_insert_connection_to_database_when_url_is_not_provided()
        {
            goodConnection.Url = string.Empty;

            var exception = typeof(BusinessException).ShouldBeThrownBy(() => connectionService.CreateConnection(goodConnection));
            exception.Message.ShouldEqual("Cannot create connection");
        }

        [Test]
        [Category("IntegrationTests")]
        [Category("Connection")]
        public void Cannot_insert_connection_to_database_when_description_is_not_provided()
        {
           goodConnection.Description = string.Empty;

           var exception = typeof(BusinessException).ShouldBeThrownBy(() => connectionService.CreateConnection(goodConnection));
           exception.Message.ShouldEqual("Cannot create connection");
        }

        [Test]
        [Category("IntegrationTests")]
        [Category("Connection")]
        public void Can_get_connection_by_id()
        {
            connectionService.CreateConnection(goodConnection);

            goodConnection.ShouldNotBeNull();
            goodConnection.Id.ShouldNotBeNull();

            var connectionFromDb = connectionService.GetConnection(goodConnection.Id.ToString());
            connectionFromDb.ShouldNotBeNull();
            connectionFromDb.Id.ShouldNotBeNull();
            connectionFromDb.UniqueName.ShouldEqual("raspberry-pi-3");
        }

        [Test]
        [Category("IntegrationTests")]
        [Category("Connection")]
        public void Can_get_all_connections()
        {
            connectionService.CreateConnection(goodConnection);

            var connection2 = new Connection
            {
                UniqueName = "raspberry-pi-4",
                Name = "Dropbox",
                Images =
                {
                    "5821dcc11e9f341d4c6d0994"
                },
                Url = "url",
                Description = "description"
            };
           
            connectionService.CreateConnection(connection2);
            connection2.ShouldNotBeNull();
            connection2.Id.ShouldNotBeNull();

            var connections = connectionService.GetAllConnections();

            connections.ShouldBe<List<Connection>>();
            (connections.Count > 0).ShouldBeTrue();
        }

        [Test]
        [Category("IntegrationTests")]
        [Category("Connection")]
        public void Can_get_all_connections_by_name()
        {
            var connection1 = new Connection
            {
                UniqueName = "dropbox",
                Name = "Dropbox",
                Images =
                {
                    "5821dcc11e9f341d4c6d0994"
                },
                Url = "url1",
                Description = "description1"
            };

            var connection2 = new Connection
            {
                UniqueName = "sendgrid",
                Name = "SendGrid",
                Images =
                {
                    "5821dcc11e9f341d4c6d0994"
                },
                Url = "url2",
                Description = "description2"
            };

            connectionService.CreateConnection(connection1);
            connection1.ShouldNotBeNull();
            connection1.Id.ShouldNotBeNull();

            connectionService.CreateConnection(connection2);
            connection2.ShouldNotBeNull();
            connection2.Id.ShouldNotBeNull();

            var connections = connectionService.GetAllConnections("SendGrid");

            connections.ShouldBe<List<Connection>>();
            connections.Count.ShouldEqual(1);
        }

        [Test]
        [Category("IntegrationTests")]
        [Category("Connection")]
        public void Can_get_all_connections_by_case_insensetive_name()
        {
            var connection1 = new Connection
            {
                UniqueName = "dropbox",
                Name = "Dropbox",
                Images =
                {
                    "5821dcc11e9f341d4c6d0994"
                },
                Url = "url1",
                Description = "description1"
            };

            var connection2 = new Connection
            {
                UniqueName = "sendgrid",
                Name = "SendGrid",
                Images =
                {
                    "5821dcc11e9f341d4c6d0994"
                },
                Url = "url2",
                Description = "description2"
            };

            connectionService.CreateConnection(connection1);
            connection1.ShouldNotBeNull();
            connection1.Id.ShouldNotBeNull();

            connectionService.CreateConnection(connection2);
            connection2.ShouldNotBeNull();
            connection2.Id.ShouldNotBeNull();

            var connections = connectionService.GetAllConnections("sendGrid");

            connections.ShouldBe<List<Connection>>();
            connections.Count.ShouldEqual(1);
        }

        [Test]
        [Category("IntegrationTests")]
        [Category("Connection")]
        public void Can_get_all_connections_by_incomplete_name()
        {
            var connection1 = new Connection
            {
                UniqueName = "dropbox",
                Name = "Dropbox",
                Images =
                {
                    "5821dcc11e9f341d4c6d0994"
                },
                Url = "url1",
                Description = "description1"
            };

            var connection2 = new Connection
            {
                UniqueName = "sendgrid",
                Name = "SendGrid",
                Images =
                {
                    "5821dcc11e9f341d4c6d0994"
                },
                Url = "url2",
                Description = "description2"
            };

            connectionService.CreateConnection(connection1);
            connection1.ShouldNotBeNull();
            connection1.Id.ShouldNotBeNull();

            connectionService.CreateConnection(connection2);
            connection2.ShouldNotBeNull();
            connection2.Id.ShouldNotBeNull();

            var connections = connectionService.GetAllConnections("Send");

            connections.ShouldBe<List<Connection>>();
            connections.Count.ShouldEqual(1);
        }

        [Test]
        [Category("IntegrationTests")]
        [Category("Connection")]
        public void Can_update_connection_to_database()
        {
            connectionService.CreateConnection(goodConnection);

            goodConnection.ShouldNotBeNull();
            goodConnection.Id.ShouldNotBeNull();

            goodConnection.UniqueName = "raspberry-pi-4";
            connectionService.UpdateConnection(goodConnection);

            var connectionFromDb = connectionService.GetConnection(goodConnection.Id.ToString());
            connectionFromDb.ShouldNotBeNull();
            connectionFromDb.UniqueName.ShouldEqual("raspberry-pi-4");
        }

        [Test]
        [Category("IntegrationTests")]
        [Category("Connection")]
        public void Can_delete_connection_from_database()
        {
            connectionService.CreateConnection(goodConnection);

            goodConnection.ShouldNotBeNull();
            goodConnection.Id.ShouldNotBeNull();

            connectionService.DeleteConnection(goodConnection.Id.ToString());
            var connectionFromDb = connectionService.GetConnection(goodConnection.Id.ToString());

            connectionFromDb.ShouldNotBeNull();
            connectionFromDb.Id.ShouldEqual(ObjectId.Empty);
        }

        [TearDown]
        public void Dispose()
        {
            var connections = connectionService.GetAllConnections();
            foreach (var connection in connections)
            {
                connectionService.DeleteConnection(connection.Id.ToString());
            }
        }
    }
}
