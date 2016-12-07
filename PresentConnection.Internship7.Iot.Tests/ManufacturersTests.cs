using MongoDB.Bson;
using NUnit.Framework;
using System.Linq;
using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.BusinessImplementation;
using PresentConnection.Internship7.Iot.Domain;
using System.Collections.Generic;
using PresentConnection.Internship7.Iot.Utils;

namespace PresentConnection.Internship7.Iot.Tests
{
    [TestFixture]
    public class ManufacturersTests
    {
        private IManufacturerService manufacturerService;
        private Manufacturer goodManufacturer;

        [SetUp]
        public void SetUp()
        {
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
    }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.Manufacturer")]
        public void Can_insert_manufacturer_to_database()
        {
            manufacturerService.CreateManufacturer(goodManufacturer);

            goodManufacturer.ShouldNotBeNull();
            goodManufacturer.Id.ShouldNotBeNull();
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.Manufacturer")]
        public void Cannot_insert_manufacturer_to_database_when_name_is_not_provided()
        {
            goodManufacturer.Name = string.Empty;

            var exception = typeof(BusinessException).ShouldBeThrownBy(() => manufacturerService.CreateManufacturer(goodManufacturer));
            exception.Message.ShouldEqual("Cannot create manufacturer");
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.Manufacturer")]
        public void Cannot_insert_manufacturer_to_database_when_uniquename_is_not_provided()
        {
            goodManufacturer.UniqueName = string.Empty;

            var exception =   typeof(BusinessException).ShouldBeThrownBy(() => manufacturerService.CreateManufacturer(goodManufacturer));
            exception.Message.ShouldEqual("Cannot create manufacturer");
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.Manufacturer")]
        public void Cannot_insert_manufacturer_to_database_when_such_uniquename_exist()
        {
            manufacturerService.CreateManufacturer(goodManufacturer);

            var manufacturer2 = new Manufacturer
            {
                UniqueName = "raspberry-pi-3",
                Name = "Name 2",
                Images =
                {
                    "5821dcc11e9f341d4c6d0994"
                }
            };

            var exception = typeof(BusinessException).ShouldBeThrownBy(() => manufacturerService.CreateManufacturer(manufacturer2));

            var businessException = exception as BusinessException;
            businessException.ShouldNotBeNull();

            businessException?.Errors.SingleOrDefault(error => error.ErrorMessage.Equals("Property  should be unique in database and in correct format !"))
            .ShouldNotBeNull("Received different error message");

            exception.Message.ShouldEqual("Cannot create manufacturer");
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.Manufacturer")]
        public void Cannot_insert_manufacturer_to_database_when_uniquename_is_not_in_correct_format()
        {
            goodManufacturer.UniqueName = "Raspberry PI 3";

            var exception = typeof(BusinessException).ShouldBeThrownBy(() => manufacturerService.CreateManufacturer(goodManufacturer));

            var businessException = exception as BusinessException;
            businessException.ShouldNotBeNull();

            businessException?.Errors.SingleOrDefault(error => error.ErrorMessage.Equals("Property  should be unique in database and in correct format !"))
            .ShouldNotBeNull("Received different error message");

            exception.Message.ShouldEqual("Cannot create manufacturer");
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.Manufacturer")]
        public void Cannot_insert_manufacturer_to_database_when_uniquename_is_not_in_correct_format_unique_name_with_upercases()
        {
            goodManufacturer.UniqueName = "raspberry-PI-3";

            var exception = typeof(BusinessException).ShouldBeThrownBy(() => manufacturerService.CreateManufacturer(goodManufacturer));

            var businessException = exception as BusinessException;
            businessException.ShouldNotBeNull();

            businessException?.Errors.SingleOrDefault(error => error.ErrorMessage.Equals("Property  should be unique in database and in correct format !"))
            .ShouldNotBeNull("Received different error message");

            exception.Message.ShouldEqual("Cannot create manufacturer");
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.Manufacturer")]
        public void Cannot_insert_manufacturer_to_database_when_image_is_not_provided()
        {
            goodManufacturer.Images = null;

            var exception = typeof(BusinessException).ShouldBeThrownBy(() => manufacturerService.CreateManufacturer(goodManufacturer));

            var businessException = exception as BusinessException;
            businessException.ShouldNotBeNull();
            businessException?.Errors.SingleOrDefault(error => error.ErrorMessage.Equals("Property Images should contain at least one item!"))
            .ShouldNotBeNull("Received different error message");

            exception.Message.ShouldEqual("Cannot create manufacturer");
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.Manufacturer")]
        public void Can_get_manufacturer_by_id()
        {
            manufacturerService.CreateManufacturer(goodManufacturer);

            goodManufacturer.ShouldNotBeNull();
            goodManufacturer.Id.ShouldNotBeNull();

            var manufacturerFromDb = manufacturerService.GetManufacturer(goodManufacturer.Id.ToString());
            manufacturerFromDb.ShouldNotBeNull();
            manufacturerFromDb.Id.ShouldNotBeNull();
            manufacturerFromDb.UniqueName.ShouldEqual("raspberry-pi-3");
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.Manufacturer")]
        public void Can_get_all_manufacturers()
        {
            manufacturerService.CreateManufacturer(goodManufacturer);

            var manufacturer2 = new Manufacturer
            {
                Name = "Arduino1",
                Description = "description",
                UniqueName = "raspberry-pi-2",
                Images =
                {
                    "5821dcc11e9f341d4c6d0994"
                },
                Url = "url",
                IsVisible = true
            };

            manufacturerService.CreateManufacturer(manufacturer2);
            manufacturer2.ShouldNotBeNull();
            manufacturer2.Id.ShouldNotBeNull();

            var manufacturers = manufacturerService.GetAllManufacturers();

            manufacturers.ShouldBe<List<Manufacturer>>();
            (manufacturers.Count > 0).ShouldBeTrue();
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.Manufacturer")]
        public void Can_get_all_manufacturers_by_name()
        {
            manufacturerService.CreateManufacturer(goodManufacturer);
            var manufacturer2 = new Manufacturer()
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

            var manufacturer3 = new Manufacturer()
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

            manufacturerService.CreateManufacturer(manufacturer2);
            manufacturer2.ShouldNotBeNull();
            manufacturer2.Id.ShouldNotBeNull();

            manufacturerService.CreateManufacturer(manufacturer3);
            manufacturer3.ShouldNotBeNull();
            manufacturer3.Id.ShouldNotBeNull();
            
            var manufacturers = manufacturerService.GetAllManufacturers("Arduino 2");

            manufacturers.ShouldBe<List<Manufacturer>>();
            manufacturers.Count.ShouldEqual(1);
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.Manufacturer")]
        public void Can_get_all_manufacturers_by_case_insensetive_name()
        {
            var manufacturer1 = new Manufacturer()
            {
                Name = "Raspberry PI",
                Description = "...",
                IsVisible = true,
                UniqueName = "raspberry-pi",
                Url = "raspberry-pi"
            };

            var manufacturer2 = new Manufacturer()
            {
                Name = "Arduino",
                Description = "...",
                IsVisible = true,
                UniqueName = "arduino",
                Url = "arduino"
            };

            var manufacturer3 = new Manufacturer()
            {
                Name = "My Manufacturer",
                Description = "...",
                IsVisible = true,
                UniqueName = "my manu",
                Url = ""
            };

            manufacturerService.CreateManufacturer(manufacturer1);
            manufacturer1.ShouldNotBeNull();
            manufacturer1.Id.ShouldNotBeNull();


            manufacturerService.CreateManufacturer(manufacturer2);
            manufacturer2.ShouldNotBeNull();
            manufacturer2.Id.ShouldNotBeNull();

            manufacturerService.CreateManufacturer(manufacturer3);
            manufacturer3.ShouldNotBeNull();
            manufacturer3.Id.ShouldNotBeNull();

            var manufacturers = manufacturerService.GetAllManufacturers("my manufacturer");

            manufacturers.ShouldBe<List<Manufacturer>>();
            manufacturers.Count.ShouldEqual(1);
        }


        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.Manufacturer")]
        public void Can_get_all_manufacturers_by_incomplete_name()
        {
            var manufacturer1 = new Manufacturer()
            {
                Name = "Raspberry PI",
                Description = "...",
                IsVisible = true,
                UniqueName = "raspberry-pi",
                Url = "raspberry-pi"
            };

            var manufacturer2 = new Manufacturer()
            {
                Name = "Arduino",
                Description = "...",
                IsVisible = true,
                UniqueName = "arduino",
                Url = "arduino"
            };

            var manufacturer3 = new Manufacturer()
            {
                Name = "My Manufacturer",
                Description = "...",
                IsVisible = true,
                UniqueName = "my manu",
                Url = ""
            };

            manufacturerService.CreateManufacturer(manufacturer1);
            manufacturer1.ShouldNotBeNull();
            manufacturer1.Id.ShouldNotBeNull();


            manufacturerService.CreateManufacturer(manufacturer2);
            manufacturer2.ShouldNotBeNull();
            manufacturer2.Id.ShouldNotBeNull();

            manufacturerService.CreateManufacturer(manufacturer3);
            manufacturer3.ShouldNotBeNull();
            manufacturer3.Id.ShouldNotBeNull();

            var manufacturers = manufacturerService.GetAllManufacturers("manufacturer");

            manufacturers.ShouldBe<List<Manufacturer>>();
            manufacturers.Count.ShouldEqual(1);
        }


        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.Manufacturer")]
        public void Can_update_manufacturer_to_database()
        {
            manufacturerService.CreateManufacturer(goodManufacturer);

            goodManufacturer.ShouldNotBeNull();
            goodManufacturer.Id.ShouldNotBeNull();

            goodManufacturer.Name = "Arduino";
            manufacturerService.UdpdateManufacturer(goodManufacturer);

            var manufacturerFromDb = manufacturerService.GetManufacturer(goodManufacturer.Id.ToString());
            manufacturerFromDb.ShouldNotBeNull();
            manufacturerFromDb.Name.ShouldEqual("Arduino");
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.Manufacturer")]
        public void Can_delete_manufacturer_from_database()
        {
            manufacturerService.CreateManufacturer(goodManufacturer);

            goodManufacturer.ShouldNotBeNull();
            goodManufacturer.Id.ShouldNotBeNull();

            manufacturerService.DeleteManufacturer(goodManufacturer.Id.ToString());

            var manufacturerFromDb = manufacturerService.GetManufacturer(goodManufacturer.Id.ToString());

            manufacturerFromDb.ShouldNotBeNull();
            manufacturerFromDb.Id.ShouldEqual(ObjectId.Empty);
        }

        [TearDown]
        public void Dispose()
        {
            var manufacturers = manufacturerService.GetAllManufacturers();
            foreach (var manufacturer in manufacturers)
            {
                manufacturerService.DeleteManufacturer(manufacturer.Id.ToString());
            }
        }
    }
}
