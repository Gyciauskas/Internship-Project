using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using MongoDB.Bson;
using NSubstitute;
using NUnit.Framework;
using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.BusinessImplementation;
using PresentConnection.Internship7.Iot.Domain;
using PresentConnection.Internship7.Iot.ServiceModels;
using ServiceStack;
using System;

namespace PresentConnection.Internship7.Iot.Tests
{
    [TestFixture]
    public partial class ManufacturersTests : TestsBase
    {
        private ServiceStackHost appHost;
        private IManufacturerService manufacturerService;
        private IManufacturerService manufacturerServiceMock;

        private Manufacturer goodManufacturer;
        private string testImagePath;
        private byte[] imageBytes;
        private string imageDir;

        private void SetupMocks()
        {
            manufacturerServiceMock = Substitute.For<IManufacturerService>();

            manufacturerServiceMock.GetManufacturer(Arg.Any<string>()).Returns(info => 
            {
                goodManufacturer.Id = ObjectId.Parse(info.ArgAt<string>(0));
                return goodManufacturer;
            });

            manufacturerServiceMock.DeleteManufacturer(Arg.Any<string>()).Returns(info => true);
            
            // when we are trying get manufacturers, just return generated list
            manufacturerServiceMock.GetAllManufacturers(Arg.Is<string>(x => x.IsNullOrEmpty())).Returns(info =>
            {
                var list = new List<Manufacturer>();

                goodManufacturer.Id = ObjectId.GenerateNewId();
                list.Add(goodManufacturer);

                var manufacturer2 = new Manufacturer
                {
                    Name = "Arduino 2",
                    Description = "description",
                    UniqueName = "raspberry-pi-2",
                    Images =
                {
                    "5821dcc11e9f341d4c6d0994"
                },
                    Url = "url",
                    IsVisible = true
                };
                manufacturer2.Id = ObjectId.GenerateNewId();
                manufacturer2.Name = "Object1";
                list.Add(manufacturer2);

                var manufacturer3 = new Manufacturer
                {
                    Name = "Arduino 3",
                    Description = "description",
                    UniqueName = "raspberry-pi-1",
                    Images =
                {
                    "5821dcc11e9f341d4c6d0994"
                },
                    Url = "url",
                    IsVisible = true
                };
                manufacturer3.Id = ObjectId.GenerateNewId();
                manufacturer3.Name = "Object2";
                list.Add(manufacturer2);

                return list;
            });

            // when we are trying get manufacturers by name, just generate list and filter by name
            manufacturerServiceMock.GetAllManufacturers(Arg.Is<string>(x => x.Length >= 1)).Returns(info =>
            {
                var list = new List<Manufacturer>();

                goodManufacturer.Id = ObjectId.GenerateNewId();
                list.Add(goodManufacturer);

                var manufacturer2 = new Manufacturer
                {
                    Name = "Arduino 2",
                    Description = "description",
                    UniqueName = "raspberry-pi-2",
                    Images =
                {
                    "5821dcc11e9f341d4c6d0994"
                },
                    Url = "url",
                    IsVisible = true
                };
                manufacturer2.Id = ObjectId.GenerateNewId();
                manufacturer2.Name = "Object1";
                list.Add(manufacturer2);

                var manufacturer3 = new Manufacturer
                {
                    Name = "Arduino 3",
                    Description = "description",
                    UniqueName = "raspberry-pi-1",
                    Images =
                {
                    "5821dcc11e9f341d4c6d0994"
                },
                    Url = "url",
                    IsVisible = true
                };
                manufacturer3.Id = ObjectId.GenerateNewId();
                manufacturer3.Name = "Object2";
                list.Add(manufacturer2);

                return list.Where(x => x.Name.Contains(info.ArgAt<string>(0))).ToList();
            });
            
        }

        [SetUp]
        public void SetUp()
        {
            //Start your AppHost on TestFixtureSetUp
            appHost = new AppHost()
                .Init()
                .Start(BaseUri);

            var container = appHost.Container;

            manufacturerService = new ManufacturerService();

            goodManufacturer = new Manufacturer
            {
                Name = "Arduino",
                Description = "description",
                UniqueName = "raspberry-pi-3",
                Images =
                {
                    "5821dcc11e9f341d4c6d0994"
                },
                Url = "url",
                IsVisible = true
            };

            string projectPath = Environment.CurrentDirectory;
            testImagePath = Directory.EnumerateFiles(projectPath + "\\PresentConnection.Internship7.Iot.Tests\\Images".MapHostAbsolutePath()).Last();
            //testImagePath = Directory.EnumerateFiles("~/testImages".MapHostAbsolutePath()).Last();
            imageDir = ConfigurationManager.AppSettings["ImagesPath"].MapHostAbsolutePath();
            imageBytes = File.ReadAllBytes(testImagePath);

            SetupMocks();

            container.Register(manufacturerServiceMock);
        }


