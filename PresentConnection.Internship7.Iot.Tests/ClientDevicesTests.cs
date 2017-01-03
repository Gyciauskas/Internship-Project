using System;
using MongoDB.Bson;
using NUnit.Framework;
using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.BusinessImplementation;
using PresentConnection.Internship7.Iot.Domain;
using System.Collections.Generic;
using System.Linq;
using PresentConnection.Internship7.Iot.Utils;
using CodeMash.Net;

namespace PresentConnection.Internship7.Iot.Tests
{
    [TestFixture]
    public class ClientDevicesTests
    {
        private IClientDeviceService clientDeviceService;
        private ClientDevice goodDevice;
        private ClientDevice goodDevice2;
        private ClientDevice goodDevice3;

        [SetUp]
        public void SetUp()
        {
            clientDeviceService = new ClientDeviceService();
            goodDevice = new ClientDevice
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

            goodDevice2 = new ClientDevice
            {
                ClientId = "Tomas",
                DeviceId = "22222",
                DeviceDisplayId = "Tomas22222", // needs to be unique
                Latitude = "20",
                Longitude = "30",
                AuthKey1 = Guid.NewGuid().ToString(),
                AuthKey2 = Guid.NewGuid().ToString()
            };

            goodDevice3 = new ClientDevice
            {
                ClientId = "Tadas",
                DeviceId = "33333",
                DeviceDisplayId = "Tadas33333", // needs to be unique
                Latitude = "15",
                Longitude = "15",
                AuthKey1 = Guid.NewGuid().ToString(),
                AuthKey2 = Guid.NewGuid().ToString()
            };
        }

        [Test]
        [Category("IntegrationTests")]
        [Category("ClientDevice")]
        public void Can_insert_client_device_to_database()
        {            
            clientDeviceService.CreateClientDevice(goodDevice, "Lukas");

            goodDevice.ShouldNotBeNull();
            goodDevice.Id.ShouldNotBeNull();
        }

        [Test]
        [Category("IntegrationTests")]
        [Category("ClientDevice")]
        public void Cannot_insert_when_code_user_wants_to_compromise_data_and_pass_different_client_id()
        {
            goodDevice.ClientId = "OtherClientId";

            var exception = typeof(BusinessException).ShouldBeThrownBy(() => clientDeviceService.CreateClientDevice(goodDevice, "OtherClientId2"));

            var businessException = exception as BusinessException;
            businessException.ShouldNotBeNull();

            businessException?.Errors.SingleOrDefault(error => error.ErrorMessage.Equals("Access is denied"))
                .ShouldNotBeNull("Received different error message");

            exception.Message.ShouldEqual("Cannot create client device");            
        }

        [Test]
        [Category("IntegrationTests")]
        [Category("ClientDevice")]
        public void Cannot_insert_client_device_to_database_when_client_id_is_not_provided()
        {
            goodDevice.ClientId = string.Empty;

            typeof(BusinessException).ShouldBeThrownBy(() => clientDeviceService.CreateClientDevice(goodDevice, "15"));
        }

        [Test]
        [Category("IntegrationTests")]
        [Category("ClientDevice")]
        public void Cannot_insert_client_device_to_database_when_device_id_is_not_provided()
        {
            goodDevice.DeviceId = string.Empty;

            typeof(BusinessException).ShouldBeThrownBy(() => clientDeviceService.CreateClientDevice(goodDevice, "Lukas"));
        }

        [Test]
        [Category("IntegrationTests")]
        [Category("ClientDevice")]
        public void Cannot_insert_client_device_to_database_when_device_display_id_is_not_provided()
        {
            goodDevice.DeviceId = string.Empty;

            typeof(BusinessException).ShouldBeThrownBy(() => clientDeviceService.CreateClientDevice(goodDevice, "Lukas"));
        }

