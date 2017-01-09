using System.Collections.Generic;
using CodeMash.Net;
using MongoDB.Bson;
using MongoDB.Driver;
using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.Domain;
using PresentConnection.Internship7.Iot.Utils;

namespace PresentConnection.Internship7.Iot.BusinessImplementation
{
    public partial class ImageService
    {
        public string InsertImage(DisplayImage displayImage)
        {

            var validator = new ImageValidator();
            var results = validator.Validate(displayImage);
            var validationSucceeded = results.IsValid;

            if (validationSucceeded)
            {
                Db.InsertOne(displayImage);
            }
            else
            {
                throw new BusinessException("Cannot insert image to database", results.Errors);
            }
            return displayImage.Id.ToString();
        }

        public List<DisplayImage> GetAllImages()
        {
            return Db.Find(Builders<DisplayImage>.Filter.Empty);
        }

        public DisplayImage GetImage(string id)
        {
            return Db.FindOneById<DisplayImage>(id);
        }

        public bool DeleteImageFromDb(string id)
        {
            var deleteResult = Db.DeleteOne<DisplayImage>(x => x.Id == ObjectId.Parse(id));
            return deleteResult.DeletedCount == 1;
        }
    }
}
