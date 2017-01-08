using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
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
        public ImageService()
        {
            // Checks if directory exists if not creates one.
            Directory.CreateDirectory(ImagesDir);
            for (int i = 0; i < SizeNames.Count; i++)
                Directory.CreateDirectory(Path.Combine(ImagesDir, SizeNames[i]));
        }


        readonly string ImagesDir = "~/images".MapHostAbsolutePath();
        readonly int MaxImageSize = 1000;
        readonly List<string> SizeNames = new List<string> {"medium", "small", "thumbnail"};
        

        private Image ConvertBytesToImage(byte[] image)
        {
            var ms = new MemoryStream(image) {Position = 0};
            return Image.FromStream(ms);
        }

        private Image ResizeImage(Image image, int newWidth, int newHeight)
        {
            var ratioX = (double)newWidth / image.Width;
            var ratioY = (double)newHeight / image.Height;
            var ratio = Math.Max(ratioX, ratioY);
            var width = (int)(image.Width * ratio);
            var height = (int)(image.Height * ratio);

            var newImage = new Bitmap(image, width, height);
            image = newImage;

            if (image.Width != newWidth || image.Height != newHeight)
            {
                var startX = (Math.Max(image.Width, newWidth) - Math.Min(image.Width, newWidth)) / 2;
                var startY = (Math.Max(image.Height, newHeight) - Math.Min(image.Height, newHeight)) / 2;
                image = Crop(image, newWidth, newHeight, startX, startY);
            }

            return image;
        }

        public static Image Crop(Image Image, int newWidth, int newHeight, int startX = 0, int startY = 0)
        {
            if (Image.Height < newHeight)
                newHeight = Image.Height;

            if (Image.Width < newWidth)
                newWidth = Image.Width;

            using (var bmp = new Bitmap(newWidth, newHeight, PixelFormat.Format24bppRgb))
            {
                bmp.SetResolution(72, 72);
                using (var g = Graphics.FromImage(bmp))
                {
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    g.DrawImage(Image, new Rectangle(0, 0, newWidth, newHeight), startX, startY, newWidth, newHeight, GraphicsUnit.Pixel);

                    var ms = new MemoryStream();
                    bmp.Save(ms, ImageFormat.Jpeg);
                    Image.Dispose();
                    var outimage = Image.FromStream(ms);
                    return outimage;
                }
            }
        }

        private Image CompresImage(Image image, long size)
        {
            var quality = 100;
            var msImage = new MemoryStream();
            
            while (size >= MaxImageSize)
            {
                if (quality < 0 || quality > 100)
                    throw new ArgumentOutOfRangeException("quality must be between 0 and 100.");

                EncoderParameter qualityParam = new EncoderParameter(Encoder.Quality, quality);
                ImageCodecInfo jpegCodec = GetEncoderInfo("image/jpeg");
                EncoderParameters encoderParams = new EncoderParameters(1);
                encoderParams.Param[0] = qualityParam;
                using (var ms = new MemoryStream()) // estimatedLength can be original fileLength
                {
                    image.Save(ms, jpegCodec, encoderParams);
                    size = (ms.Length / 1000);                    
                }
                quality = quality-10;

                if (size < MaxImageSize)
                {
                    image.Save(msImage, jpegCodec, encoderParams);
                }

            }

            return Image.FromStream(msImage);
        }

        private static ImageCodecInfo GetEncoderInfo(string mimeType)
        {
            // Get image codecs for all image formats 
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();

            // Find the correct image codec 
            for (int i = 0; i < codecs.Length; i++)
                if (codecs[i].MimeType == mimeType)
                    return codecs[i];

            return null;
        }

        private void InsertToDirectory(Image image, string nameWithExtension)
        {
           
            image.Save(ImagesDir.CombineWith(nameWithExtension));
        }

        private void DeleteFromDirectory(string id)
        {
            var image = GetImage(id);
            var imagePath = ImagesDir + "//" + image.UniqueImageName + image.MimeType;
            if (File.Exists(imagePath))
            {
                File.Delete(imagePath);
            }
            for (int i = 0; i < SizeNames.Count; i++)
            {
                imagePath = ImagesDir + "//" +  SizeNames[i] + "//" + image.UniqueImageName + ".jpg";
                if (File.Exists(imagePath))
                {
                    File.Delete(imagePath);
                }
            }
        }

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

        //----------ConnectImageToDatabaseObject------------------------------------------

        public string AddImage(DisplayImage displayImage, byte[] imageBytes)
        {
            var original = ConvertBytesToImage(imageBytes);
            if (imageBytes.Length / 1000 >= MaxImageSize)
            {
                original = CompresImage(original, imageBytes.Length / 1000);
                displayImage.MimeType = ".jpg";
            }
            // if mimetype not provided 
            if (displayImage.MimeType == null)
            {
                displayImage.MimeType = ".jpg";
            }
            var id = InsertImage(displayImage); // try insert image

            if (id != null)
            {
                InsertToDirectory(original, displayImage.UniqueImageName + displayImage.MimeType);

                // Inser resized images to hdd
                List<Image> Images = new List<Image>
                {
                    ResizeImage(original, 600, 300),
                    ResizeImage(original, 400, 200),
                    ResizeImage(original, 200, 50)
                };
                for (int i = 0; i < Images.Count; i++)
                {
                    Directory.CreateDirectory(ImagesDir);
                    InsertToDirectory(Images[i], SizeNames[i] + "//" + displayImage.UniqueImageName + ".jpg");
                }
            }
            return id;
        }

        public bool DeleteImage(string id)
        {

            DeleteFromDirectory(id);
            return DeleteImageFromDb(id);
        }      

    }
}
