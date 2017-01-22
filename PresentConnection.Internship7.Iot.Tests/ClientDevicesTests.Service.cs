using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using MongoDB.Bson;
using NSubstitute;
using NSubstitute.Core.Arguments;
using NUnit.Framework;
using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.BusinessImplementation;
using PresentConnection.Internship7.Iot.Domain;
using PresentConnection.Internship7.Iot.ServiceModels;
using PresentConnection.Internship7.Iot.Services;
using ServiceStack;
using CodeMash.Net;
using System;

namespace PresentConnection.Internship7.Iot.Tests
{
    [TestFixture]
    public partial class ClientDevicesTests : TestsBase
    {
        private ServiceStackHost appHost;
        private IClientDeviceService clientDeviceService;
        private IClientDeviceService clientDeviceServiceMock;
        private ClientDevice goodClientDevice;
        private ClientDevice goodClientDevice2;
        private ClientDevice goodClientDevice3;

        private void SetupMocks()
        {
            clientDeviceServiceMock = Substitute.For<IClientDeviceService>();

            // when pass any parameter (Id) to clientDeviceService.GetClientDevice, return goodClientDevice with passed id
            clientDeviceServiceMock.GetClientDevice(Arg.Any<string>(), Arg.Any<string>()).Returns(info =>
            {
                goodClientDevice.Id = ObjectId.Parse(info.ArgAt<string>(0));
                return goodClientDevice;
            });

            // when we are trying get clientDevices, just return generated list
            clientDeviceServiceMock.GetClientDevices(string.Empty, string.Empty).Returns(info =>
            {
                var list = new List<ClientDevice>();

                goodClientDevice.Id = ObjectId.GenerateNewId();
                list.Add(goodClientDevice);

                var clientDevice1 = new ClientDevice
                {
                    //UserId, DeviceId, DeviceDisplayId, Latitude, Longitude, AuthKey1, AuthKey2 mandatory fields
                    ClientId = "Lukas",
                    DeviceId = "11111",
                    DeviceDisplayId = "Lukas11111",
                    Latitude = "10",
                    Longitude = "20",
                    AuthKey1 = Guid.NewGuid().ToString(),
                    AuthKey2 = Guid.NewGuid().ToString()
                };
                clientDevice1.Id = ObjectId.GenerateNewId();
                list.Add(clientDevice1);

                var clientDevice2 = new ClientDevice
                {
                    ClientId = "Tomas",
                    DeviceId = "22222",
                    DeviceDisplayId = "Tomas22222", // needs to be unique
                    Latitude = "20",
                    Longitude = "30",
                    AuthKey1 = Guid.NewGuid().ToString(),
                    AuthKey2 = Guid.NewGuid().ToString()
                };
                clientDevice2.Id = ObjectId.GenerateNewId();
                list.Add(clientDevice2);

                var clientDevice3 = new ClientDevice
                {
                    ClientId = "Tadas",
                    DeviceId = "33333",
                    DeviceDisplayId = "Tadas33333", // needs to be unique
                    Latitude = "15",
                    Longitude = "15",
                    AuthKey1 = Guid.NewGuid().ToString(),
                    AuthKey2 = Guid.NewGuid().ToString()
                };
                clientDevice3.Id = ObjectId.GenerateNewId();
                list.Add(clientDevice3);

                return list;
            });
        }

        [SetUp]
        public void SetUp()
        {
            //Start your AppHost on TestFixtureSetUp
            appHost = new AppHost()
                .Init()
                .Start(BaseUri);

            clientDeviceService = new ClientDeviceService();

            // Create client device
            goodClientDevice = new ClientDevice
            {
                //UserId, DeviceId, DeviceDisplayId, Latitude, Longitude, AuthKey1, AuthKey2 mandatory fields
                ClientId = "Lukas",
                DeviceId = "11111",
                DeviceDisplayId = "Lukas11111",
                Latitude = "10",
                Longitude = "20",
                AuthKey1 = Guid.NewGuid().ToString(),
                AuthKey2 = Guid.NewGuid().ToString()
            };

            goodClientDevice2 = new ClientDevice
            {
                ClientId = "Tomas",
                DeviceId = "22222",
                DeviceDisplayId = "Tomas22222", // needs to be unique
                Latitude = "20",
                Longitude = "30",
                AuthKey1 = Guid.NewGuid().ToString(),
                AuthKey2 = Guid.NewGuid().ToString()
            };

            goodClientDevice3 = new ClientDevice
            {
                ClientId = "Tadas",
                DeviceId = "33333",
                DeviceDisplayId = "Tadas33333", // needs to be unique
                Latitude = "15",
                Longitude = "15",
                AuthKey1 = Guid.NewGuid().ToString(),
                AuthKey2 = Guid.NewGuid().ToString()
            };

            SetupMocks();
        }

