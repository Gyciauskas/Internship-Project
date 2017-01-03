using MongoDB.Bson;
using NUnit.Framework;
using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.BusinessImplementation;
using PresentConnection.Internship7.Iot.Domain;
using PresentConnection.Internship7.Iot.ServiceModels;
using ServiceStack;
using Assert = NUnit.Framework.Assert;

namespace PresentConnection.Internship7.Iot.Tests
{
    [TestFixture]
    public class ManufacturerServiceTests : TestsBase
    {
        private IManufacturerService manufacturerService;
        private Manufacturer goodManufacturer;
        private ServiceStackHost appHost;

        [SetUp]
        public void SetUp()
        {
            //Start your AppHost on TestFixtureSetUp
            appHost = new AppHost()
                .Init()
                .Start(BaseUri);
            
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

            var manufacturers = manufacturerService.GetAllManufacturers();

            foreach (var manufacturer in manufacturers)
            {
                manufacturerService.DeleteManufacturer(manufacturer.Id.ToString());
            }

        }


        [Test]
        [Category("ServiceTests")]
        [Category("Manufacturer")]
        public void Can_do_CRUD_manufacturer_to_database()
        {
            var client = new JsonServiceClient(BaseUri);

            // Create

            var createRequest = new CreateManufacturer
            {
                Name = goodManufacturer.Name,
                UniqueName = goodManufacturer.UniqueName,
                Images = goodManufacturer.Images
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
                Images = goodManufacturer.Images
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

        

        [TearDown]
        public void Dispose()
        {
            appHost.Dispose();
        }
    }
}