        [Test]
        [Category("IntegrationTests")]
        [Category("Device")]
        public void Cannot_insert_client_device_to_database_when_uniquename_is_not_unique()
        {                       
            clientDeviceService.CreateClientDevice(goodDevice, "Lukas");

            goodDevice.ShouldNotBeNull();
            goodDevice.Id.ShouldNotBeNull();

            goodDevice2.DeviceDisplayId = "Lukas11111";

            typeof(BusinessException).ShouldBeThrownBy(() => clientDeviceService.CreateClientDevice(goodDevice2, "Tomas"));
        }

        [Test]
        [Category("IntegrationTests")]
        [Category("ClientDevice")]
        public void Cannot_insert_userDevice_to_database_when_latitude_is_not_provided()
        {
            goodDevice.Latitude = string.Empty;
            typeof(BusinessException).ShouldBeThrownBy(() => clientDeviceService.CreateClientDevice(goodDevice, "Lukas"));
        }

        [Test]
        [Category("IntegrationTests")]
        [Category("ClientDevice")]
        public void Cannot_insert_client_device_to_database_when_longtitude_is_not_provided()
        {
            goodDevice.Longitude = string.Empty;
            typeof(BusinessException).ShouldBeThrownBy(() => clientDeviceService.CreateClientDevice(goodDevice, "Lukas"));
        }

        [Test]
        [Category("IntegrationTests")]
        [Category("ClientDevice")]
        public void Cannot_insert_userDevice_to_database_when_AuthKey1_is_not_provided()
        {
            goodDevice.AuthKey1 = string.Empty;
            typeof(BusinessException).ShouldBeThrownBy(() => clientDeviceService.CreateClientDevice(goodDevice, "Lukas"));
        }

        [Test]
        [Category("IntegrationTests")]
        [Category("ClientDevice")]
        public void Cannot_insert_userDevice_to_database_when_AuthKey2_is_not_provided()
        {
            goodDevice.AuthKey2 = string.Empty;
            typeof(BusinessException).ShouldBeThrownBy(() => clientDeviceService.CreateClientDevice(goodDevice, "Lukas"));
        }

        [Test]
        [Category("IntegrationTests")]
        [Category("ClientDevice")]
        public void Can_get_userDevice_by_id()
        {            
            clientDeviceService.CreateClientDevice(goodDevice, "Lukas");

            goodDevice.ShouldNotBeNull();
            goodDevice.Id.ShouldNotBeNull();

            var userDeviceFromDb = clientDeviceService.GetClientDevice(goodDevice.Id.ToString(), "Lukas");
            userDeviceFromDb.ShouldNotBeNull();
            userDeviceFromDb.Id.ShouldNotBeNull();
            userDeviceFromDb.ClientId.ShouldEqual("Lukas");
        }

        [Test]
        [Category("IntegrationTests")]
        [Category("ClientDevice")]
        public void Can_get_all_client_devices()
        {
            clientDeviceService.CreateClientDevice(goodDevice, "Lukas");
            goodDevice.ShouldNotBeNull();
            goodDevice.Id.ShouldNotBeNull();

            clientDeviceService.CreateClientDevice(goodDevice2, "Tomas");
            goodDevice2.ShouldNotBeNull();
            goodDevice2.Id.ShouldNotBeNull();

            var userDevices = clientDeviceService.GetClientDevices("Lukas", "Lukas");

            userDevices.ShouldBe<List<ClientDevice>>();
            (userDevices.Count > 0).ShouldBeTrue();
        }

        [Test]
        [Category("IntegrationTests")]
        [Category("ClientDevice")]
        public void Can_get_client_devices_by_client_id()
        {
            clientDeviceService.CreateClientDevice(goodDevice, "Lukas");
            goodDevice.ShouldNotBeNull();
            goodDevice.Id.ShouldNotBeNull();

            clientDeviceService.CreateClientDevice(goodDevice2, "Tomas");
            goodDevice2.ShouldNotBeNull();
            goodDevice2.Id.ShouldNotBeNull();

            clientDeviceService.CreateClientDevice(goodDevice3, "Tadas");
            goodDevice3.ShouldNotBeNull();
            goodDevice3.Id.ShouldNotBeNull();
            
            var userDevices = clientDeviceService.GetClientDevices("Tadas", "Tadas");

            userDevices.ShouldBe<List<ClientDevice>>();
            userDevices.Count.ShouldEqual(1);
        }


