﻿using MongoDB.Bson;
using NUnit.Framework;
using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.BusinessImplementation;
using PresentConnection.Internship7.Iot.Domain;
using System.Collections.Generic;
using PresentConnection.Internship7.Iot.Utils;
using CodeMash.Net;
using System.Linq;
using System;
namespace PresentConnection.Internship7.Iot.Tests
{

    [TestFixture]
    public class SettingsTests
    {
        private ISetingsService settingsService;

        BsonDocument item = new BsonDocument
        {
          { "ModelName", "Display" },
          { "UnitName", 
             new BsonDocument
             {
               { "Type", "Float-HD" },
               { "Width", "400" },
               { "Height", "150" },
             }
          }
        };
                      
        [SetUp]
        public void SetUp()
        {
            settingsService = new SettingsService();
        }


        [Test]
        [Category("IntegrationTests")]
        [Category("Settings")]
        public void Can_update_or_insert_settings_to_database()
        {
            var settings = new Settings
            {
                SettingsAsJson = item
            };
           
            settingsService.UpdateOrInsertSettings(settings);

            settings.ShouldNotBeNull<Settings>();
            settings.Id.ShouldNotBeNull();
        }


        [Test]
        [Category("IntegrationTests")]
        [Category("Settings")]
        public void Cannot_insert_settings_to_database_when_jason_is_not_provided()
        {
            BsonDocument item1 = new BsonDocument();
        
            var settings = new Settings
            {
                SettingsAsJson = item1
            };

            typeof(BusinessException).ShouldBeThrownBy(() => settingsService.UpdateOrInsertSettings(settings));
        }


        [Test]
        [Category("IntegrationTests")]
        [Category("Settings")]
        public void Can_get_settings()
        {
            var settings = new Settings
            {
                SettingsAsJson = item
            };
            var settings1 = new Settings
            {
                SettingsAsJson = item
            };

          
            settings.ShouldNotBeNull();
            settings.Id.ShouldNotBeNull();
            settings1.ShouldNotBeNull();
            settings1.Id.ShouldNotBeNull();
           

            var settingsFromDb = Db.Find<Settings>(_ => true).FirstOrDefault();
                    

        }

        [TearDown]
        public void Dispose()
        {
            var settings = Db.Find<Settings>(x => true);
        }

    }
}