        [Test]
        [Category("Manufacturer")]
        public void Can_do_CRUD_manufacturer_to_database()
        {
            var client = new JsonServiceClient(BaseUri);

            // Create

            var createRequest = new CreateManufacturer
            {
                Name = goodManufacturer.Name,
                FileName = "computer-case-fan.png",
                Image = imageBytes
            };

            var createManufacturerResponse = client.Post(createRequest);
            createManufacturerResponse.ShouldNotBeNull();
            createManufacturerResponse.Result.ShouldNotBeNull();
            createManufacturerResponse.Result.ShouldNotBeNull();
            createManufacturerResponse.Result.ShouldBeNotBeTheSameAs(ObjectId.Empty);

            var createRequest2 = new CreateManufacturer
            {
                Name = goodManufacturer.Name + "2",
                FileName = "computer-case-fan.png",
                Image = imageBytes
            };

            var createManufacturerResponse2 = client.Post(createRequest2);
            createManufacturerResponse2.ShouldNotBeNull();
            createManufacturerResponse2.Result.ShouldNotBeNull();
            createManufacturerResponse2.Result.ShouldNotBeNull();
            createManufacturerResponse2.Result.ShouldBeNotBeTheSameAs(ObjectId.Empty);

            // Get all
            var getManufacturersRequest = new GetManufacturers
            {
                Name = string.Empty
            };

            var getManufacturersResponse = client.Get(getManufacturersRequest);
            getManufacturersResponse.ShouldNotBeNull();
            getManufacturersResponse.Result.ShouldNotBeNull();
            getManufacturersResponse.Result.Count.ShouldEqual(3);
            
            // Get by name
            // Get find by name works in "LIKE '%Name%'" manner, so I provide name which is one in db e.g. Arduino2
            var getManufacturersByNameRequest = new GetManufacturers
            {
                Name = goodManufacturer.Name
            };

            var getManufacturersByNameResponse = client.Get(getManufacturersByNameRequest);
            getManufacturersByNameResponse.ShouldNotBeNull();
            getManufacturersByNameResponse.Result.ShouldNotBeNull();
            getManufacturersByNameResponse.Result.Count.ShouldEqual(1);
            

            // I will update first inserted item 
            var updateManufacturerRequest = new UpdateManufacturer
            {
                Id = createManufacturerResponse.Result,
                Name = createRequest.Name + "Updated",
                UniqueName = /* TODO : call sluggify service*/ createRequest.Name.Trim().ToLower() + "-updated"
            };

            var updateManufacturerResponse = client.Put(updateManufacturerRequest);
            updateManufacturerResponse.ShouldNotBeNull();
            updateManufacturerResponse.Result.ShouldNotBeNull();
            
            // Get by id
            var getManufacturerById = new GetManufacturer { Id = createManufacturerResponse.Result };

            var getManufacturerByIdResponse = client.Get(getManufacturerById);
            getManufacturerByIdResponse.ShouldNotBeNull();
            getManufacturerByIdResponse.Result.ShouldNotBeNull();
            getManufacturerByIdResponse.Result.UniqueName.ShouldEqual(/* TODO : call sluggify service*/ createRequest.Name.Trim().ToLower() + "-updated");
            getManufacturerByIdResponse.Result.Name.ShouldEqual(createRequest.Name + "Updated");


            // Delete 
            var deleteRequest = new DeleteManufacturer { Id = getManufacturerByIdResponse.Result.Id };

            var deleteRequestResponse = client.Delete(deleteRequest);

            deleteRequestResponse.ShouldNotBeNull();
            deleteRequestResponse.Result.ShouldNotBeNull();
            deleteRequestResponse.Result.ShouldEqual(true);
        }

        //[Test]
        //[Category("Manufacturer")]
        //public void When_i_call_create_manufacturer_service_i_call_manufacturer_service()
        //{
        //    var service = new CreateManufacturerService();
        //    var mockService = Substitute.For<IManufacturerService>();

        //    mockService.Received(1).CreateManufacturer(Arg.Any<Manufacturer>());
        //    service.ManufacturerService = mockService;

        //    // Do insert
        //    service.Any(new CreateManufacturer());

        //}

        

        [TearDown]
        public void Dispose()
        {
            appHost.Dispose();
            
            var manufacturers = manufacturerService.GetAllManufacturers();

            foreach (var manufacturer in manufacturers)
            {
                manufacturerService.DeleteManufacturer(manufacturer.Id.ToString());
            }

            var files = Directory.GetFiles(imageDir);
            foreach (var file in files)
            {
                File.Delete(file);
            }
        }
    }
}
