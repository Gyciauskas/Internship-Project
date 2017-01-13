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
    public partial class CollaboratorTests
    {
        [Test]
        [Category("Collaborator")]
        public void Can_insert_collaborator_to_database()
        {
            collaboratorService.CreateCollaborator(goodCollaborator);

            goodCollaborator.ShouldNotBeNull();
            goodCollaborator.Id.ShouldNotBeNull();
        }


        [Test]
        [Category("Collaborator")]
        public void Cannot_insert_collaborator_to_database_when_userid_is_not_provided()
        {
            goodCollaborator.UserId = string.Empty;

            typeof(BusinessException).ShouldBeThrownBy(() => collaboratorService.CreateCollaborator(goodCollaborator));
        }

        [Test]
        [Category("Collaborator")]
        public void Cannot_insert_collaborator_to_database_when_email_is_not_provided()
        {
            goodCollaborator.Email = string.Empty;

            typeof(BusinessException).ShouldBeThrownBy(() => collaboratorService.CreateCollaborator(goodCollaborator));
        }


        [Test]
        [Category("Collaborator")]
        public void Can_get_collaborator_by_id()
        {
            collaboratorService.CreateCollaborator(goodCollaborator);

            goodCollaborator.ShouldNotBeNull();
            goodCollaborator.Id.ShouldNotBeNull();

            var collaboratorFromDb = collaboratorService.GetCollaborator(goodCollaborator.Id.ToString());
            collaboratorFromDb.ShouldNotBeNull();
            collaboratorFromDb.Id.ShouldNotBeNull();
            collaboratorFromDb.Name.ShouldEqual("Some Name");
        }


        [Test]
        [Category("Collaborator")]
        public void Can_get_all_collaborators()
        {
            var collaborator2 = new Collaborator()
            {
                UserId = "124",
                Email = "TestName124@testemail.com",
                Name = "TestName124",
                Phone = "860000124",
                Permissions = goodCollaborator.Permissions,
            };

            collaboratorService.CreateCollaborator(goodCollaborator);
            goodCollaborator.ShouldNotBeNull();
            goodCollaborator.Id.ShouldNotBeNull();


            collaboratorService.CreateCollaborator(collaborator2);
            collaborator2.ShouldNotBeNull();
            collaborator2.Id.ShouldNotBeNull();



            var collaborators = collaboratorService.GetAllCollaborators();

            collaborators.ShouldBe<List<Collaborator>>();
            (collaborators.Count > 0).ShouldBeTrue();
        }


        [Test]
        [Category("Collaborator")]
        public void Can_get_all_collaborators_by_name()
        {
            var collaborator2 = new Collaborator()
            {
                UserId = "124",
                Email = "TestName124@testemail.com",
                Name = "TestName12488888",
                Phone = "860000124",
                Permissions = goodCollaborator.Permissions,
            };

            var collaborator3 = new Collaborator()
            {
                UserId = "123",
                Email = "TestName123@testemail.com",
                Name = "TestName12388888",
                Phone = "860000123",
                Permissions = goodCollaborator.Permissions,
            };

            collaboratorService.CreateCollaborator(goodCollaborator);
            goodCollaborator.ShouldNotBeNull();
            goodCollaborator.Id.ShouldNotBeNull();


            collaboratorService.CreateCollaborator(collaborator2);
            collaborator2.ShouldNotBeNull();
            collaborator2.Id.ShouldNotBeNull();

            collaboratorService.CreateCollaborator(collaborator3);
            collaborator3.ShouldNotBeNull();
            collaborator3.Id.ShouldNotBeNull();
            
            var collaborators = collaboratorService.GetAllCollaborators("TestName12388888");

            collaborators.ShouldBe<List<Collaborator>>();
            collaborators.Count.ShouldEqual(1);
        }


        [Test]
        [Category("Collaborator")]
        public void Can_update_collaborator_to_database()
        {
            collaboratorService.CreateCollaborator(goodCollaborator);

            // First insert collaborator to db
            goodCollaborator.ShouldNotBeNull();
            goodCollaborator.Id.ShouldNotBeNull();

            // Update name and send update to db
            goodCollaborator.Name = "TestName126";
            collaboratorService.UpdateCollaborator(goodCollaborator);

            // Get item from db and check if name was updated
            var collaboratorFromDb = collaboratorService.GetCollaborator(goodCollaborator.Id.ToString());
            collaboratorFromDb.ShouldNotBeNull();
            collaboratorFromDb.Name.ShouldEqual("TestName126");
        }


        [Test]
        [Category("Collaborator")]
        public void Can_delete_collaborator_from_database()
        {
            collaboratorService.CreateCollaborator(goodCollaborator);

            // First insert collaborator to db
            goodCollaborator.ShouldNotBeNull();
            goodCollaborator.Id.ShouldNotBeNull();

            // Delete collaborator from db
            collaboratorService.DeleteCollaborator(goodCollaborator.Id.ToString());

            // Get item from db and check if name was updated
            var collaboratorFromDb = collaboratorService.GetCollaborator(goodCollaborator.Id.ToString());

            // issue with CodeMash library. Should return null when not found - not default object
            collaboratorFromDb.ShouldNotBeNull();
            collaboratorFromDb.Id.ShouldEqual(ObjectId.Empty);
        }
    }
}
