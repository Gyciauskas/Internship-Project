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

        [SetUp]
        public void SetUp()
        {
            clientDeviceService = new ClientDeviceService();
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.ClientDevice")]
        public void Can_insert_client_device_to_database()
        {
            var userDevice = new ClientDevice
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
            clientDeviceService.CreateClientDevice(userDevice, "Lukas");

            userDevice.ShouldNotBeNull();
            userDevice.Id.ShouldNotBeNull();
        }


        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.ClientDevice")]
        public void Cannot_insert_when_code_user_wants_to_compromise_data_and_pass_different_client_id()
        {
            var userDevice = new ClientDevice
            {
                ClientId = "OtherClientId",
                DeviceId = "11111",
                DeviceDisplayId = "Lukas11111",
                Latitude = "10",
                Longitude = "20",
                AuthKey1 = Guid.NewGuid().ToString(),
                AuthKey2 = Guid.NewGuid().ToString()
            };

            var exception = typeof(BusinessException).ShouldBeThrownBy(() => clientDeviceService.CreateClientDevice(userDevice, "OtherClientId2"));

            var businessException = exception as BusinessException;
            businessException.ShouldNotBeNull();

            businessException?.Errors.SingleOrDefault(error => error.ErrorMessage.Equals("Access is denied"))
                .ShouldNotBeNull("Received different error message");

            exception.Message.ShouldEqual("Cannot create client device");
            
        }
        

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.ClientDevice")]
        public void Cannot_insert_client_device_to_database_when_client_id_is_not_provided()
        {
            var userDevice = new ClientDevice
            {
                ClientId = "",
                DeviceId = "11111",
                DeviceDisplayId = "Lukas11111",
                Latitude = "10",
                Longitude = "20",
                AuthKey1 = Guid.NewGuid().ToString(),
                AuthKey2 = Guid.NewGuid().ToString()
            };

            typeof(BusinessException).ShouldBeThrownBy(() => clientDeviceService.CreateClientDevice(userDevice, "15"));
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.ClientDevice")]
        public void Cannot_insert_client_device_to_database_when_device_id_is_not_provided()
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

            typeof(BusinessException).ShouldBeThrownBy(() => clientDeviceService.CreateClientDevice(userDevice, "Lukas"));
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.ClientDevice")]
        public void Cannot_insert_client_device_to_database_when_device_display_id_is_not_provided()
        {
            var userDevice = new ClientDevice
            {
                ClientId = "Lukas",
                DeviceId = "11111",
                DeviceDisplayId = "", // needs to be unique
                Latitude = "10",
                Longitude = "20",
                AuthKey1 = Guid.NewGuid().ToString(),
                AuthKey2 = Guid.NewGuid().ToString()
            };

            typeof(BusinessException).ShouldBeThrownBy(() => clientDeviceService.CreateClientDevice(userDevice, "Lukas"));
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.Device")]
        public void Cannot_insert_client_device_to_database_when_uniquename_is_not_unique()
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

            clientDeviceService.CreateClientDevice(userDevice, "Lukas");

            userDevice.ShouldNotBeNull();
            userDevice.Id.ShouldNotBeNull();

            typeof(BusinessException).ShouldBeThrownBy(() => clientDeviceService.CreateClientDevice(userDevice2, "Tadas"));
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.ClientDevice")]
        public void Cannot_insert_userDevice_to_database_when_latitude_is_not_provided()
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
            typeof(BusinessException).ShouldBeThrownBy(() => clientDeviceService.CreateClientDevice(userDevice, "Lukas"));
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.ClientDevice")]
        public void Cannot_insert_client_device_to_database_when_longtitude_is_not_provided()
        {
            var userDevice = new ClientDevice
            {
                ClientId = "Lukas",
                DeviceId = "11111",
                DeviceDisplayId = "Lukas11111", // needs to be unique
                Latitude = "10",
                Longitude = "",
                AuthKey1 = Guid.NewGuid().ToString(),
                AuthKey2 = Guid.NewGuid().ToString()
            };
            typeof(BusinessException).ShouldBeThrownBy(() => clientDeviceService.CreateClientDevice(userDevice, "Lukas"));
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
            typeof(BusinessException).ShouldBeThrownBy(() => clientDeviceService.CreateClientDevice(userDevice, "Lukas"));
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
            typeof(BusinessException).ShouldBeThrownBy(() => clientDeviceService.CreateClientDevice(userDevice, "Lukas"));
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
            clientDeviceService.CreateClientDevice(userDevice, "Lukas");

            userDevice.ShouldNotBeNull();
            userDevice.Id.ShouldNotBeNull();

            var userDeviceFromDb = clientDeviceService.GetClientDevice(userDevice.Id.ToString(), "Lukas");
            userDeviceFromDb.ShouldNotBeNull();
            userDeviceFromDb.Id.ShouldNotBeNull();
            userDeviceFromDb.ClientId.ShouldEqual("Lukas");

        }


        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.ClientDevice")]
        public void Can_get_all_client_devices()
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

            clientDeviceService.CreateClientDevice(userDevice1, "Lukas");
            userDevice1.ShouldNotBeNull();
            userDevice1.Id.ShouldNotBeNull();


            clientDeviceService.CreateClientDevice(userDevice2, "Tomas");
            userDevice2.ShouldNotBeNull();
            userDevice2.Id.ShouldNotBeNull();



            var userDevices = clientDeviceService.GetClientDevices("Lukas", "Lukas");

            userDevices.ShouldBe<List<ClientDevice>>();
            (userDevices.Count > 0).ShouldBeTrue();
        }


        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.ClientDevice")]
        public void Can_get_client_devices_by_client_id()
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

            clientDeviceService.CreateClientDevice(userDevice1, "Lukas");
            userDevice1.ShouldNotBeNull();
            userDevice1.Id.ShouldNotBeNull();


            clientDeviceService.CreateClientDevice(userDevice2, "Tomas");
            userDevice2.ShouldNotBeNull();
            userDevice2.Id.ShouldNotBeNull();

            clientDeviceService.CreateClientDevice(userDevice3, "Tadas");
            userDevice3.ShouldNotBeNull();
            userDevice3.Id.ShouldNotBeNull();
            
            var userDevices = clientDeviceService.GetClientDevices("Tadas", "Tadas");

            userDevices.ShouldBe<List<ClientDevice>>();
            userDevices.Count.ShouldEqual(1);
        }


        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.ClientDevice")]
        public void Cannot_get_other_client_devices()
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

            clientDeviceService.CreateClientDevice(userDevice1, "Lukas");
            userDevice1.ShouldNotBeNull();
            userDevice1.Id.ShouldNotBeNull();


            clientDeviceService.CreateClientDevice(userDevice2, "Tomas");
            userDevice2.ShouldNotBeNull();
            userDevice2.Id.ShouldNotBeNull();

            clientDeviceService.CreateClientDevice(userDevice3, "Tadas");
            userDevice3.ShouldNotBeNull();
            userDevice3.Id.ShouldNotBeNull();
            
            var exception = typeof(BusinessException).ShouldBeThrownBy(() => clientDeviceService.GetClientDevices("Tadas", "Tadas17"));

            var businessException = exception as BusinessException;
            businessException.ShouldNotBeNull();

            businessException?.Errors.SingleOrDefault(error => error.ErrorMessage.Equals("Access is denied"))
                .ShouldNotBeNull("Received different error message");

            exception.Message.ShouldEqual("You don't have permissions to get client devices");
            
        }
        
        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.ClientDevice")]
        public void Cannot_update_other_client_device()
        {
            var userDevice = new ClientDevice
            {
                ClientId = "Client1",
                DeviceId = "11111",
                DeviceDisplayId = "Lukas11111",
                Latitude = "10",
                Longitude = "20",
                AuthKey1 = Guid.NewGuid().ToString(),
                AuthKey2 = Guid.NewGuid().ToString()
            };

            // Somehow I stole Id
            clientDeviceService.CreateClientDevice(userDevice, "Client1");

            var userDeviceCompromised = new ClientDevice
            {
                Id = userDevice.Id,
                ClientId = "Client1",
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
        [Category("Iot")]
        [Category("IntegrationTests.UserDevices")]
        public void Can_delete_client_device_from_database()
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

            clientDeviceService.CreateClientDevice(userDevice, "Lukas");

            // First insert userDevice to db
            userDevice.ShouldNotBeNull();
            userDevice.Id.ShouldNotBeNull();

            // Delete userDevice from db
            var isDeleted = clientDeviceService.DeleteClientDevice(userDevice.Id.ToString(), "Lukas");
            isDeleted.ShouldEqual(true);
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.UserDevices")]
        public void Cannot_delete_other_client_device()
        {
            var userDevice = new ClientDevice
            {
                ClientId = "Client1",
                DeviceId = "11111",
                DeviceDisplayId = "Lukas11111",
                Latitude = "10",
                Longitude = "20",
                AuthKey1 = Guid.NewGuid().ToString(),
                AuthKey2 = Guid.NewGuid().ToString()
            };
            
            // Somehow I stole Id
            var exception = typeof(BusinessException).ShouldBeThrownBy(() => clientDeviceService.DeleteClientDevice(userDevice.Id.ToString(), "Client2"));

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
