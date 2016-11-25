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
    public class DevicesTests
    {
        private IDeviceService deviceService;

        [SetUp]
        public void SetUp()
        {
            deviceService = new DeviceService();
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.Device")]
        public void Can_insert_device_to_database()
        {
            var device = new Device()
            {
                ModelName = "Raspberry PI 3",
                UniqueName = "raspberry-pi-3",
                Images =
                {
                    new DisplayImage()
                    {
                       ImageName = "RaspberryLogo",
                       Widht = "600px",
                       Height = "250px"
                    }
                }
            };
            deviceService.CreateDevice(device);

            device.ShouldNotBeNull();
            device.Id.ShouldNotBeNull();
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.Device")]
        public void Cannot_insert_device_to_database_when_modelname_is_not_provided()
        {
            var device = new Device()
            {
                ModelName = "",
                UniqueName = "raspberry-pi-3",
                Images =
                {
                    new DisplayImage()
                    {
                       ImageName = "RaspberryLogo",
                       Widht = "600px",
                       Height = "250px"
                    }
                }
            };

            typeof(BusinessException).ShouldBeThrownBy(() => deviceService.CreateDevice(device));
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.Device")]
        public void Cannot_insert_device_to_database_when_uniquename_is_not_provided()
        {
            var device = new Device()
            {
                ModelName = "Raspberry PI 3",
                UniqueName = "",
                Images =
                {
                    new DisplayImage()
                    {
                       ImageName = "RaspberryLogo",
                       Widht = "600px",
                       Height = "250px"
                    }
                }
            };

            typeof(BusinessException).ShouldBeThrownBy(() => deviceService.CreateDevice(device));
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.Device")]
        public void Cannot_insert_device_to_database_when_image_is_not_provided()
        {
            var device = new Device()
            {
                ModelName = "Raspberry PI 3",
                UniqueName = "raspberry-pi-3",
                Images = { }
            };

            typeof(BusinessException).ShouldBeThrownBy(() => deviceService.CreateDevice(device));
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.Device")]
        public void Cannot_insert_device_to_database_when_uniquename_is_not_unique()
        {
            var device = new Device()
            {
                ModelName = "Raspberry PI 3",
                UniqueName = "raspberry-pi-3",
                Images =
                {
                    new DisplayImage()
                    {
                       ImageName = "RaspberryLogo",
                       Widht = "600px",
                       Height = "250px"
                    }
                }
            };

            var device2 = new Device()
            {
                ModelName = "Device 2",
                UniqueName = "raspberry-pi-3",
                Images =
                {
                    new DisplayImage()
                    {
                       ImageName = "DeviceLogo",
                       Widht = "600px",
                       Height = "250px"
                    }
                }
            };

            deviceService.CreateDevice(device);

            device.ShouldNotBeNull();
            device.Id.ShouldNotBeNull();

            typeof(BusinessException).ShouldBeThrownBy(() => deviceService.CreateDevice(device2));
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.Device")]
        public void Can_get_device_by_id()
        {
            var device = new Device()
            {
                ModelName = "Raspberry PI 3",
                UniqueName = "raspberry-pi-3",
                Images =
                {
                    new DisplayImage()
                    {
                       ImageName = "RaspberryLogo",
                       Widht = "600px",
                       Height = "250px"
                    }
                }
            };
            deviceService.CreateDevice(device);

            device.ShouldNotBeNull();
            device.Id.ShouldNotBeNull();

            var deviceFromDb = deviceService.GetDevice(device.Id.ToString());
            deviceFromDb.ShouldNotBeNull();
            deviceFromDb.Id.ShouldNotBeNull();
            deviceFromDb.ModelName.ShouldEqual("Raspberry PI 3");
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.Device")]
        public void Can_get_all_devices()
        {
            var device1 = new Device()
            {
                ModelName = "Raspberry PI 3",
                UniqueName = "raspberry-pi-3",
                Images =
                {
                    new DisplayImage()
                    {
                       ImageName = "RaspberryLogo",
                       Widht = "600px",
                       Height = "250px"
                    }
                }
            };

            var device2 = new Device()
            {
                ModelName = "Device 2",
                UniqueName = "device-2",
                Images =
                {
                    new DisplayImage()
                    {
                       ImageName = "DeviceLogo",
                       Widht = "600px",
                       Height = "250px"
                    }
                }
            };

            deviceService.CreateDevice(device1);
            device1.ShouldNotBeNull();
            device1.Id.ShouldNotBeNull();

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
            var device1 = new Device()
            {
                ModelName = "Raspberry PI 3",
                UniqueName = "raspberry-pi-3",
                Images =
                {
                    new DisplayImage()
                    {
                       ImageName = "RaspberryLogo",
                       Widht = "600px",
                       Height = "250px"
                    }
                }
            };

            var device2 = new Device()
            {
                ModelName = "Device 2",
                UniqueName = "device-2",
                Images =
                {
                    new DisplayImage()
                    {
                       ImageName = "Device2Logo",
                       Widht = "600px",
                       Height = "250px"
                    }
                }
            };

            var device3 = new Device()
            {
                ModelName = "Device 3",
                UniqueName = "device-3",
                Images =
                {
                    new DisplayImage()
                    {
                       ImageName = "Device3Logo",
                       Widht = "600px",
                       Height = "250px"
                    }
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
        public void Can_update_device_to_database()
        {
            var device = new Device()
            {
                ModelName = "Raspberry PI 3",
                UniqueName = "raspberry-pi-3",
                Images =
                {
                    new DisplayImage()
                    {
                       ImageName = "RaspberryLogo",
                       Widht = "600px",
                       Height = "250px"
                    }
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
        public void Can_delete_device_from_database()
        {
            var device = new Device()
            {
                ModelName = "Raspberry PI 3",
                UniqueName = "raspberry-pi-3",
                Images =
                {
                    new DisplayImage()
                    {
                       ImageName = "RaspberryLogo",
                       Widht = "600px",
                       Height = "250px"
                    }
                }
            };
            deviceService.CreateDevice(device);

            device.ShouldNotBeNull();
            device.Id.ShouldNotBeNull();

            deviceService.DeleteDevice(device.Id.ToString());
            var deviceFromDb = deviceService.GetDevice(device.Id.ToString());

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
