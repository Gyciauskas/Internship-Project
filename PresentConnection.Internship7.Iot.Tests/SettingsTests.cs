using MongoDB.Bson;
using NUnit.Framework;
using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.BusinessImplementation;
using PresentConnection.Internship7.Iot.Domain;
using System.Collections.Generic;
using PresentConnection.Internship7.Iot.Utils;
using CodeMash.Net;
using System.Linq;
namespace PresentConnection.Internship7.Iot.Tests
{

    [TestFixture]
    public class SettingsTests
    {
        private ISetingsService settingService;

        object GetSetting;

        [SetUp]
        public void SetUp()
        {
            settingService = new SettingsService();
        }


        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.Setting")]
        public void Can_insert_setting_to_database()
        {
            var setting = new Setting
            {
                SettingsAsJson = "username",
            };

            settingService.CreateSetting(setting);

            setting.ShouldNotBeNull();
            setting.Id.ShouldNotBeNull();
        }


        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.Setting")]
        public void Cannot_insert_setting_to_database_when_jason_is_not_provided()
        {
            var setting = new Setting
            {
                SettingsAsJson = "",
            };

            typeof(BusinessException).ShouldBeThrownBy(() => settingService.CreateSetting(setting));
        }


        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.Setting")]
        public void Can_get_one_setting()
        {
            var setting = new Setting
            {
                SettingsAsJson = "username",
            };
            settingService.CreateSetting(setting);

            setting.ShouldNotBeNull();
            setting.Id.ShouldNotBeNull();
        
            var settingFromDb = Db.Find<Setting>(_ => true).FirstOrDefault();

            settingFromDb.ShouldNotBeNull();
            settingFromDb.SettingsAsJson.ShouldNotBeNull();
            }


        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.Setting")]
        public void Can_update_settings_to_database()
        {

            var setting = new Setting
            {
                SettingsAsJson = "level",
            };

            settingService.CreateSetting(setting);

            // First insert setting to db
            setting.ShouldNotBeNull();
            setting.Id.ShouldNotBeNull();

            // Update name and send update to db
            setting.SettingsAsJson = "username";
            settingService.UpdateSetting(setting);

            // Get item from db and check if name was updated
            var settingFromDb = settingService.GetSetting(setting.Id.ToString());
            settingFromDb.ShouldNotBeNull();
            settingFromDb.SettingsAsJson.ShouldEqual("username");
        }


        [Test]
        [Category("Iot")]
        [Category("IntegrationTests.Setting")]
        public void Can_delete_setting_from_database()
        {
             var setting = new Setting
            {
                SettingsAsJson = "level",
            };
            settingService.CreateSetting(setting);
         
            // First insert setting to db
            setting.ShouldNotBeNull();
            setting.Id.ShouldNotBeNull();

            // Delete setting from db
            settingService.DeleteSetting(setting.Id.ToString());

            // Get item from db and check if name was updated
            var settingFromDb = settingService.GetSetting(setting.Id.ToString());

            // issue with CodeMash library. Should return null when not found - not default object
            settingFromDb.ShouldNotBeNull();
            settingFromDb.Id.ShouldEqual(ObjectId.Empty);
          }


        [TearDown]
        public void Dispose()
        {
            var settings = settingService.GetAllSettings();
            foreach (var setting in settings)
            {
             settingService.DeleteSetting(setting.Id.ToString());
            }
        }

    }
}
