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

namespace PresentConnection.Internship7.Iot.Tests
{
    [TestFixture]
    public partial class DevicesTests : TestsBase
    {
        private ServiceStackHost appHost;
        private IDeviceService deviceService;
        private Device goodDevice;
        private IDeviceService deviceServiceMock;

        private void SetupMocks()
        {
            deviceServiceMock = Substitute.For<IDeviceService>();

            // when pass any parameter (Id) to deviceService.GetDevice, return goodDevice with passed id
            deviceServiceMock.GetDevice(Arg.Any<string>()).Returns(info =>
            {
                goodDevice.Id = ObjectId.Parse(info.ArgAt<string>(0));
                return goodDevice;
            });


            // when we are trying get devices, just return generated list
            deviceServiceMock.GetAllDevices(string.Empty).Returns(info =>
            {
                var list = new List<Device>();

                goodDevice.Id = ObjectId.GenerateNewId();
                list.Add(goodDevice);

                goodDevice.Id = ObjectId.GenerateNewId();
                goodDevice.ModelName = "Object1";
                list.Add(goodDevice);

                goodDevice.Id = ObjectId.GenerateNewId();
                goodDevice.ModelName = "Object2";
                list.Add(goodDevice);

                return list;
            });

            // when we are trying get devices by name, just generate list and filter by name
            deviceServiceMock.GetAllDevices(Arg.Any<string>()).Returns(info =>
            {
                var list = new List<Device>();

                goodDevice.Id = ObjectId.GenerateNewId();
                list.Add(goodDevice);

                goodDevice.Id = ObjectId.GenerateNewId();
                goodDevice.ModelName = "Object1";
                list.Add(goodDevice);

                goodDevice.Id = ObjectId.GenerateNewId();
                goodDevice.ModelName = "Object2";
                list.Add(goodDevice);

                return list.Where(x => x.ModelName.Contains(info.ArgAt<string>(0))).ToList();
            });


        }

        [SetUp]
        public void SetUp()
        {
            //Start your AppHost on TestFixtureSetUp
            appHost = new AppHost()
                .Init()
                .Start(BaseUri);

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

            SetupMocks();
        }


        [Test]
        [Category("Device")]
        public void Can_do_CRUD_device_to_database()
        {
            var client = new JsonServiceClient(BaseUri);

            // Create

            var createRequest = new CreateDevice
            {
                ModelName = goodDevice.ModelName,
                UniqueName = goodDevice.UniqueName,
                Images = goodDevice.Images
            };

            var createDeviceResponse = client.Post(createRequest);
            createDeviceResponse.ShouldNotBeNull();
            createDeviceResponse.Result.ShouldNotBeNull();
            createDeviceResponse.Result.Id.ShouldNotBeNull();
            createDeviceResponse.Result.Id.ShouldBeNotBeTheSameAs(ObjectId.Empty);

            var createRequest2 = new CreateDevice
            {
                ModelName = goodDevice.ModelName + "2",
                UniqueName = goodDevice.UniqueName + "-2",
                Images = goodDevice.Images
            };

            var createDeviceResponse2 = client.Post(createRequest2);
            createDeviceResponse2.ShouldNotBeNull();
            createDeviceResponse2.Result.ShouldNotBeNull();
            createDeviceResponse2.Result.Id.ShouldNotBeNull();
            createDeviceResponse2.Result.Id.ShouldBeNotBeTheSameAs(ObjectId.Empty);

            // Get all

            var getDevicesRequest = new GetDevices
            {
                Name = string.Empty
            };

            var getDevicesResponse = client.Get(getDevicesRequest);
            getDevicesResponse.ShouldNotBeNull();
            getDevicesResponse.Result.ShouldNotBeNull();
            getDevicesResponse.Result.Count.ShouldEqual(2);

            // Get by name
            // Get find by name works in "LIKE '%Name%'" manner, so I provide name which is one in db e.g. Arduino2
            var getDevicesByNameRequest = new GetDevices
            {
                Name = goodDevice.ModelName + "2"
            };

            var getDevicesByNameResponse = client.Get(getDevicesByNameRequest);
            getDevicesByNameResponse.ShouldNotBeNull();
            getDevicesByNameResponse.Result.ShouldNotBeNull();
            getDevicesByNameResponse.Result.Count.ShouldEqual(1);


            // Get by name
            // I will update first inserted item 
            var updateDeviceRequest = new UpdateDevice
            {
                Id = createDeviceResponse.Result.Id.ToString(),
                ModelName = createDeviceResponse.Result.ModelName + "Updated",
                UniqueName = createDeviceResponse.Result.UniqueName + "-updated"
            };

            var updateDeviceResponse = client.Put(updateDeviceRequest);
            updateDeviceResponse.ShouldNotBeNull();
            updateDeviceResponse.Result.ShouldNotBeNull();


            // Get by id
            var getDeviceById = new GetDevice
            {
                Id = updateDeviceResponse.Result.Id.ToString()

            };

            var getDeviceByIdResponse = client.Get(getDeviceById);
            getDeviceByIdResponse.ShouldNotBeNull();
            getDeviceByIdResponse.Result.ShouldNotBeNull();
            getDeviceByIdResponse.Result.UniqueName.ShouldEqual(createDeviceResponse.Result.UniqueName + "-updated");
            getDeviceByIdResponse.Result.ModelName.ShouldEqual(createDeviceResponse.Result.ModelName + "Updated");


            // Delete 
            var deleteRequest = new DeleteDevice
            {
                Id = getDeviceByIdResponse.Result.Id.ToString()
            };

            var deleteRequestResponse = client.Delete(deleteRequest);

            deleteRequestResponse.ShouldNotBeNull();
            deleteRequestResponse.Result.ShouldNotBeNull();
            deleteRequestResponse.Result.ShouldEqual(true);
        }

        [TearDown]
        public void Dispose()
        {
            appHost.Dispose();

            var devices = deviceService.GetAllDevices();

            foreach (var device in devices)
            {
                deviceService.DeleteDevice(device.Id.ToString());
            }

        }
    }
}
