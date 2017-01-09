using System.Collections.Generic;
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

namespace PresentConnection.Internship7.Iot.Tests
{
    [TestFixture]
    public partial class ManufacturersTests : TestsBase
    {
        private ServiceStackHost appHost;
        private IManufacturerService manufacturerService;
        private Manufacturer goodManufacturer;
        private IManufacturerService manufacturerServiceMock;
        private string testImagePath;
        private byte[] imageBytes;
        private DisplayImage goodDisplayImage;
        private string imagesDir;
        private IImageService imageService;

        private void SetupMocks()
        {
            manufacturerServiceMock = Substitute.For<IManufacturerService>();

            // when pass any parameter (Id) to manufacturerService.GetManufacturer, return goodManufacturer with passed id
            manufacturerServiceMock.GetManufacturer(Arg.Any<string>()).Returns(info => 
            {
                goodManufacturer.Id = ObjectId.Parse(info.ArgAt<string>(0));
                return goodManufacturer;
            });
            
            
            // when we are trying get manufacturers, just return generated list
            manufacturerServiceMock.GetAllManufacturers(string.Empty).Returns(info =>
            {
                var list = new List<Manufacturer>();

                goodManufacturer.Id = ObjectId.GenerateNewId();
                list.Add(goodManufacturer);

                goodManufacturer.Id = ObjectId.GenerateNewId();
                goodManufacturer.Name = "Object1";
                list.Add(goodManufacturer);

                goodManufacturer.Id = ObjectId.GenerateNewId();
                goodManufacturer.Name = "Object2";
                list.Add(goodManufacturer);

                return list;
            });

            // when we are trying get manufacturers by name, just generate list and filter by name
            manufacturerServiceMock.GetAllManufacturers(Arg.Any<string>()).Returns(info =>
            {
                var list = new List<Manufacturer>();

                goodManufacturer.Id = ObjectId.GenerateNewId();
                list.Add(goodManufacturer);

                goodManufacturer.Id = ObjectId.GenerateNewId();
                goodManufacturer.Name = "Object1";
                list.Add(goodManufacturer);

                goodManufacturer.Id = ObjectId.GenerateNewId();
                goodManufacturer.Name = "Object2";
                list.Add(goodManufacturer);

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
            
            manufacturerService = new ManufacturerService();
            imageService = new ImageService();

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
            
            goodDisplayImage = new DisplayImage()
            {
                SeoFileName = Path.GetFileNameWithoutExtension(testImagePath),
                MimeType = Path.GetExtension(testImagePath)
            };
            imagesDir = "~/images".MapHostAbsolutePath();
            testImagePath = Directory.EnumerateFiles("~/testImages".MapHostAbsolutePath()).Last();
            imageBytes = File.ReadAllBytes(testImagePath);

            SetupMocks();
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
                UniqueName = goodManufacturer.UniqueName,
                ImageBytes = imageBytes,
                SeoFileName = goodDisplayImage.SeoFileName,
                MimeType = goodDisplayImage.MimeType
            };

            var createManufacturerResponse = client.Post(createRequest);
            createManufacturerResponse.ShouldNotBeNull();
            createManufacturerResponse.Result.ShouldNotBeNull();
            createManufacturerResponse.Result.Id.ShouldNotBeNull();
            createManufacturerResponse.Result.Id.ShouldBeNotBeTheSameAs(ObjectId.Empty);

            var createRequest2 = new CreateManufacturer
            {
                Name = goodManufacturer.Name + "2",
                UniqueName = goodManufacturer.UniqueName + "-2",
                ImageBytes = imageBytes,
                SeoFileName = goodDisplayImage.SeoFileName + "2",
                MimeType = goodDisplayImage.MimeType
            };

            var createManufacturerResponse2 = client.Post(createRequest2);
            createManufacturerResponse2.ShouldNotBeNull();
            createManufacturerResponse2.Result.ShouldNotBeNull();
            createManufacturerResponse2.Result.Id.ShouldNotBeNull();
            createManufacturerResponse2.Result.Id.ShouldBeNotBeTheSameAs(ObjectId.Empty);

            // Get all

            var getManufacturersRequest = new GetManufacturers
            {
                Name = string.Empty
            };

            var getManufacturersResponse = client.Get(getManufacturersRequest);
            getManufacturersResponse.ShouldNotBeNull();
            getManufacturersResponse.Result.ShouldNotBeNull();
            getManufacturersResponse.Result.Count.ShouldEqual(2);
            
            // Get by name
            // Get find by name works in "LIKE '%Name%'" manner, so I provide name which is one in db e.g. Arduino2
            var getManufacturersByNameRequest = new GetManufacturers
            {
                Name = goodManufacturer.Name + "2"
            };

            var getManufacturersByNameResponse = client.Get(getManufacturersByNameRequest);
            getManufacturersByNameResponse.ShouldNotBeNull();
            getManufacturersByNameResponse.Result.ShouldNotBeNull();
            getManufacturersByNameResponse.Result.Count.ShouldEqual(1);
            
            
            // Get by name
            // I will update first inserted item 
            var updateManufacturerRequest = new UpdateManufacturer
            {
                Id = createManufacturerResponse.Result.Id.ToString(),
                Name = createManufacturerResponse.Result.Name + "Updated",
                UniqueName = createManufacturerResponse.Result.UniqueName + "-updated"
            };

            var updateManufacturerResponse = client.Put(updateManufacturerRequest);
            updateManufacturerResponse.ShouldNotBeNull();
            updateManufacturerResponse.Result.ShouldNotBeNull();
            

            // Get by id
            var getManufacturerById = new GetManufacturer
            {
                Id = updateManufacturerResponse.Result.Id.ToString()

            };

            var getManufacturerByIdResponse = client.Get(getManufacturerById);
            getManufacturerByIdResponse.ShouldNotBeNull();
            getManufacturerByIdResponse.Result.ShouldNotBeNull();
            getManufacturerByIdResponse.Result.UniqueName.ShouldEqual(createManufacturerResponse.Result.UniqueName + "-updated");
            getManufacturerByIdResponse.Result.Name.ShouldEqual(createManufacturerResponse.Result.Name + "Updated");


            // Delete 
            var deleteRequest = new DeleteManufacturer
            {
                Id = getManufacturerByIdResponse.Result.Id.ToString()
            };

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
            Directory.Delete(imagesDir, true);

            var manufacturers = manufacturerService.GetAllManufacturers();

            foreach (var manufacturer in manufacturers)
            {
                manufacturerService.DeleteManufacturer(manufacturer.Id.ToString());
            }
            
        }
    }
}
