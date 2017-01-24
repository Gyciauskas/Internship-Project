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
    public partial class ClientDevicesTests
    {
        [Test]
        [Category("IntegrationTests")]
        [Category("ClientDevice")]
        public void Can_insert_client_device_to_database()
        {            
            clientDeviceService.CreateClientDevice(goodClientDevice, "Lukas");

            goodClientDevice.ShouldNotBeNull();
            goodClientDevice.Id.ShouldNotBeNull();
        }

        [Test]
        [Category("IntegrationTests")]
        [Category("ClientDevice")]
        public void Cannot_insert_when_code_user_wants_to_compromise_data_and_pass_different_client_id()
        {
            goodClientDevice.ClientId = "OtherClientId";

            var exception = typeof(BusinessException).ShouldBeThrownBy(() => clientDeviceService.CreateClientDevice(goodClientDevice, "OtherClientId2"));

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
            goodClientDevice.ClientId = string.Empty;

            typeof(BusinessException).ShouldBeThrownBy(() => clientDeviceService.CreateClientDevice(goodClientDevice, "15"));
        }

        [Test]
        [Category("IntegrationTests")]
        [Category("ClientDevice")]
        public void Cannot_insert_client_device_to_database_when_device_id_is_not_provided()
        {
            goodClientDevice.DeviceId = string.Empty;

            typeof(BusinessException).ShouldBeThrownBy(() => clientDeviceService.CreateClientDevice(goodClientDevice, "Lukas"));
        }

        [Test]
        [Category("IntegrationTests")]
        [Category("ClientDevice")]
        public void Cannot_insert_client_device_to_database_when_device_display_id_is_not_provided()
        {
            goodClientDevice.DeviceId = string.Empty;

            typeof(BusinessException).ShouldBeThrownBy(() => clientDeviceService.CreateClientDevice(goodClientDevice, "Lukas"));
        }

        [Test]
        [Category("IntegrationTests")]
        [Category("Device")]
        public void Cannot_insert_client_device_to_database_when_uniquename_is_not_unique()
        {                       
            clientDeviceService.CreateClientDevice(goodClientDevice, "Lukas");

            goodClientDevice.ShouldNotBeNull();
            goodClientDevice.Id.ShouldNotBeNull();

            goodClientDevice2.DeviceDisplayId = "Lukas11111";

            typeof(BusinessException).ShouldBeThrownBy(() => clientDeviceService.CreateClientDevice(goodClientDevice2, "Tomas"));
        }

        [Test]
        [Category("IntegrationTests")]
        [Category("ClientDevice")]
        public void Cannot_insert_userDevice_to_database_when_latitude_is_not_provided()
        {
            goodClientDevice.Latitude = string.Empty;
            typeof(BusinessException).ShouldBeThrownBy(() => clientDeviceService.CreateClientDevice(goodClientDevice, "Lukas"));
        }

        [Test]
        [Category("IntegrationTests")]
        [Category("ClientDevice")]
        public void Cannot_insert_client_device_to_database_when_longtitude_is_not_provided()
        {
            goodClientDevice.Longitude = string.Empty;
            typeof(BusinessException).ShouldBeThrownBy(() => clientDeviceService.CreateClientDevice(goodClientDevice, "Lukas"));
        }

        [Test]
        [Category("IntegrationTests")]
        [Category("ClientDevice")]
        public void Cannot_insert_userDevice_to_database_when_AuthKey1_is_not_provided()
        {
            goodClientDevice.AuthKey1 = string.Empty;
            typeof(BusinessException).ShouldBeThrownBy(() => clientDeviceService.CreateClientDevice(goodClientDevice, "Lukas"));
        }

        [Test]
        [Category("IntegrationTests")]
        [Category("ClientDevice")]
        public void Cannot_insert_userDevice_to_database_when_AuthKey2_is_not_provided()
        {
            goodClientDevice.AuthKey2 = string.Empty;
            typeof(BusinessException).ShouldBeThrownBy(() => clientDeviceService.CreateClientDevice(goodClientDevice, "Lukas"));
        }

        [Test]
        [Category("IntegrationTests")]
        [Category("ClientDevice")]
        public void Can_get_userDevice_by_id()
        {            
            clientDeviceService.CreateClientDevice(goodClientDevice, "Lukas");

            goodClientDevice.ShouldNotBeNull();
            goodClientDevice.Id.ShouldNotBeNull();

            var userDeviceFromDb = clientDeviceService.GetClientDevice(goodClientDevice.Id.ToString(), "Lukas");
            userDeviceFromDb.ShouldNotBeNull();
            userDeviceFromDb.Id.ShouldNotBeNull();
            userDeviceFromDb.ClientId.ShouldEqual("Lukas");
        }

        [Test]
        [Category("IntegrationTests")]
        [Category("ClientDevice")]
        public void Can_get_all_client_devices()
        {
            clientDeviceService.CreateClientDevice(goodClientDevice, "Lukas");
            goodClientDevice.ShouldNotBeNull();
            goodClientDevice.Id.ShouldNotBeNull();

            clientDeviceService.CreateClientDevice(goodClientDevice2, "Tomas");
            goodClientDevice2.ShouldNotBeNull();
            goodClientDevice2.Id.ShouldNotBeNull();

            var userDevices = clientDeviceService.GetClientDevices("Lukas", "Lukas");

            userDevices.ShouldBe<List<ClientDevice>>();
            (userDevices.Count > 0).ShouldBeTrue();
        }

        [Test]
        [Category("IntegrationTests")]
        [Category("ClientDevice")]
        public void Can_get_client_devices_by_client_id()
        {
            clientDeviceService.CreateClientDevice(goodClientDevice, "Lukas");
            goodClientDevice.ShouldNotBeNull();
            goodClientDevice.Id.ShouldNotBeNull();

            clientDeviceService.CreateClientDevice(goodClientDevice2, "Tomas");
            goodClientDevice2.ShouldNotBeNull();
            goodClientDevice2.Id.ShouldNotBeNull();

            clientDeviceService.CreateClientDevice(goodClientDevice3, "Tadas");
            goodClientDevice3.ShouldNotBeNull();
            goodClientDevice3.Id.ShouldNotBeNull();
            
            var userDevices = clientDeviceService.GetClientDevices("Tadas", "Tadas");

            userDevices.ShouldBe<List<ClientDevice>>();
            userDevices.Count.ShouldEqual(1);
        }


        [Test]
        [Category("IntegrationTests")]
        [Category("ClientDevice")]
        public void Cannot_get_other_client_devices()
        {
            clientDeviceService.CreateClientDevice(goodClientDevice, "Lukas");
            goodClientDevice.ShouldNotBeNull();
            goodClientDevice.Id.ShouldNotBeNull();


            clientDeviceService.CreateClientDevice(goodClientDevice2, "Tomas");
            goodClientDevice2.ShouldNotBeNull();
            goodClientDevice2.Id.ShouldNotBeNull();

            clientDeviceService.CreateClientDevice(goodClientDevice3, "Tadas");
            goodClientDevice3.ShouldNotBeNull();
            goodClientDevice3.Id.ShouldNotBeNull();
            
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
            clientDeviceService.CreateClientDevice(goodClientDevice, "Lukas");

            var userDeviceCompromised = new ClientDevice
            {
                Id = goodClientDevice.Id,
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
            clientDeviceService.CreateClientDevice(goodClientDevice, "Lukas");

            // First insert userDevice to db
            goodClientDevice.ShouldNotBeNull();
            goodClientDevice.Id.ShouldNotBeNull();

            // Delete userDevice from db
            var isDeleted = clientDeviceService.DeleteClientDevice(goodClientDevice.Id.ToString(), "Lukas");
            isDeleted.ShouldEqual(true);
        }

        [Test]
        [Category("IntegrationTests")]
        [Category("UserDevices")]
        public void Cannot_delete_other_client_device()
        {
            // Somehow I stole Id
            var exception = typeof(BusinessException).ShouldBeThrownBy(() => clientDeviceService.DeleteClientDevice(goodClientDevice.Id.ToString(), "Client2"));

            var businessException = exception.InnerException as BusinessException;
            businessException.ShouldNotBeNull();

            businessException?.Errors.SingleOrDefault(error => error.ErrorMessage.Equals("Access is denied"))
                .ShouldNotBeNull("Received different error message");

            exception.Message.ShouldEqual("You don't have permissions to delete this client device");
        }
    }
}
