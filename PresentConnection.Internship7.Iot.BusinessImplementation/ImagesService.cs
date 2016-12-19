using System;
using System.Drawing;
using System.IO;
using CodeMash.Net;
using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.Domain;
using ServiceStack;

namespace PresentConnection.Internship7.Iot.BusinessImplementation
{
    public class ImagesService : IImagesService
    {
        private readonly string _imagesDir = "~/images".MapHostAbsolutePath();
        public string InsertImage(byte[] image)
        {
            DisplayImage displayImage = new DisplayImage();

            var ms = new MemoryStream(image);
            var fileName = displayImage.UniqueImageName + ".jpg";
            ms.Position = 0;
            using (var img = Image.FromStream(ms))
            {
                img.Save(_imagesDir.CombineWith(fileName));
            }
            
            Db.InsertOne(displayImage);
            return displayImage.Id.ToString();
        }
    }
}
