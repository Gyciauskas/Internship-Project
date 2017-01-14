using CodeMash.Net;
using MongoDB.Bson;
using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.Domain;
using PresentConnection.Internship7.Iot.Utils;

namespace PresentConnection.Internship7.Iot.BusinessImplementation
{
    public class DisplayImageService : IDisplayImageService
    {
        public string CreateDisplayImage(DisplayImage displayImage)
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

        public void UpdateDisplayImage(DisplayImage displayImage)
        {
            var validator = new ImageValidator();
            var results = validator.Validate(displayImage);
            var validationSuceeded = results.IsValid;

            if (validationSuceeded)
            {
                Db.FindOneAndReplace(x => x.Id == displayImage.Id, displayImage);
            }
            else
            {
                throw new BusinessException("Cannot update device", results.Errors);
            }
        }

        public DisplayImage GetDisplayImage(string id)
        {
            return Db.FindOneById<DisplayImage>(id);
        }

        public bool DeleteDisplayImage(string id)
        {
            var deleteResult = Db.DeleteOne<DisplayImage>(x => x.Id == ObjectId.Parse(id));
            return deleteResult.DeletedCount == 1;
        }
    }
}
