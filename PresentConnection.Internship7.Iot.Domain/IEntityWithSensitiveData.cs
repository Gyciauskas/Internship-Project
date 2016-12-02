using MongoDB.Bson;

namespace PresentConnection.Internship7.Iot.Domain
{
    public interface  IEntityWithSensitiveData
    {
        ObjectId Id { get; set; }
        string ClientId { get; set; }
    }
}