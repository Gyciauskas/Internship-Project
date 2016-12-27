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
        public List<string> rules;
        public List<string> permissions;

        [SetUp]
        public void SetUp()
        {

            rules = new List<string>();
            rules.Add("rule1");
            rules.Add("rule2");
            permissions = new List<string>();
            permissions.Add("permission1");
            permissions.Add("permission2");
            
            userService = new UserService();
           
        }


        [Test]
        [Category("IntegrationTests")]
        [Category("User")]
        public void Can_insert_user_to_database()
        {
          
            var user = new User()
            {
                FullName = "John Brand",
                Rules = rules,
                Permissions = permissions,
                Email = "john.brand@gmail.com"
            };

            userService.CreateUser(user);
            user.ShouldNotBeNull();
            user.Id.ShouldNotBeNull();
        }


        [Test]
        [Category("IntegrationTests")]
        [Category("User")]
        public void Cannot_insert_user_to_database_when_fullname_is_not_provided()
        {
          
            var user = new User()
            {
                FullName = "",
            };

            typeof(BusinessException).ShouldBeThrownBy(() => userService.CreateUser(user));
        }
        

        [Test]
        [Category("IntegrationTests")]
        [Category("User")]
        public void Cannot_insert_user_to_database_when_rules_is_not_provided()
        {

            var user = new User()
            {
                FullName = "John Brand",
                Rules = null,
                Permissions = permissions,
                Email = "john.brand@gmail.com"
            };

            typeof(BusinessException).ShouldBeThrownBy(() => userService.CreateUser(user));
        }


        [Test]
        [Category("IntegrationTests")]
        [Category("User")]
        public void Cannot_insert_user_to_database_when_permisions_is_not_provided()
        {
            
            var user = new User()
            {
                FullName = "John Brand",
                Rules = rules,
                Permissions = null,
                Email = "john.brand@gmail.com"
            };

            typeof(BusinessException).ShouldBeThrownBy(() => userService.CreateUser(user));
        }


        [Test]
        [Category("IntegrationTests")]
        [Category("User")]
        public void Cannot_insert_user_to_database_when_email_is_not_provided()
        {
          
            var user = new User()
            {
                Email = ""
            };

            typeof(BusinessException).ShouldBeThrownBy(() => userService.CreateUser(user));
        }
        

        [Test]
        [Category("IntegrationTests")]
        [Category("User")]
        public void Can_get_user_by_id()
        {
            
            var user = new User()
            {
                FullName = "John Brand",
                Rules = rules,
                Permissions = permissions,
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
        [Category("IntegrationTests")]
        [Category("User")]
        public void Can_get_all_users()
        {
          
            var user = new User()
            {
                FullName = "John Brand",
                Rules = rules,
                Permissions = permissions,
                Email = "john.brand@gmail.com"
            };

            var user1 = new User()
            {
                FullName = "Mark Smith",
                Rules = rules,
                Permissions = permissions,
                Email = "m.smith@gmail.com"
            };

            userService.CreateUser(user);
            user.ShouldNotBeNull();
            user.Id.ShouldNotBeNull();
 
            userService.CreateUser(user1);
            user1.ShouldNotBeNull();
            user1.Id.ShouldNotBeNull();

            var users = userService.GetAllUsers();

            users.ShouldBe<List<User>>();
            (users.Count > 0).ShouldBeTrue();
        }

        [Test]
        [Category("IntegrationTests")]
        [Category("User")]
        public void Can_get_all_users_by_name()
        {
         
            var user = new User()
            {
                FullName = "John Brand",
                Rules = rules,
                Permissions = permissions,
                Email = "john.brand@gmail.com"
            };
                      
            var user1 = new User()
            {
                FullName = "Mark Smith",
                Rules = rules,
                Permissions = permissions,
                Email = "m.smith@gmail.com"
            };
           
            var user2 = new User()
            {
                FullName = "Linda Bruss",
                Rules = rules,
                Permissions = permissions,
                Email = "l.bruss@gmail.com"
            };

            userService.CreateUser(user);
            user.ShouldNotBeNull();
            user.Id.ShouldNotBeNull();

            userService.CreateUser(user1);
            user1.ShouldNotBeNull();
            user1.Id.ShouldNotBeNull();

            userService.CreateUser(user2);
            user2.ShouldNotBeNull();
            user2.Id.ShouldNotBeNull();


            var users = userService.GetAllUsers("Mark Smith");

            users.ShouldBe<List<User>>();
            users.Count.ShouldEqual(1);
        }

        [Test]
        [Category("IntegrationTests")]
        [Category("User")]
        public void Can_update_user_to_database()
        {
         
            var user = new User()
            {
                FullName = "John Brand",
                Rules = rules,
                Permissions = permissions,
                Email = "john.brand@gmail.com"
            };
    
            userService.CreateUser(user);

            user.ShouldNotBeNull();
            user.Id.ShouldNotBeNull();

            user.FullName = "Peter Frost";
            userService.UpdateUser(user);

            var userFromDb = userService.GetUser(user.Id.ToString());
            userFromDb.ShouldNotBeNull();
            userFromDb.FullName.ShouldEqual("Peter Frost");

        }


        [Test]
        [Category("IntegrationTests")]
        [Category("User")]
        public void Can_delete_user_from_database()
        {
         
            var user = new User()
            {
                FullName = "John Brand",
                Rules = rules,
                Permissions = permissions,
                Email = "john.brand@gmail.com"
            };

            userService.CreateUser(user);

            user.ShouldNotBeNull();
            user.Id.ShouldNotBeNull();

            userService.DeleteUser(user.Id.ToString());

            var userFromDb = userService.GetUser(user.Id.ToString());

            userFromDb.ShouldNotBeNull();
            userFromDb.Id.ShouldEqual(ObjectId.Empty);
        }


        [TearDown]
        public void Dispose()
        {
            var users = userService.GetAllUsers();
            foreach (var user in users)
            {
                userService.DeleteUser(user.Id.ToString());
            }
        }


    }
}

