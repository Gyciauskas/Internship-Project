using System.Collections.Generic;
using System.Drawing;
using System.IO;
using CodeMash.Net;
using MongoDB.Bson;
using MongoDB.Driver;
using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.Domain;
using PresentConnection.Internship7.Iot.Utils;
using ServiceStack;

namespace PresentConnection.Internship7.Iot.BusinessImplementation
{
    public class ImageService : IImageService
    {
        private readonly string _imagesDir = "~/images".MapHostAbsolutePath();

        private void InsertImageToDir(string fileName, byte[] image)
        {
            Directory.CreateDirectory(_imagesDir);
            var ms = new MemoryStream(image) { Position = 0 };
            using (var img = Image.FromStream(ms))
            {
                img.Save(_imagesDir.CombineWith(fileName));
            }
        }

        private void DeleteImageFromDir(string id)
        {
            var image = GetImage(id);
            string imagePath = _imagesDir + "\\" + image.UniqueImageName + image.MimeType;
            if (File.Exists(imagePath))
            {
                File.Delete(imagePath);
            }
        }

        public string InsertImage(DisplayImage displayImage, byte[] image)
        {

            var validator = new ImageValidator();
            var results = validator.Validate(displayImage);
            var validationSucceeded = results.IsValid;

            if (validationSucceeded)
            {
                var fileName = displayImage.UniqueImageName + displayImage.MimeType;
                InsertImageToDir(fileName, image);
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

        public bool DeleteImage(string id)
        {
            DeleteImageFromDir(id);
            var deleteResult = Db.DeleteOne<DisplayImage>(x => x.Id == ObjectId.Parse(id));            
            return deleteResult.DeletedCount == 1;
        }

    }
}
