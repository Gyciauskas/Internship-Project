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
    public class ManufacturersTests
    {
        private IManufacturerService manufacturerService;

        [SetUp]
        public void SetUp()
        {
            manufacturerService = new ManufacturerService();
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.Manufacturer")]
        public void Can_insert_manufacturer_to_database()
        {
            var manufacturer = new Manufacturer()
            {
                Name = "Raspberry PI",
                Description = "...",
                IsVisible = true,
                UniqueName = "raspberry-pi",
                Url = "raspberry-pi"
            };
            manufacturerService.CreateManufacturer(manufacturer);

            manufacturer.ShouldNotBeNull();
            manufacturer.Id.ShouldNotBeNull();
        }


        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.Manufacturer")]
        public void Cannot_insert_manufacturer_to_database_when_name_is_not_provided()
        {
            var manufacturer = new Manufacturer()
            {
                Name = "",
                Description = "...",
                IsVisible = true,
                UniqueName = "raspberry-pi",
                Url = "raspberry-pi"
            };

            typeof(BusinessException).ShouldBeThrownBy(() => manufacturerService.CreateManufacturer(manufacturer));
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.Manufacturer")]
        public void Cannot_insert_manufacturer_to_database_when_uniquename_is_not_provided()
        {
            var manufacturer = new Manufacturer()
            {
                Name = "Raspberry PI",
                Description = "...",
                IsVisible = true,
                UniqueName = "",
                Url = "raspberry-pi"
            };

            typeof(BusinessException).ShouldBeThrownBy(() => manufacturerService.CreateManufacturer(manufacturer));
        }


        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.Manufacturer")]
        public void Can_get_manufacturer_by_id()
        {
            var manufacturer = new Manufacturer()
            {
                Name = "Raspberry PI",
                Description = "...",
                IsVisible = true,
                UniqueName = "raspberry-pi",
                Url = "raspberry-pi"
            };
            manufacturerService.CreateManufacturer(manufacturer);

            manufacturer.ShouldNotBeNull();
            manufacturer.Id.ShouldNotBeNull();

            var manufacturerFromDb = manufacturerService.GetManufacturer(manufacturer.Id.ToString());
            manufacturerFromDb.ShouldNotBeNull();
            manufacturerFromDb.Id.ShouldNotBeNull();
            manufacturerFromDb.Name.ShouldEqual("Raspberry PI");

        }


        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.Manufacturer")]
        public void Can_get_all_manufacturers()
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

            manufacturerService.CreateManufacturer(manufacturer1);
            manufacturer1.ShouldNotBeNull();
            manufacturer1.Id.ShouldNotBeNull();


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
            
            var manufacturers = manufacturerService.GetAllManufacturers("My Manufacturer");

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
            var manufacturer = new Manufacturer()
            {
                Name = "Raspberry PI",
                Description = "...",
                IsVisible = true,
                UniqueName = "raspberry-pi",
                Url = "raspberry-pi"
            };

            manufacturerService.CreateManufacturer(manufacturer);

            // First insert manufacturer to db
            manufacturer.ShouldNotBeNull();
            manufacturer.Id.ShouldNotBeNull();

            // Update name and send update to db
            manufacturer.Name = "Arduino";
            manufacturerService.UdpdateManufacturer(manufacturer);

            // Get item from db and check if name was updated
            var manufacturerFromDb = manufacturerService.GetManufacturer(manufacturer.Id.ToString());
            manufacturerFromDb.ShouldNotBeNull();
            manufacturerFromDb.Name.ShouldEqual("Arduino");
        }


        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.Manufacturer")]
        public void Can_delete_manufacturer_from_database()
        {
            var manufacturer = new Manufacturer()
            {
                Name = "Raspberry PI",
                Description = "...",
                IsVisible = true,
                UniqueName = "raspberry-pi",
                Url = "raspberry-pi"
            };

            manufacturerService.CreateManufacturer(manufacturer);

            // First insert manufacturer to db
            manufacturer.ShouldNotBeNull();
            manufacturer.Id.ShouldNotBeNull();

            // Delete manufacturer from db
            manufacturerService.DeleteManufacturer(manufacturer.Id.ToString());

            // Get item from db and check if name was updated
            var manufacturerFromDb = manufacturerService.GetManufacturer(manufacturer.Id.ToString());

            // issue with CodeMash library. Should return null when not found - not default object
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
