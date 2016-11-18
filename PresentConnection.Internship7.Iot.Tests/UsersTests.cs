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
    public class UsersTests
    {
        public IUserService userService;

             

        [SetUp]
        public void SetUp()
        {
            userService = new UserService();
       
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.User")]
        public void Can_insert_user_to_database()
        {
            List<string> rules = new List<string>();
            rules.Add("rule1");
            rules.Add("rule2");
            List<string> permisions = new List<string>();
            permisions.Add("permision1");
            permisions.Add("permision2");


            var user = new User()
            {

                FullName = "John Brand",
                Rules = rules,
                Permisions = permisions,
                Email = "john.brand@gmail.com"
            };


            userService.CreateUser(user);
            user.ShouldNotBeNull();
            user.Id.ShouldNotBeNull();
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.User")]
        public void Cannot_insert_user_to_database_when_fullname_is_not_provided()
        {
            List<string> rules = new List<string>();
            rules.Add("rule1");
            rules.Add("rule2");
            List<string> permisions = new List<string>();
            permisions.Add("permision1");
            permisions.Add("permision2");


            var user = new User()
            {

                FullName = "",
                Rules = rules,
                Permisions = permisions,
                Email = "john.brand@gmail.com"
            };

            typeof(BusinessException).ShouldBeThrownBy(() => userService.CreateUser(user));
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.User")]
        public void Cannot_insert_user_to_database_when_rules_is_not_provided()
        {
            List<string> rules = new List<string>();
            rules.Add("rule1");
            rules.Add("rule2");
            List<string> permisions = new List<string>();
            permisions.Add("permision1");
            permisions.Add("permision2");


            var user = new User()
            {

                FullName = "John Brand",
                Rules = null,
                Permisions = permisions,
                Email = "john.brand@gmail.com"
            };

            typeof(BusinessException).ShouldBeThrownBy(() => userService.CreateUser(user));
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.User")]
        public void Cannot_insert_user_to_database_when_permisions_is_not_provided()
        {
            List<string> rules = new List<string>();
            rules.Add("rule1");
            rules.Add("rule2");
            List<string> permisions = new List<string>();
            permisions.Add("permision1");
            permisions.Add("permision2");


            var user = new User()
            {

                FullName = "John Brand",
                Rules = rules,
                Permisions = null,
                Email = "john.brand@gmail.com"
            };

            typeof(BusinessException).ShouldBeThrownBy(() => userService.CreateUser(user));
        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.User")]
        public void Cannot_insert_user_to_database_when_email_is_not_provided()
        {
            List<string> rules = new List<string>();
            rules.Add("rule1");
            rules.Add("rule2");
            List<string> permisions = new List<string>();
            permisions.Add("permision1");
            permisions.Add("permision2");


            var user = new User()
            {

                FullName = "John Brand",
                Rules = rules,
                Permisions = permisions,
                Email = ""
            };

            typeof(BusinessException).ShouldBeThrownBy(() => userService.CreateUser(user));
        }
        

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.User")]
        public void Can_get_user_by_id()
        {
            List<string> rules = new List<string>();
            rules.Add("rule1");
            rules.Add("rule2");
            List<string> permisions = new List<string>();
            permisions.Add("permision1");
            permisions.Add("permision2");


            var user = new User()
            {

                FullName = "John Brand",
                Rules = rules,
                Permisions = permisions,
                Email = "john.brand@gmail.com"
            };
            userService.CreateUser(user);

            user.ShouldNotBeNull();
            user.Id.ShouldNotBeNull();

            var userFromDb = userService.GetUser(user.Id.ToString());
            userFromDb.ShouldNotBeNull();
            userFromDb.Id.ShouldNotBeNull();
            userFromDb.FullName.ShouldEqual("John Brand");

        }

        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.User")]
        public void Can_get_all_users()
        {
            List<string> rules = new List<string>();
            rules.Add("rule1");
            rules.Add("rule2");
            List<string> permisions = new List<string>();
            permisions.Add("permision1");
            permisions.Add("permision2");


            var user = new User()
            {

                FullName = "John Brand",
                Rules = rules,
                Permisions = permisions,
                Email = "john.brand@gmail.com"
            };


            userService.CreateUser(user);
            user.ShouldNotBeNull();
            user.Id.ShouldNotBeNull();

            var user1 = new User()
            {

                FullName = "John Brand",
                Rules = rules,
                Permisions = permisions,
                Email = "john.brand@gmail.com"
            };


            userService.CreateUser(user1);
            user1.ShouldNotBeNull();
            user1.Id.ShouldNotBeNull();



            var users = userService.GetAllUsers();

            users.ShouldBe<List<User>>();
            (users.Count > 0).ShouldBeTrue();
        }




    }
}

