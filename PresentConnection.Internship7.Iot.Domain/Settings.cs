using CodeMash.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
namespace PresentConnection.Internship7.Iot.Domain
{

    [CollectionName("Settings")]
    public class Settings : EntityBase
    {
        public BsonDocument SettingsAsJson { get; set; }
    }
}
