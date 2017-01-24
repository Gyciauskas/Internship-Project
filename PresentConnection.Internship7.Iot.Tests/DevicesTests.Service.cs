using System.Collections.Generic;
using System.Configuration;
using System.IO;
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
using System;

namespace PresentConnection.Internship7.Iot.Tests
{
    [TestFixture]
    public partial class DevicesTests : TestsBase
    {
        private ServiceStackHost appHost;
        private IDeviceService deviceService;
        private IDeviceService deviceServiceMock;

        private Device goodDevice;
        private string testImagePath;
        private byte[] imageBytes;
        private string imageDir;

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
                Images =
                {
                    "5821dcc11e9f341d4c6d0994"
                },
                UniqueName = "raspberry-pi-3"
            };

            // This maked because of uploaded picture to tests project, Images folder. Otherwise it would throw exception
            string projectPath = Environment.CurrentDirectory;
            testImagePath = Directory.EnumerateFiles(projectPath + "\\PresentConnection.Internship7.Iot.Tests\\Images".MapHostAbsolutePath()).Last();
            imageDir = ConfigurationManager.AppSettings["ImagesPath"].MapHostAbsolutePath();
            imageBytes = File.ReadAllBytes(testImagePath);

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
                FileName = "test.jpg",
                Image = imageBytes
            };

            var createDeviceResponse = client.Post(createRequest);
            createDeviceResponse.ShouldNotBeNull();
            createDeviceResponse.Result.ShouldNotBeNull();
            createDeviceResponse.Result.ShouldBeNotBeTheSameAs(ObjectId.Empty);

            var createRequest2 = new CreateDevice
            {
                ModelName = goodDevice.ModelName + "2",
                FileName = "test.jpg",
                Image = imageBytes
            };

            var createDeviceResponse2 = client.Post(createRequest2);
            createDeviceResponse2.ShouldNotBeNull();
            createDeviceResponse2.Result.ShouldNotBeNull();
            createDeviceResponse2.Result.ShouldBeNotBeTheSameAs(ObjectId.Empty);

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
                Id = createDeviceResponse.Result,
                ModelName = createRequest.ModelName + "Updated",
                UniqueName = SeoService.GetSeName(createRequest.ModelName) + "-updated"
            };

            var updateDeviceResponse = client.Put(updateDeviceRequest);
            updateDeviceResponse.ShouldNotBeNull();
            updateDeviceResponse.Result.ShouldNotBeNull();


            // Get by id
            var getDeviceById = new GetDevice
            {
                Id = createDeviceResponse.Result

            };

            var getDeviceByIdResponse = client.Get(getDeviceById);
            getDeviceByIdResponse.ShouldNotBeNull();
            getDeviceByIdResponse.Result.ShouldNotBeNull();
            getDeviceByIdResponse.Result.UniqueName.ShouldEqual(SeoService.GetSeName(createRequest.ModelName) + "-updated");
            getDeviceByIdResponse.Result.ModelName.ShouldEqual(createRequest.ModelName + "Updated");


            // Delete 
            var deleteRequest = new DeleteDevice
            {
                Id = getDeviceByIdResponse.Result.Id
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

            var files = Directory.GetFiles(imageDir);
            foreach (var file in files)
            {
                File.Delete(file);
            }

        }
    }
}
