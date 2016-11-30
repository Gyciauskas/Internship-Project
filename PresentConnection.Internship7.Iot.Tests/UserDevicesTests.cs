using System;
using MongoDB.Bson;
using NUnit.Framework;
using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.BusinessImplementation;
using PresentConnection.Internship7.Iot.Domain;
using System.Collections.Generic;
using PresentConnection.Internship7.Iot.Utils;

namespace PresentConnection.Internship7.Iot.Tests
{
    [TestFixture]
    public class UserDevicesTests
    {
        private IClientDeviceService _clientDeviceService;

        [SetUp]
        public void SetUp()
        {
            _clientDeviceService = new ClientDeviceService();
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.ClientDevice")]
        public void Can_insert_userDevice_to_database()
        {
            var userDevice = new ClientDevice()
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
            _clientDeviceService.CreateClientDevice(userDevice);

            userDevice.ShouldNotBeNull();
            userDevice.Id.ShouldNotBeNull();
        }


        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.ClientDevice")]
        public void Cannot_insert_userDevice_to_database_when_UserId_is_not_provided()
        {
            var userDevice = new ClientDevice()
            {
                ClientId = "",
                DeviceId = "11111",
                DeviceDisplayId = "Lukas11111",
                Latitude = "10",
                Longitude = "20",
                AuthKey1 = Guid.NewGuid().ToString(),
                AuthKey2 = Guid.NewGuid().ToString()
            };

            typeof(BusinessException).ShouldBeThrownBy(() => _clientDeviceService.CreateClientDevice(userDevice));
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.ClientDevice")]
        public void Cannot_insert_userDevice_to_database_when_DeviceId_is_not_provided()
        {
            var userDevice = new ClientDevice()
            {
                ClientId = "Lukas",
                DeviceId = "",
                DeviceDisplayId = "Lukas11111", // needs to be unique
                Latitude = "10",
                Longitude = "20",
                AuthKey1 = Guid.NewGuid().ToString(),
                AuthKey2 = Guid.NewGuid().ToString()
            };

            typeof(BusinessException).ShouldBeThrownBy(() => _clientDeviceService.CreateClientDevice(userDevice));
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.ClientDevice")]
        public void Cannot_insert_userDevice_to_database_when_DeviceDisplayId_is_not_provided()
        {
            var userDevice = new ClientDevice()
            {
                ClientId = "Lukas",
                DeviceId = "11111",
                DeviceDisplayId = "", // needs to be unique
                Latitude = "10",
                Longitude = "20",
                AuthKey1 = Guid.NewGuid().ToString(),
                AuthKey2 = Guid.NewGuid().ToString()
            };

            typeof(BusinessException).ShouldBeThrownBy(() => _clientDeviceService.CreateClientDevice(userDevice));
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.Device")]
        public void Cannot_insert_device_to_database_when_uniquename_is_not_unique()
        {
            var userDevice = new ClientDevice()
            {
                ClientId = "Lukas",
                DeviceId = "11111",
                DeviceDisplayId = "Lukas11111", // needs to be unique
                Latitude = "10",
                Longitude = "20",
                AuthKey1 = Guid.NewGuid().ToString(),
                AuthKey2 = Guid.NewGuid().ToString()
            };

            var userDevice2 = new ClientDevice()
            {
                ClientId = "Tadas",
                DeviceId = "22222",
                DeviceDisplayId = "Lukas11111", // needs to be unique
                Latitude = "5",
                Longitude = "5",
                AuthKey1 = Guid.NewGuid().ToString(),
                AuthKey2 = Guid.NewGuid().ToString()
            };

            _clientDeviceService.CreateClientDevice(userDevice);

            userDevice.ShouldNotBeNull();
            userDevice.Id.ShouldNotBeNull();

            typeof(BusinessException).ShouldBeThrownBy(() => _clientDeviceService.CreateClientDevice(userDevice2));
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.ClientDevice")]
        public void Cannot_insert_userDevice_to_database_when_Latitude_is_not_provided()
        {
            var userDevice = new ClientDevice()
            {
                ClientId = "Lukas",
                DeviceId = "11111",
                DeviceDisplayId = "Lukas11111", // needs to be unique
                Latitude = "",
                Longitude = "20",
                AuthKey1 = Guid.NewGuid().ToString(),
                AuthKey2 = Guid.NewGuid().ToString()
            };
            typeof(BusinessException).ShouldBeThrownBy(() => _clientDeviceService.CreateClientDevice(userDevice));
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.ClientDevice")]
        public void Cannot_insert_userDevice_to_database_when_Longtitude_is_not_provided()
        {
            var userDevice = new ClientDevice()
            {
                ClientId = "Lukas",
                DeviceId = "11111",
                DeviceDisplayId = "Lukas11111", // needs to be unique
                Latitude = "10",
                Longitude = "",
                AuthKey1 = Guid.NewGuid().ToString(),
                AuthKey2 = Guid.NewGuid().ToString()
            };
            typeof(BusinessException).ShouldBeThrownBy(() => _clientDeviceService.CreateClientDevice(userDevice));
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.ClientDevice")]
        public void Cannot_insert_userDevice_to_database_when_AuthKey1_is_not_provided()
        {
            var userDevice = new ClientDevice()
            {
                ClientId = "Lukas",
                DeviceId = "11111",
                DeviceDisplayId = "Lukas11111", // needs to be unique
                Latitude = "10",
                Longitude = "20",
                AuthKey1 = "",
                AuthKey2 = Guid.NewGuid().ToString()
            };
            typeof(BusinessException).ShouldBeThrownBy(() => _clientDeviceService.CreateClientDevice(userDevice));
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.ClientDevice")]
        public void Cannot_insert_userDevice_to_database_when_AuthKey2_is_not_provided()
        {
            var userDevice = new ClientDevice()
            {
                ClientId = "Lukas",
                DeviceId = "11111",
                DeviceDisplayId = "Lukas11111", // needs to be unique
                Latitude = "10",
                Longitude = "20",
                AuthKey1 = Guid.NewGuid().ToString(),
                AuthKey2 = ""
            };
            typeof(BusinessException).ShouldBeThrownBy(() => _clientDeviceService.CreateClientDevice(userDevice));
        }


        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.ClientDevice")]
        public void Can_get_userDevice_by_id()
        {
            var userDevice = new ClientDevice()
            {
                ClientId = "Lukas",
                DeviceId = "11111",
                DeviceDisplayId = "Lukas11111",
                Latitude = "10",
                Longitude = "20",
                AuthKey1 = Guid.NewGuid().ToString(),
                AuthKey2 = Guid.NewGuid().ToString()
            };
            _clientDeviceService.CreateClientDevice(userDevice);

            userDevice.ShouldNotBeNull();
            userDevice.Id.ShouldNotBeNull();

            var userDeviceFromDb = _clientDeviceService.GetClientDevice(userDevice.Id.ToString());
            userDeviceFromDb.ShouldNotBeNull();
            userDeviceFromDb.Id.ShouldNotBeNull();
            userDeviceFromDb.ClientId.ShouldEqual("Lukas");

        }


        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.ClientDevice")]
        public void Can_get_all_userDevice()
        {
            var userDevice1 = new ClientDevice()
            {
                ClientId = "Lukas",
                DeviceId = "11111",
                DeviceDisplayId = "Lukas11111", // needs to be unique
                Latitude = "10",
                Longitude = "20",
                AuthKey1 = Guid.NewGuid().ToString(),
                AuthKey2 = Guid.NewGuid().ToString()
            };

            var userDevice2 = new ClientDevice()
            {
                ClientId = "Tomas",
                DeviceId = "22222",
                DeviceDisplayId = "Tomas22222", // needs to be unique
                Latitude = "20",
                Longitude = "30",
                AuthKey1 = Guid.NewGuid().ToString(),
                AuthKey2 = Guid.NewGuid().ToString()
            };

            _clientDeviceService.CreateClientDevice(userDevice1);
            userDevice1.ShouldNotBeNull();
            userDevice1.Id.ShouldNotBeNull();


            _clientDeviceService.CreateClientDevice(userDevice2);
            userDevice2.ShouldNotBeNull();
            userDevice2.Id.ShouldNotBeNull();



            var userDevices = _clientDeviceService.GetAllClientDevices(string.Empty);

            userDevices.ShouldBe<List<ClientDevice>>();
            (userDevices.Count > 0).ShouldBeTrue();
        }


        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.ClientDevice")]
        public void Can_get_all_userDevice_by_name()
        {
            var userDevice1 = new ClientDevice()
            {
                ClientId = "Lukas",
                DeviceId = "11111",
                DeviceDisplayId = "Lukas11111",
                Latitude = "10",
                Longitude = "20",
                AuthKey1 = Guid.NewGuid().ToString(),
                AuthKey2 = Guid.NewGuid().ToString()
            };

            var userDevice2 = new ClientDevice()
            {
                ClientId = "Tomas",
                DeviceId = "22222",
                DeviceDisplayId = "Tomas22222", // needs to be unique
                Latitude = "20",
                Longitude = "30",
                AuthKey1 = Guid.NewGuid().ToString(),
                AuthKey2 = Guid.NewGuid().ToString()
            };

            var userDevice3 = new ClientDevice()
            {
                ClientId = "Tadas",
                DeviceId = "33333",
                DeviceDisplayId = "Tadas33333", // needs to be unique
                Latitude = "15",
                Longitude = "15",
                AuthKey1 = Guid.NewGuid().ToString(),
                AuthKey2 = Guid.NewGuid().ToString()
            };

            _clientDeviceService.CreateClientDevice(userDevice1);
            userDevice1.ShouldNotBeNull();
            userDevice1.Id.ShouldNotBeNull();


            _clientDeviceService.CreateClientDevice(userDevice2);
            userDevice2.ShouldNotBeNull();
            userDevice2.Id.ShouldNotBeNull();

            _clientDeviceService.CreateClientDevice(userDevice3);
            userDevice3.ShouldNotBeNull();
            userDevice3.Id.ShouldNotBeNull();
            
            var userDevices = _clientDeviceService.GetAllClientDevices("Tadas");

            userDevices.ShouldBe<List<ClientDevice>>();
            userDevices.Count.ShouldEqual(1);
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.UserDevices")]
        public void Can_update_userDevice_to_database()
        {
            var userDevice = new  ClientDevice()
            {
                ClientId = "Matas",
                DeviceId = "33333",
                DeviceDisplayId = "Matas33333",
                Latitude = "8",
                Longitude = "7",
                AuthKey1 = Guid.NewGuid().ToString(),
                AuthKey2 = Guid.NewGuid().ToString()
            };

            _clientDeviceService.CreateClientDevice(userDevice);

            // First insert userDevice to db
            userDevice.ShouldNotBeNull();
            userDevice.Id.ShouldNotBeNull();

            // Update name and send update to db
            userDevice.ClientId = "Matukas";
            _clientDeviceService.UpdateClientDevice(userDevice);

            // Get item from db and check if name was updated
            var userDevicesFromDb = _clientDeviceService.GetClientDevice(userDevice.Id.ToString());
            userDevicesFromDb.ShouldNotBeNull();
            userDevicesFromDb.ClientId.ShouldEqual("Matukas");
        }


        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.UserDevices")]
        public void Can_delete_userDevice_from_database()
        {
            var userDevice = new ClientDevice()
            {
                ClientId = "Lukas",
                DeviceId = "11111",
                DeviceDisplayId = "Lukas11111",
                Latitude = "10",
                Longitude = "20",
                AuthKey1 = Guid.NewGuid().ToString(),
                AuthKey2 = Guid.NewGuid().ToString()
            };

            _clientDeviceService.CreateClientDevice(userDevice);

            // First insert userDevice to db
            userDevice.ShouldNotBeNull();
            userDevice.Id.ShouldNotBeNull();

            // Delete userDevice from db
            _clientDeviceService.DeleteClientDevice(userDevice.Id.ToString());

            // Get item from db and check if name was updated
            var userDeviceFromDb = _clientDeviceService.GetClientDevice(userDevice.Id.ToString());

            // issue with CodeMash library. Should return null when not found - not default object
            userDeviceFromDb.ShouldNotBeNull();
            userDeviceFromDb.Id.ShouldEqual(ObjectId.Empty);
        }

        
        [TearDown]
        public void Dispose()
        {
            var userDevices = _clientDeviceService.GetAllClientDevices(string.Empty);
            foreach (var userDevice in userDevices)
            {
                _clientDeviceService.DeleteClientDevice(userDevice.Id.ToString());
            }
        }
    }
}
