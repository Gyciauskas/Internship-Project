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
    public class LookupsTests
    {
        private ILookupService lookupService;

        [SetUp]
        public void SetUp()
        {
            lookupService = new LookupService();
        }

        [Test]
        [Category("Lookup")]
        public void Can_insert_lookup_to_database()
        {
            var lookup = new Lookup()
            {
                Name = "Name",
                Description = "Short description",
                Order = 1,
                Type = "Widget"
            };
            lookupService.CreateLookup(lookup);

            lookup.ShouldNotBeNull();
            lookup.Id.ShouldNotBeNull();
        }

        [Test]
        [Category("Lookup")]
        public void Cannot_insert_lookup_to_database_when_name_is_not_provided()
        {
            var lookup = new Lookup()
            {
                Name = "",
                Description = "Short description",
                Order = 1,
                Type = "Widget"
            };

            typeof(BusinessException).ShouldBeThrownBy(() => lookupService.CreateLookup(lookup));
        }

        [Test]
        [Category("Lookup")]
        public void Cannot_insert_lookup_to_database_when_type_is_not_provided()
        {
            var lookup = new Lookup()
            {
                Name = "Name",
                Description = "Short description",
                Order = 1,
                Type = ""
            };

            typeof(BusinessException).ShouldBeThrownBy(() => lookupService.CreateLookup(lookup));
        }

        [Test]
        [Category("Lookup")]
        public void Can_get_lookup_by_id()
        {
            var lookup = new Lookup()
            {
                Name = "Name",
                Description = "Short description",
                Order = 1,
                Type = "Widget"
            };
            lookupService.CreateLookup(lookup);

            lookup.ShouldNotBeNull();
            lookup.Id.ShouldNotBeNull();

            var lookupFromDb = lookupService.GetLookup(lookup.Id.ToString());
            lookupFromDb.ShouldNotBeNull();
            lookupFromDb.Id.ShouldNotBeNull();
            lookupFromDb.Name.ShouldEqual("Name");
        }

        [Test]
        [Category("Lookup")]
        public void Can_get_all_lookups()
        {
            var lookup1 = new Lookup()
            {
                Name = "Name",
                Description = "Short description",
                Order = 1,
                Type = "Widget"
            };

            var lookup2 = new Lookup()
            {
                Name = "Name2",
                Description = "Short description",
                Order = 2,
                Type = "Industry"
            };

            lookupService.CreateLookup(lookup1);
            lookup1.ShouldNotBeNull();
            lookup1.Id.ShouldNotBeNull();

            lookupService.CreateLookup(lookup2);
            lookup2.ShouldNotBeNull();
            lookup2.Id.ShouldNotBeNull();

            var lookups = lookupService.GetAllLookups();

            lookups.ShouldBe<List<Lookup>>();
            (lookups.Count > 0).ShouldBeTrue();
        }

        [Test]
        [Category("Lookup")]
        public void Can_get_all_lookups_by_type()
        {
            var lookup1 = new Lookup()
            {
                Name = "Name",
                Description = "Short description",
                Order = 1,
                Type = "Widget"
            };

            var lookup2 = new Lookup()
            {
                Name = "Name2",
                Description = "Short description",
                Order = 2,
                Type = "Industry"
            };

            var lookup3 = new Lookup()
            {
                Name = "Name3",
                Description = "Short description",
                Order = 3,
                Type = "Type"
            };

            lookupService.CreateLookup(lookup1);
            lookup1.ShouldNotBeNull();
            lookup1.Id.ShouldNotBeNull();

            lookupService.CreateLookup(lookup2);
            lookup2.ShouldNotBeNull();
            lookup2.Id.ShouldNotBeNull();

            lookupService.CreateLookup(lookup3);
            lookup2.ShouldNotBeNull();
            lookup2.Id.ShouldNotBeNull();

            var lookups = lookupService.GetAllLookups("Type");

            lookups.ShouldBe<List<Lookup>>();
            lookups.Count.ShouldEqual(1);
        }

        [Test]
        [Category("Lookup")]
        public void Can_update_lookup_to_database()
        {
            var lookup = new Lookup()
            {
                Name = "Name",
                Description = "Short description",
                Order = 1,
                Type = "Widget"
            };
            lookupService.CreateLookup(lookup);

            lookup.ShouldNotBeNull();
            lookup.Id.ShouldNotBeNull();

            lookup.Name = "Edited";
            lookupService.UpdateLookup(lookup);

            var lookupFromDb = lookupService.GetLookup(lookup.Id.ToString());
            lookupFromDb.ShouldNotBeNull();
            lookupFromDb.Name.ShouldEqual("Edited");
        }

        [Test]
        [Category("Lookup")]
        public void Can_delete_lookup_from_database()
        {
            var lookup = new Lookup()
            {
                Name = "Name",
                Description = "Short description",
                Order = 1,
                Type = "Widget"
            };
            lookupService.CreateLookup(lookup);

            lookup.ShouldNotBeNull();
            lookup.Id.ShouldNotBeNull();

            lookupService.DeleteLookup(lookup.Id.ToString());
            var lookupFromDb = lookupService.GetLookup(lookup.Id.ToString());

            lookupFromDb.ShouldNotBeNull();
            lookupFromDb.Id.ShouldEqual(ObjectId.Empty);
        }

        [TearDown]
        public void Dispose()
        {
            var lookups = lookupService.GetAllLookups();
            foreach (var lookup in lookups)
            {
                lookupService.DeleteLookup(lookup.Id.ToString());
            }
        }

    }
}
