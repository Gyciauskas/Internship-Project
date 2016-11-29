using MongoDB.Bson;

namespace PresentConnection.Internship7.Iot.Domain
{
    public interface  IEntityWithUniqueName
    {
        ObjectId Id { get; set; }
        string UniqueName { get; set; }
    }
}
