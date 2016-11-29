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
        private IUserDeviceService userDeviceService;

        [SetUp]
        public void SetUp()
        {
            userDeviceService = new UserDeviceService();
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.UserDevice")]
        public void Can_insert_userDevice_to_database()
        {
            var userDevice = new UserDevices()
            {
                //UserId, DeviceId, DeviceDisplayId, Latitude, Longitude, AuthKey1, AuthKey2 mandatory fields
                UserId = "Lukas",
                DeviceId = "11111",
                DeviceDisplayId = "Lukas11111",
                Latitude = "10",
                Longitude = "20",
                AuthKey1 = Guid.NewGuid().ToString(),
                AuthKey2 = Guid.NewGuid().ToString()
            };
            userDeviceService.CreateUserDevice(userDevice);

            userDevice.ShouldNotBeNull();
            userDevice.Id.ShouldNotBeNull();
        }


        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.UserDevice")]
        public void Cannot_insert_userDevice_to_database_when_UserId_is_not_provided()
        {
            var userDevice = new UserDevices()
            {
                UserId = "",
                DeviceId = "11111",
                DeviceDisplayId = "Lukas11111",
                Latitude = "10",
                Longitude = "20",
                AuthKey1 = Guid.NewGuid().ToString(),
                AuthKey2 = Guid.NewGuid().ToString()
            };

            typeof(BusinessException).ShouldBeThrownBy(() => userDeviceService.CreateUserDevice(userDevice));
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.UserDevice")]
        public void Cannot_insert_userDevice_to_database_when_DeviceId_is_not_provided()
        {
            var userDevice = new UserDevices()
            {
                UserId = "Lukas",
                DeviceId = "",
                DeviceDisplayId = "Lukas11111", // needs to be unique
                Latitude = "10",
                Longitude = "20",
                AuthKey1 = Guid.NewGuid().ToString(),
                AuthKey2 = Guid.NewGuid().ToString()
            };

            typeof(BusinessException).ShouldBeThrownBy(() => userDeviceService.CreateUserDevice(userDevice));
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.UserDevice")]
        public void Cannot_insert_userDevice_to_database_when_DeviceDisplayId_is_not_provided()
        {
            var userDevice = new UserDevices()
            {
                UserId = "Lukas",
                DeviceId = "11111",
                DeviceDisplayId = "", // needs to be unique
                Latitude = "10",
                Longitude = "20",
                AuthKey1 = Guid.NewGuid().ToString(),
                AuthKey2 = Guid.NewGuid().ToString()
            };

            typeof(BusinessException).ShouldBeThrownBy(() => userDeviceService.CreateUserDevice(userDevice));
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.Device")]
        public void Cannot_insert_device_to_database_when_uniquename_is_not_unique()
        {
            var userDevice = new UserDevices()
            {
                UserId = "Lukas",
                DeviceId = "11111",
                DeviceDisplayId = "Lukas11111", // needs to be unique
                Latitude = "10",
                Longitude = "20",
                AuthKey1 = Guid.NewGuid().ToString(),
                AuthKey2 = Guid.NewGuid().ToString()
            };

            var userDevice2 = new UserDevices()
            {
                UserId = "Tadas",
                DeviceId = "22222",
                DeviceDisplayId = "Lukas11111", // needs to be unique
                Latitude = "5",
                Longitude = "5",
                AuthKey1 = Guid.NewGuid().ToString(),
                AuthKey2 = Guid.NewGuid().ToString()
            };

            userDeviceService.CreateUserDevice(userDevice);

            userDevice.ShouldNotBeNull();
            userDevice.Id.ShouldNotBeNull();

            typeof(BusinessException).ShouldBeThrownBy(() => userDeviceService.CreateUserDevice(userDevice2));
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.UserDevice")]
        public void Cannot_insert_userDevice_to_database_when_Latitude_is_not_provided()
        {
            var userDevice = new UserDevices()
            {
                UserId = "Lukas",
                DeviceId = "11111",
                DeviceDisplayId = "Lukas11111", // needs to be unique
                Latitude = "",
                Longitude = "20",
                AuthKey1 = Guid.NewGuid().ToString(),
                AuthKey2 = Guid.NewGuid().ToString()
            };
            typeof(BusinessException).ShouldBeThrownBy(() => userDeviceService.CreateUserDevice(userDevice));
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.UserDevice")]
        public void Cannot_insert_userDevice_to_database_when_Longtitude_is_not_provided()
        {
            var userDevice = new UserDevices()
            {
                UserId = "Lukas",
                DeviceId = "11111",
                DeviceDisplayId = "Lukas11111", // needs to be unique
                Latitude = "10",
                Longitude = "",
                AuthKey1 = Guid.NewGuid().ToString(),
                AuthKey2 = Guid.NewGuid().ToString()
            };
            typeof(BusinessException).ShouldBeThrownBy(() => userDeviceService.CreateUserDevice(userDevice));
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.UserDevice")]
        public void Cannot_insert_userDevice_to_database_when_AuthKey1_is_not_provided()
        {
            var userDevice = new UserDevices()
            {
                UserId = "Lukas",
                DeviceId = "11111",
                DeviceDisplayId = "Lukas11111", // needs to be unique
                Latitude = "10",
                Longitude = "20",
                AuthKey1 = "",
                AuthKey2 = Guid.NewGuid().ToString()
            };
            typeof(BusinessException).ShouldBeThrownBy(() => userDeviceService.CreateUserDevice(userDevice));
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.UserDevice")]
        public void Cannot_insert_userDevice_to_database_when_AuthKey2_is_not_provided()
        {
            var userDevice = new UserDevices()
            {
                UserId = "Lukas",
                DeviceId = "11111",
                DeviceDisplayId = "Lukas11111", // needs to be unique
                Latitude = "10",
                Longitude = "20",
                AuthKey1 = Guid.NewGuid().ToString(),
                AuthKey2 = ""
            };
            typeof(BusinessException).ShouldBeThrownBy(() => userDeviceService.CreateUserDevice(userDevice));
        }


        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.UserDevice")]
        public void Can_get_userDevice_by_id()
        {
            var userDevice = new UserDevices()
            {
                UserId = "Lukas",
                DeviceId = "11111",
                DeviceDisplayId = "Lukas11111",
                Latitude = "10",
                Longitude = "20",
                AuthKey1 = Guid.NewGuid().ToString(),
                AuthKey2 = Guid.NewGuid().ToString()
            };
            userDeviceService.CreateUserDevice(userDevice);

            userDevice.ShouldNotBeNull();
            userDevice.Id.ShouldNotBeNull();

            var userDeviceFromDb = userDeviceService.GetUserDevice(userDevice.Id.ToString());
            userDeviceFromDb.ShouldNotBeNull();
            userDeviceFromDb.Id.ShouldNotBeNull();
            userDeviceFromDb.UserId.ShouldEqual("Lukas");

        }


        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.UserDevice")]
        public void Can_get_all_userDevice()
        {
            var userDevice1 = new UserDevices()
            {
                UserId = "Lukas",
                DeviceId = "11111",
                DeviceDisplayId = "Lukas11111", // needs to be unique
                Latitude = "10",
                Longitude = "20",
                AuthKey1 = Guid.NewGuid().ToString(),
                AuthKey2 = Guid.NewGuid().ToString()
            };

            var userDevice2 = new UserDevices()
            {
                UserId = "Tomas",
                DeviceId = "22222",
                DeviceDisplayId = "Tomas22222", // needs to be unique
                Latitude = "20",
                Longitude = "30",
                AuthKey1 = Guid.NewGuid().ToString(),
                AuthKey2 = Guid.NewGuid().ToString()
            };

            userDeviceService.CreateUserDevice(userDevice1);
            userDevice1.ShouldNotBeNull();
            userDevice1.Id.ShouldNotBeNull();


            userDeviceService.CreateUserDevice(userDevice2);
            userDevice2.ShouldNotBeNull();
            userDevice2.Id.ShouldNotBeNull();



            var userDevices = userDeviceService.GetAllUserDevices();

            userDevices.ShouldBe<List<UserDevices>>();
            (userDevices.Count > 0).ShouldBeTrue();
        }


        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.UserDevice")]
        public void Can_get_all_userDevice_by_name()
        {
            var userDevice1 = new UserDevices()
            {
                UserId = "Lukas",
                DeviceId = "11111",
                DeviceDisplayId = "Lukas11111",
                Latitude = "10",
                Longitude = "20",
                AuthKey1 = Guid.NewGuid().ToString(),
                AuthKey2 = Guid.NewGuid().ToString()
            };

            var userDevice2 = new UserDevices()
            {
                UserId = "Tomas",
                DeviceId = "22222",
                DeviceDisplayId = "Tomas22222", // needs to be unique
                Latitude = "20",
                Longitude = "30",
                AuthKey1 = Guid.NewGuid().ToString(),
                AuthKey2 = Guid.NewGuid().ToString()
            };

            var userDevice3 = new UserDevices()
            {
                UserId = "Tadas",
                DeviceId = "33333",
                DeviceDisplayId = "Tadas33333", // needs to be unique
                Latitude = "15",
                Longitude = "15",
                AuthKey1 = Guid.NewGuid().ToString(),
                AuthKey2 = Guid.NewGuid().ToString()
            };

            userDeviceService.CreateUserDevice(userDevice1);
            userDevice1.ShouldNotBeNull();
            userDevice1.Id.ShouldNotBeNull();


            userDeviceService.CreateUserDevice(userDevice2);
            userDevice2.ShouldNotBeNull();
            userDevice2.Id.ShouldNotBeNull();

            userDeviceService.CreateUserDevice(userDevice3);
            userDevice3.ShouldNotBeNull();
            userDevice3.Id.ShouldNotBeNull();
            
            var userDevices = userDeviceService.GetAllUserDevices("Tadas");

            userDevices.ShouldBe<List<UserDevices>>();
            userDevices.Count.ShouldEqual(1);
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.UserDevices")]
        public void Can_update_userDevice_to_database()
        {
            var userDevice = new  UserDevices()
            {
                UserId = "Matas",
                DeviceId = "33333",
                DeviceDisplayId = "Matas33333",
                Latitude = "8",
                Longitude = "7",
                AuthKey1 = Guid.NewGuid().ToString(),
                AuthKey2 = Guid.NewGuid().ToString()
            };

            userDeviceService.CreateUserDevice(userDevice);

            // First insert userDevice to db
            userDevice.ShouldNotBeNull();
            userDevice.Id.ShouldNotBeNull();

            // Update name and send update to db
            userDevice.UserId = "Matukas";
            userDeviceService.UpdateUserDevice(userDevice);

            // Get item from db and check if name was updated
            var userDevicesFromDb = userDeviceService.GetUserDevice(userDevice.Id.ToString());
            userDevicesFromDb.ShouldNotBeNull();
            userDevicesFromDb.UserId.ShouldEqual("Matukas");
        }


        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.UserDevices")]
        public void Can_delete_userDevice_from_database()
        {
            var userDevice = new UserDevices()
            {
                UserId = "Lukas",
                DeviceId = "11111",
                DeviceDisplayId = "Lukas11111",
                Latitude = "10",
                Longitude = "20",
                AuthKey1 = Guid.NewGuid().ToString(),
                AuthKey2 = Guid.NewGuid().ToString()
            };

            userDeviceService.CreateUserDevice(userDevice);

            // First insert userDevice to db
            userDevice.ShouldNotBeNull();
            userDevice.Id.ShouldNotBeNull();

            // Delete userDevice from db
            userDeviceService.DeleteUserDevice(userDevice.Id.ToString());

            // Get item from db and check if name was updated
            var userDeviceFromDb = userDeviceService.GetUserDevice(userDevice.Id.ToString());

            // issue with CodeMash library. Should return null when not found - not default object
            userDeviceFromDb.ShouldNotBeNull();
            userDeviceFromDb.Id.ShouldEqual(ObjectId.Empty);
        }

        
        [TearDown]
        public void Dispose()
        {
            var userDevices = userDeviceService.GetAllUserDevices();
            foreach (var userDevice in userDevices)
            {
                userDeviceService.DeleteUserDevice(userDevice.Id.ToString());
            }
        }
    }
}