        [Test]
        [Category("IntegrationTests")]
        [Category("ClientDevice")]
        public void Cannot_get_other_client_devices()
        {
            clientDeviceService.CreateClientDevice(goodDevice, "Lukas");
            goodDevice.ShouldNotBeNull();
            goodDevice.Id.ShouldNotBeNull();


            clientDeviceService.CreateClientDevice(goodDevice2, "Tomas");
            goodDevice2.ShouldNotBeNull();
            goodDevice2.Id.ShouldNotBeNull();

            clientDeviceService.CreateClientDevice(goodDevice3, "Tadas");
            goodDevice3.ShouldNotBeNull();
            goodDevice3.Id.ShouldNotBeNull();
            
            var exception = typeof(BusinessException).ShouldBeThrownBy(() => clientDeviceService.GetClientDevices("Tadas", "Tadas17"));

            var businessException = exception as BusinessException;
            businessException.ShouldNotBeNull();

            businessException?.Errors.SingleOrDefault(error => error.ErrorMessage.Equals("Access is denied"))
                .ShouldNotBeNull("Received different error message");

            exception.Message.ShouldEqual("You don't have permissions to get client devices");            
        }
        
        [Test]
        [Category("IntegrationTests")]
        [Category("ClientDevice")]
        public void Cannot_update_other_client_device()
        {
            // Somehow I stole Id
            clientDeviceService.CreateClientDevice(goodDevice, "Lukas");

            var userDeviceCompromised = new ClientDevice
            {
                Id = goodDevice.Id,
                ClientId = "Lukas",
                DeviceId = "11111",
                DeviceDisplayId = "Lukas11111",
                Latitude = "100000",
                Longitude = "2020000",
                AuthKey1 = Guid.NewGuid().ToString(),
                AuthKey2 = Guid.NewGuid().ToString()
            };

            var exception = typeof(BusinessException).ShouldBeThrownBy(() => clientDeviceService.UpdateClientDevice(userDeviceCompromised, "Client2"));

            var businessException = exception.InnerException as BusinessException;
            businessException.ShouldNotBeNull();

            businessException?.Errors.SingleOrDefault(error => error.ErrorMessage.Equals("Access is denied"))
                .ShouldNotBeNull("Received different error message");

            exception.Message.ShouldEqual("You don't have permissions to update this client device");
        }

        [Test]
        [Category("IntegrationTests")]
        [Category("UserDevices")]
        public void Can_delete_client_device_from_database()
        {            
            clientDeviceService.CreateClientDevice(goodDevice, "Lukas");

            // First insert userDevice to db
            goodDevice.ShouldNotBeNull();
            goodDevice.Id.ShouldNotBeNull();

            // Delete userDevice from db
            var isDeleted = clientDeviceService.DeleteClientDevice(goodDevice.Id.ToString(), "Lukas");
            isDeleted.ShouldEqual(true);
        }

        [Test]
        [Category("IntegrationTests")]
        [Category("UserDevices")]
        public void Cannot_delete_other_client_device()
        {
            // Somehow I stole Id
            var exception = typeof(BusinessException).ShouldBeThrownBy(() => clientDeviceService.DeleteClientDevice(goodDevice.Id.ToString(), "Client2"));

            var businessException = exception.InnerException as BusinessException;
            businessException.ShouldNotBeNull();

            businessException?.Errors.SingleOrDefault(error => error.ErrorMessage.Equals("Access is denied"))
                .ShouldNotBeNull("Received different error message");

            exception.Message.ShouldEqual("You don't have permissions to delete this client device");
        }
        
        [TearDown]
        public void Dispose()
        {
            var clientDevices = Db.Find<ClientDevice>(_ => true);
            foreach (var clientDevice in clientDevices)
            {
                Db.DeleteOne<ClientDevice>(x => x.Id == clientDevice.Id);
            }
        }
    }
}