        [Test]
        [Category("ClientDevice")]
        public void Can_do_CRUD_clientDevice_to_database()
        {
            var client = new JsonServiceClient(BaseUri);

            // Create request
            var createRequest = new AddDeviceToClient
            {
                DeviceId = goodClientDevice.DeviceId,
                ClientId = goodClientDevice.ClientId,
            };

            var createClientDeviceResponse = client.Post(createRequest);
            createClientDeviceResponse.ShouldNotBeNull();
            createClientDeviceResponse.Result.ShouldNotBeNull();
            createClientDeviceResponse.Result.Id.ShouldNotBeNull();
            createClientDeviceResponse.Result.Id.ShouldBeNotBeTheSameAs(ObjectId.Empty);

            var createRequest2 = new AddDeviceToClient
            {
                DeviceId = goodClientDevice2.DeviceId,
                ClientId = goodClientDevice2.ClientId,
            };

            var createClientDeviceResponse2 = client.Post(createRequest2);
            createClientDeviceResponse2.ShouldNotBeNull();
            createClientDeviceResponse2.Result.ShouldNotBeNull();
            createClientDeviceResponse2.Result.Id.ShouldNotBeNull();
            createClientDeviceResponse2.Result.Id.ShouldBeNotBeTheSameAs(ObjectId.Empty);

            var createRequest3 = new AddDeviceToClient
            {
                DeviceId = goodClientDevice3.DeviceId,
                ClientId = goodClientDevice3.ClientId,
            };

            var createClientDeviceResponse3 = client.Post(createRequest3);
            createClientDeviceResponse3.ShouldNotBeNull();
            createClientDeviceResponse3.Result.ShouldNotBeNull();
            createClientDeviceResponse3.Result.Id.ShouldNotBeNull();
            createClientDeviceResponse3.Result.Id.ShouldBeNotBeTheSameAs(ObjectId.Empty);

            // Get all
            var getRequests = new GetClientDevices
            {
            };

            var getClientDevicesResponse = client.Get(getRequests);
            getClientDevicesResponse.ShouldNotBeNull();
            getClientDevicesResponse.Result.ShouldNotBeNull();
            //getClientDevicesResponse.Result.Count.ShouldEqual(3);

            // Update
            var updateRequest = new UpdateClientDevice
            {
                Id = createClientDeviceResponse.Result.Id.ToString(),
            };

            var updateResponse = client.Put(updateRequest);
            updateResponse.ShouldNotBeNull();
            updateResponse.Result.ShouldNotBeNull();

            // Get by Id
            var getByIdRequest = new GetClientDevice
            {
                Id = createClientDeviceResponse.Result.Id.ToString()
            };

            var getByIdResponse = client.Get(getByIdRequest);
            getByIdResponse.ShouldNotBeNull();
            getByIdResponse.Result.ShouldNotBeNull();
            getByIdResponse.Result.DeviceId.ShouldEqual(createClientDeviceResponse.Result.DeviceId + "-updated");
            getByIdResponse.Result.ClientId.ShouldEqual(createClientDeviceResponse.Result.ClientId + "Updated");

            // Delete
            var deleteRequest = new DeleteClientDevice
            {
                Id = createClientDeviceResponse.Result.Id.ToString()
            };

            var deleteResponse = client.Delete(deleteRequest);
            deleteResponse.ShouldNotBeNull();
            deleteResponse.Result.ShouldNotBeNull();
            deleteResponse.Result.ShouldEqual(true);
        }

        // After every test, delete all records from db
        [TearDown]
        public void Dispose()
        {
            appHost.Dispose();

            var clientDevices = Db.Find<ClientDevice>(_ => true);

            foreach (var clientDevice in clientDevices)
            {
                Db.DeleteOne<ClientDevice>(x => x.Id == clientDevice.Id);
            }
        }
    }
}
