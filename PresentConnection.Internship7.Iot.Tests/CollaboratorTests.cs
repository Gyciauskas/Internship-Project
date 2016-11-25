﻿using MongoDB.Bson;
using NUnit.Framework;
using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.BusinessImplementation;
using PresentConnection.Internship7.Iot.Domain;
using System.Collections.Generic;
using PresentConnection.Internship7.Iot.Utils;

namespace PresentConnection.Internship7.Iot.Tests
{
    [TestFixture]
    public class CollaboratorTests
    {
        private ICollaboratorService collaboratorService;

        [SetUp]
        public void SetUp()
        {
            collaboratorService = new CollaboratorService();
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.Collaborator")]
        public void Can_insert_collaborator_to_database()
        {
            List<string> PermSet = new List<string>();
            PermSet.Add("Write");
            PermSet.Add("Read");
            PermSet.Add("Change");
            var collaborator = new Collaborator()
            {
                UserId = "125",
                Email = "TestName125@testemail.com",
                Name = "TestName125",
                Phone = "860000125",
                PermissionsSet = PermSet,
            };
            collaboratorService.CreateCollaborator(collaborator);

            collaborator.ShouldNotBeNull();
            collaborator.Id.ShouldNotBeNull();
        }


        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.Collaborator")]
        public void Cannot_insert_collaborator_to_database_when_userid_is_not_provided()
        {
            List<string> PermSet = new List<string>();
            PermSet.Add("Write");
            PermSet.Add("Read");
            PermSet.Add("Change");
            var collaborator = new Collaborator()
            {
                UserId = "",
                Email = "TestName125@testemail.com",
                Name = "TestName125",
                Phone = "860000125",
                PermissionsSet = PermSet,
            };

            typeof(BusinessException).ShouldBeThrownBy(() => collaboratorService.CreateCollaborator(collaborator));
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.Collaborator")]
        public void Cannot_insert_collaborator_to_database_when_email_is_not_provided()
        {
            List<string> PermSet = new List<string>();
            PermSet.Add("Write");
            PermSet.Add("Read");
            PermSet.Add("Change");
            var collaborator = new Collaborator()
            {
                UserId = "125",
                Email = "",
                Name = "TestName125",
                Phone = "860000125",
                PermissionsSet = PermSet,
            };

            typeof(BusinessException).ShouldBeThrownBy(() => collaboratorService.CreateCollaborator(collaborator));
        }


        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.Collaborator")]
        public void Can_get_collaborator_by_id()
        {
            List<string> PermSet = new List<string>();
            PermSet.Add("Write");
            PermSet.Add("Read");
            PermSet.Add("Change");
            var collaborator = new Collaborator()
            {
                UserId = "125",
                Email = "TestName125@testemail.com",
                Name = "TestName125",
                Phone = "860000125",
                PermissionsSet = PermSet,
            };
            collaboratorService.CreateCollaborator(collaborator);

            collaborator.ShouldNotBeNull();
            collaborator.Id.ShouldNotBeNull();

            var collaboratorFromDb = collaboratorService.GetCollaborator(collaborator.Id.ToString());
            collaboratorFromDb.ShouldNotBeNull();
            collaboratorFromDb.Id.ShouldNotBeNull();
            collaboratorFromDb.Name.ShouldEqual("Raspberry PI");

        }


        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.Collaborator")]
        public void Can_get_all_collaborators()
        {
            List<string> PermSet = new List<string>();
            PermSet.Add("Write");
            PermSet.Add("Read");
            PermSet.Add("Change");
            var collaborator1 = new Collaborator()
            {
                UserId = "125",
                Email = "TestName125@testemail.com",
                Name = "TestName125",
                Phone = "860000125",
                PermissionsSet = PermSet,
            };

            List<string> PermSet2 = new List<string>();
            PermSet.Add("Write");
            PermSet.Add("Read");
            PermSet.Add("Change");
            var collaborator2 = new Collaborator()
            {
                UserId = "124",
                Email = "TestName124@testemail.com",
                Name = "TestName124",
                Phone = "860000124",
                PermissionsSet = PermSet2,
            };

            collaboratorService.CreateCollaborator(collaborator1);
            collaborator1.ShouldNotBeNull();
            collaborator1.Id.ShouldNotBeNull();


            collaboratorService.CreateCollaborator(collaborator2);
            collaborator2.ShouldNotBeNull();
            collaborator2.Id.ShouldNotBeNull();



            var collaborators = collaboratorService.GetAllCollaborator();

            collaborators.ShouldBe<List<Collaborator>>();
            (collaborators.Count > 0).ShouldBeTrue();
        }


        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.Collaborator")]
        public void Can_get_all_collaborators_by_name()
        {
            List<string> PermSet = new List<string>();
            PermSet.Add("Write");
            PermSet.Add("Read");
            PermSet.Add("Change");
            var collaborator1 = new Collaborator()
            {
                UserId = "125",
                Email = "TestName125@testemail.com",
                Name = "TestName125",
                Phone = "860000125",
                PermissionsSet = PermSet,
            };

            List<string> PermSet2 = new List<string>();
            PermSet.Add("Write");
            PermSet.Add("Read");
            PermSet.Add("Change");
            var collaborator2 = new Collaborator()
            {
                UserId = "124",
                Email = "TestName124@testemail.com",
                Name = "TestName124",
                Phone = "860000124",
                PermissionsSet = PermSet2,
            };

            List<string> PermSet3 = new List<string>();
            PermSet.Add("Write");
            PermSet.Add("Read");
            PermSet.Add("Change");
            var collaborator3 = new Collaborator()
            {
                UserId = "123",
                Email = "TestName123@testemail.com",
                Name = "TestName123",
                Phone = "860000123",
                PermissionsSet = PermSet,
            };

            collaboratorService.CreateCollaborator(collaborator1);
            collaborator1.ShouldNotBeNull();
            collaborator1.Id.ShouldNotBeNull();


            collaboratorService.CreateCollaborator(collaborator2);
            collaborator2.ShouldNotBeNull();
            collaborator2.Id.ShouldNotBeNull();

            collaboratorService.CreateCollaborator(collaborator3);
            collaborator3.ShouldNotBeNull();
            collaborator3.Id.ShouldNotBeNull();
            
            var collaborators = collaboratorService.GetAllCollaborator("My Collaborator");

            collaborators.ShouldBe<List<Collaborator>>();
            collaborators.Count.ShouldEqual(1);
        }


        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.Collaborator")]
        public void Can_update_collaborator_to_database()
        {
            List<string> PermSet = new List<string>();
            PermSet.Add("Write");
            PermSet.Add("Read");
            PermSet.Add("Change");
            var collaborator = new Collaborator()
            {
                UserId = "125",
                Email = "TestName125@testemail.com",
                Name = "TestName125",
                Phone = "860000125",
                PermissionsSet = PermSet,
            };

            collaboratorService.CreateCollaborator(collaborator);

            // First insert collaborator to db
            collaborator.ShouldNotBeNull();
            collaborator.Id.ShouldNotBeNull();

            // Update name and send update to db
            collaborator.Name = "TestName126";
            collaboratorService.UpdateCollaborator(collaborator);

            // Get item from db and check if name was updated
            var collaboratorFromDb = collaboratorService.GetCollaborator(collaborator.Id.ToString());
            collaboratorFromDb.ShouldNotBeNull();
            collaboratorFromDb.Name.ShouldEqual("TestName126");
        }


        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.Collaborator")]
        public void Can_delete_collaborator_from_database()
        {
            List<string> PermSet = new List<string>();
            PermSet.Add("Write");
            PermSet.Add("Read");
            PermSet.Add("Change");
            var collaborator = new Collaborator()
            {
                UserId = "125",
                Email = "TestName125@testemail.com",
                Name = "TestName125",
                Phone = "860000125",
                PermissionsSet = PermSet,
            };

            collaboratorService.CreateCollaborator(collaborator);

            // First insert collaborator to db
            collaborator.ShouldNotBeNull();
            collaborator.Id.ShouldNotBeNull();

            // Delete collaborator from db
            collaboratorService.DeleteCollaborator(collaborator.Id.ToString());

            // Get item from db and check if name was updated
            var collaboratorFromDb = collaboratorService.GetCollaborator(collaborator.Id.ToString());

            // issue with CodeMash library. Should return null when not found - not default object
            collaboratorFromDb.ShouldNotBeNull();
            collaboratorFromDb.Id.ShouldEqual(ObjectId.Empty);
        }

        
        [TearDown]
        public void Dispose()
        {
            var collaborators = collaboratorService.GetAllCollaborator();
            foreach (var collaborator in collaborators)
            {
                collaboratorService.DeleteCollaborator(collaborator.Id.ToString());
            }
        }
    }
}
