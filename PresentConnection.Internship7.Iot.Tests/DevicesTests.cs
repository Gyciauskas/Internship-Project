using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using NUnit.Framework;
using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.BusinessImplementation;
using PresentConnection.Internship7.Iot.Domain;
using PresentConnection.Internship7.Iot.Utils;

namespace PresentConnection.Internship7.Iot.Tests
{
    [TestFixture]
    public class DevicesTests
    {
        private IDeviceService deviceService;
        private Device goodDevice;

        [SetUp]
        public void SetUp()
        {
            deviceService = new DeviceService();
            goodDevice = new Device
            {
                ModelName = "Raspberry PI 3",
                UniqueName = "raspberry-pi-3",
                Images =
                {
                    "5821dcc11e9f341d4c6d0994"
                }
            };

        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.Device")]
        public void Can_insert_device_to_database()
        {
            deviceService.CreateDevice(goodDevice);
            goodDevice.ShouldNotBeNull();
            goodDevice.Id.ShouldNotBeNull();
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.Device")]
        public void Cannot_insert_device_to_database_when_modelname_is_not_provided()
        {
            goodDevice.ModelName = string.Empty;
            var exception = typeof(BusinessException).ShouldBeThrownBy(() => deviceService.CreateDevice(goodDevice));
            exception.Message.ShouldEqual("Cannot create device");
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.Device")]
        public void Cannot_insert_device_to_database_when_uniquename_is_not_provided()
        {
            goodDevice.UniqueName = string.Empty;
            var exception = typeof(BusinessException).ShouldBeThrownBy(() => deviceService.CreateDevice(goodDevice));
            exception.Message.ShouldEqual("Cannot create device");
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.Device")]
        public void Cannot_insert_device_to_database_when_image_is_not_provided()
        {
            goodDevice.Images = null;
            var exception = typeof(BusinessException).ShouldBeThrownBy(() => deviceService.CreateDevice(goodDevice));

            var businessException = exception as BusinessException;
            businessException.ShouldNotBeNull();
            businessException?.Errors.SingleOrDefault(error => error.ErrorMessage.Equals("Property Images should contain at least one item!"))
                .ShouldNotBeNull("Received different error message");

            exception.Message.ShouldEqual("Cannot create device");
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.Device")]
        public void Cannot_insert_device_to_database_when_such_unique_name_exist()
        {

            deviceService.CreateDevice(goodDevice);

            var device2 = new Device
            {
                ModelName = "Device 2",
                UniqueName = "raspberry-pi-3",
                Images =
                {
                    "5821dcc11e9f341d4c6d0994"
                }
            };

            var exception = typeof(BusinessException).ShouldBeThrownBy(() => deviceService.CreateDevice(device2));

            var businessException = exception as BusinessException;
            businessException.ShouldNotBeNull();

            businessException?.Errors.SingleOrDefault(error => error.ErrorMessage.Equals("Property  should be unique in database and in correct format !"))
                .ShouldNotBeNull("Received different error message");

            exception.Message.ShouldEqual("Cannot create device");
        }


        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.Device")]
        public void Cannot_insert_device_to_database_when_uniquename_is_not_in_correct_format()
        {
            goodDevice.UniqueName = "Raspberry PI 3";
            
            var exception = typeof(BusinessException).ShouldBeThrownBy(() => deviceService.CreateDevice(goodDevice));

            var businessException = exception as BusinessException;
            businessException.ShouldNotBeNull();

            businessException?.Errors.SingleOrDefault(error => error.ErrorMessage.Equals("Property  should be unique in database and in correct format !"))
                .ShouldNotBeNull("Received different error message");

            exception.Message.ShouldEqual("Cannot create device");
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.Device")]
        public void Cannot_insert_device_to_database_when_uniquename_is_not_in_correct_format_unique_name_with_upercases()
        {
            goodDevice.UniqueName = "raspberry-PI-3";
            
            var exception = typeof(BusinessException).ShouldBeThrownBy(() => deviceService.CreateDevice(goodDevice));

            var businessException = exception as BusinessException;
            businessException.ShouldNotBeNull();

            businessException?.Errors.SingleOrDefault(error => error.ErrorMessage.Equals("Property  should be unique in database and in correct format !"))
                .ShouldNotBeNull("Received different error message");

            exception.Message.ShouldEqual("Cannot create device");
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.Device")]
        public void Can_get_device_by_id()
        {
            deviceService.CreateDevice(goodDevice);

            goodDevice.ShouldNotBeNull();
            goodDevice.Id.ShouldNotBeNull();

            var deviceFromDb = deviceService.GetDevice(goodDevice.Id.ToString());
            deviceFromDb.ShouldNotBeNull();
            deviceFromDb.Id.ShouldNotBeNull();
            deviceFromDb.ModelName.ShouldEqual("Raspberry PI 3");
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.Device")]
        public void Can_get_all_devices()
        {
            deviceService.CreateDevice(goodDevice);

            var device2 = new Device
            {
                ModelName = "Device 2",
                UniqueName = "device-2",
                Images =
                {
                    "5821dcc11e9f341d4c6d0994"
                }
            };

            deviceService.CreateDevice(device2);
            device2.ShouldNotBeNull();
            device2.Id.ShouldNotBeNull();
            
            var devices = deviceService.GetAllDevices();

            devices.ShouldBe<List<Device>>();
            (devices.Count > 0).ShouldBeTrue();
        }



        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.Device")]
        public void Can_get_all_devices_by_name()
        {
            var device1 = new Device
            {
                ModelName = "Raspberry PI 3",
                UniqueName = "raspberry-pi-3",
                Images =
                {
                    "5821dcc11e9f341d4c6d0994"
                }
            };

            var device2 = new Device
            {
                ModelName = "Device 2",
                UniqueName = "device-2",
                Images =
                {
                    "5821dcc11e9f341d4c6d0994"
                }
            };

            var device3 = new Device
            {
                ModelName = "Device 3",
                UniqueName = "device-3",
                Images =
                {
                    "5821dcc11e9f341d4c6d0994"
                }
            };

            deviceService.CreateDevice(device1);
            device1.ShouldNotBeNull();
            device1.Id.ShouldNotBeNull();

            deviceService.CreateDevice(device2);
            device2.ShouldNotBeNull();
            device2.Id.ShouldNotBeNull();

            deviceService.CreateDevice(device3);
            device3.ShouldNotBeNull();
            device3.Id.ShouldNotBeNull();

            var devices = deviceService.GetAllDevices("Device 3");

            devices.ShouldBe<List<Device>>();
            devices.Count.ShouldEqual(1);
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.Device")]
        public void Can_get_all_devices_by_case_insensetive_name()
        {
            var device1 = new Device
            {
                ModelName = "Raspberry PI 3",
                UniqueName = "raspberry-pi-3",
                Images =
                {
                    "5821dcc11e9f341d4c6d0994"
                }
            };

            var device2 = new Device
            {
                ModelName = "Device 2",
                UniqueName = "device-2",
                Images =
                {
                    "5821dcc11e9f341d4c6d0994"
                }
            };

            var device3 = new Device
            {
                ModelName = "Device 3",
                UniqueName = "device-3",
                Images =
                {
                    "5821dcc11e9f341d4c6d0994"
                }
            };

            deviceService.CreateDevice(device1);
            device1.ShouldNotBeNull();
            device1.Id.ShouldNotBeNull();

            deviceService.CreateDevice(device2);
            device2.ShouldNotBeNull();
            device2.Id.ShouldNotBeNull();

            deviceService.CreateDevice(device3);
            device3.ShouldNotBeNull();
            device3.Id.ShouldNotBeNull();

            var devices = deviceService.GetAllDevices("device 3");

            devices.ShouldBe<List<Device>>();
            devices.Count.ShouldEqual(1);
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.Device")]
        public void Can_get_all_devices_by_incomplete_name()
        {
            var device1 = new Device
            {
                ModelName = "Raspberry PI 3",
                UniqueName = "raspberry-pi-3",
                Images =
                {
                    "5821dcc11e9f341d4c6d0994"
                }
            };

            var device2 = new Device
            {
                ModelName = "Device 2",
                UniqueName = "device-2",
                Images =
                {
                    "5821dcc11e9f341d4c6d0994"
                }
            };

            var device3 = new Device
            {
                ModelName = "Device 3",
                UniqueName = "device-3",
                Images =
                {
                    "5821dcc11e9f341d4c6d0994"
                }
            };

            deviceService.CreateDevice(device1);
            device1.ShouldNotBeNull();
            device1.Id.ShouldNotBeNull();

            deviceService.CreateDevice(device2);
            device2.ShouldNotBeNull();
            device2.Id.ShouldNotBeNull();

            deviceService.CreateDevice(device3);
            device3.ShouldNotBeNull();
            device3.Id.ShouldNotBeNull();

            var devices = deviceService.GetAllDevices("device");

            devices.ShouldBe<List<Device>>();
            devices.Count.ShouldEqual(2);
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.Device")]
        public void Can_update_device_to_database()
        {
            var device = new Device
            {
                ModelName = "Raspberry PI 3",
                UniqueName = "raspberry-pi-3",
                Images =
                {
                    "5821dcc11e9f341d4c6d0994"
                }
            };
            deviceService.CreateDevice(device);

            device.ShouldNotBeNull();
            device.Id.ShouldNotBeNull();

            device.ModelName = "Edited";
            deviceService.UpdateDevice(device);

            var deviceFromDb = deviceService.GetDevice(device.Id.ToString());
            deviceFromDb.ShouldNotBeNull();
            deviceFromDb.ModelName.ShouldEqual("Edited");
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.Device")]
        public void Cannot_update_device_to_database_when_such_unique_name_already_exist()
        {
            deviceService.CreateDevice(goodDevice);

            var device = new Device
            {
                ModelName = "Raspberry PI 3",
                UniqueName = "raspberry-pi-2",
                Images =
                {
                    "5821dcc11e9f341d4c6d0994"
                }
            };
            deviceService.CreateDevice(device);

            device.ShouldNotBeNull();
            device.Id.ShouldNotBeNull();

            device.UniqueName = "raspberry-pi-3";
            
            var exception = typeof(BusinessException).ShouldBeThrownBy(() => deviceService.UpdateDevice(device));

            var businessException = exception as BusinessException;
            businessException.ShouldNotBeNull();

            businessException?.Errors.SingleOrDefault(error => error.ErrorMessage.Equals("Property  should be unique in database and in correct format !"))
                .ShouldNotBeNull("Received different error message");

            exception.Message.ShouldEqual("Cannot update device");
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.Device")]
        public void Can_delete_device_from_database()
        {
            deviceService.CreateDevice(goodDevice);

            goodDevice.ShouldNotBeNull();
            goodDevice.Id.ShouldNotBeNull();

            deviceService.DeleteDevice(goodDevice.Id.ToString());
            var deviceFromDb = deviceService.GetDevice(goodDevice.Id.ToString());

            deviceFromDb.ShouldNotBeNull();
            deviceFromDb.Id.ShouldEqual(ObjectId.Empty);
        }

        [TearDown]
        public void Dispose()
        {
            var devices = deviceService.GetAllDevices();
            foreach (var device in devices)
            {
                deviceService.DeleteDevice(device.Id.ToString());
            }
        }
    }
}
