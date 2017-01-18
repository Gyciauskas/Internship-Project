using CodeMash.Net;
using MongoDB.Bson;
namespace PresentConnection.Internship7.Iot.Domain
{

    [CollectionName(Statics.Collections.Settings)]
    public class Settings : EntityBase
    {
        public BsonDocument SettingsAsJson { get; set; }
    }
}
