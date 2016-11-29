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

        [SetUp]
        public void SetUp()
        {
            connectionService = new ConectionService();
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.Connection")]
        public void Can_insert_connection_to_database()
        {
            var connection = new Connection
            {
                UniqueName = "UNdropbox",
                Name = "Dropbox",
                Images =
                {
                    new DisplayImage()
                    {
                        Height = "600px",
                        ImageName = "DropboxLogo",
                        Width = "250px"
                    }
                },
                Url = "url",
                Description = "description"
            };

            connectionService.CreateConnection(connection);

            connection.ShouldNotBeNull();
            connection.Id.ShouldNotBeNull();
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.Connection")]
        public void Cannot_insert_connection_to_database_when_uniquename_is_not_provided()
        {
            var connection = new Connection
            {
                UniqueName = "",
                Name = "Dropbox",
                Images =
                {
                    new DisplayImage()
                    {
                        Height = "600px",
                        ImageName = "DropboxLogo",
                        Width = "250px"
                    }
                },
                Url = "url",
                Description = "description"
            };

            typeof(BusinessException).ShouldBeThrownBy(() => connectionService.CreateConnection(connection));
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.Connection")]
        public void Cannot_insert_connection_to_database_when_name_is_not_provided()
        {
            var connection = new Connection
            {
                UniqueName = "UNdropbox",
                Name = "",
                Images =
                {
                    new DisplayImage()
                    {
                        Height = "600px",
                        ImageName = "DropboxLogo",
                        Width = "250px"
                    }
                },
                Url = "url",
                Description = "description"
            };

            typeof(BusinessException).ShouldBeThrownBy(() => connectionService.CreateConnection(connection));
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.Connection")]
        public void Cannot_insert_connection_to_database_when_image_is_not_provided()
        {
            var connection = new Connection
            {
                UniqueName = "UNdropbox",
                Name = "Dropbox",
                Images = {},
                Url = "url",
                Description = "description"
            };

            typeof(BusinessException).ShouldBeThrownBy(() => connectionService.CreateConnection(connection));
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.Connection")]
        public void Cannot_insert_connection_to_database_when_url_is_not_provided()
        {
            var connection = new Connection
            {
                UniqueName = "UNdropbox",
                Name = "Dropbox",
                Images =
                {
                    new DisplayImage()
                    {
                        Height = "600px",
                        ImageName = "DropboxLogo",
                        Width = "250px"
                    }
                },
                Url = "",
                Description = "description"
            };

            typeof(BusinessException).ShouldBeThrownBy(() => connectionService.CreateConnection(connection));
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.Connection")]
        public void Cannot_insert_connection_to_database_when_description_is_not_provided()
        {
            var connection = new Connection
            {
                UniqueName = "UNdropbox",
                Name = "Dropbox",
                Images =
                {
                    new DisplayImage()
                    {
                        Height = "600px",
                        ImageName = "DropboxLogo",
                        Width = "250px"
                    }
                },
                Url = "url",
                Description = ""
            };

            typeof(BusinessException).ShouldBeThrownBy(() => connectionService.CreateConnection(connection));
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.Connection")]
        public void Can_get_connection_by_id()
        {
            var connection = new Connection
            {
                UniqueName = "UNdropbox",
                Name = "Dropbox",
                Images =
                {
                    new DisplayImage()
                    {
                        Height = "600px",
                        ImageName = "DropboxLogo",
                        Width = "250px"
                    }
                },
                Url = "url",
                Description = "description"
            };

            connectionService.CreateConnection(connection);

            connection.ShouldNotBeNull();
            connection.Id.ShouldNotBeNull();

            var connectionFromDb = connectionService.GetConnection(connection.Id.ToString());
            connectionFromDb.ShouldNotBeNull();
            connectionFromDb.Id.ShouldNotBeNull();
            connectionFromDb.UniqueName.ShouldEqual("UNdropbox");
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.Connection")]
        public void Can_get_all_connections()
        {
            var connection1 = new Connection
            {
                UniqueName = "UNdropbox",
                Name = "Dropbox",
                Images =
                {
                    new DisplayImage()
                    {
                        Height = "600px",
                        ImageName = "DropboxLogo",
                        Width = "250px"
                    }
                },
                Url = "url1",
                Description = "description1"
            };

            var connection2 = new Connection
            {
                UniqueName = "UNsendgrid",
                Name = "SendGrid",
                Images =
                {
                    new DisplayImage()
                    {
                        Height = "600px",
                        ImageName = "SendGridLogo",
                        Width = "250px"
                    }
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

            var connections = connectionService.GetAllConnections();

            connections.ShouldBe<List<Connection>>();
            (connections.Count > 0).ShouldBeTrue();
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.Connection")]
        public void Can_update_connection_to_database()
        {
            var connection = new Connection
            {
                UniqueName = "UNdropbox",
                Name = "Dropbox",
                Images =
                {
                    new DisplayImage()
                    {
                        Height = "600px",
                        ImageName = "DropboxLogo",
                        Width = "250px"
                    }
                },
                Url = "url",
                Description = "description"
            };

            connectionService.CreateConnection(connection);

            connection.ShouldNotBeNull();
            connection.Id.ShouldNotBeNull();

            connection.UniqueName = "UNedited";
            connectionService.UpdateConnection(connection);

            var connectionFromDb = connectionService.GetConnection(connection.Id.ToString());
            connectionFromDb.ShouldNotBeNull();
            connectionFromDb.UniqueName.ShouldEqual("UNedited");
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.Connection")]
        public void Can_delete_connection_from_database()
        {
            var connection = new Connection
            {
                UniqueName = "UNdropbox",
                Name = "Dropbox",
                Images =
                {
                    new DisplayImage()
                    {
                        Height = "600px",
                        ImageName = "DropboxLogo",
                        Width = "250px"
                    }
                },
                Url = "url",
                Description = "description"
            };

            connectionService.CreateConnection(connection);

            connection.ShouldNotBeNull();
            connection.Id.ShouldNotBeNull();

            connectionService.DeleteConnection(connection.Id.ToString());

            var connectionFromDb = connectionService.GetConnection(connection.Id.ToString());

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
