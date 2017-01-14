using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.Domain;
using ServiceStack;

namespace PresentConnection.Internship7.Iot.BusinessImplementation
{
    public class ImageService : IImageService
    {
        readonly string ImagesDir = "~/images".MapHostAbsolutePath();        
        readonly List<string> SizeNames = new List<string> {"medium", "small", "thumbnail"};
        readonly int MaxImageSize = 1000;

        public IDisplayImageService DisplayImageService { get; set; }
        public IFileService fileService { get; set; }

        /// <summary>
        /// Create image for service.
        /// </summary>
        /// <param name="displayImage">Image object in database.</param>
        /// <param name="image"></param>
        /// <returns>Image id.</returns>
        public string AddImage(DisplayImage displayImage, byte[] image)
        {
            var id = DisplayImageService.CreateDisplayImage(displayImage);


            if (image.Length / 1000 >= MaxImageSize)
            {
                image = CompressImage(image, image.Length / 1000);
                displayImage.MimeType = ".png";
            }

            if (!fileService.ExistFolder(ImagesDir))
            {
               fileService.CreateFolder(ImagesDir); 
            }
            fileService.UploadFile(ImagesDir, displayImage.UniqueImageName + displayImage.MimeType, image);          

            // Insert resized images to hdd
            List<byte[]> Images = new List<byte[]>
            {
                ResizeImage(image, Convert.ToInt16(ConfigurationManager.AppSettings["ImageWidth-medium"]), Convert.ToInt16(ConfigurationManager.AppSettings["ImageHeight-medium"])),
                ResizeImage(image, Convert.ToInt16(ConfigurationManager.AppSettings["ImageWidth-small"]), Convert.ToInt16(ConfigurationManager.AppSettings["ImageHeight-small"])),
                ResizeImage(image, Convert.ToInt16(ConfigurationManager.AppSettings["ImageWidth-thumb"]), Convert.ToInt16(ConfigurationManager.AppSettings["ImageHeight-thumb"]))
            };
            for (int i = 0; i < Images.Count; i++)
            {
                var newDir = fileService.ConcatDirs(ImagesDir, SizeNames[i]);
                if (!fileService.ExistFolder(newDir))
                {
                    fileService.CreateFolder(newDir);
                }
                fileService.UploadFile(newDir, displayImage.UniqueImageName + ".jpg", Images[i]);    
            }

            return id;
        }

        /// <summary>
        /// Deletes image from db and storage.
        /// </summary>
        /// <param name="id">Image id.</param>
        /// <returns>True if deleted from db.</returns>
        public bool DeleteImage(string id)
        {
            var image = DisplayImageService.GetDisplayImage(id);
            var filename = image.UniqueImageName + image.MimeType;

            if (DisplayImageService.DeleteDisplayImage(id))
            {
                if (fileService.ExistFolder(ImagesDir))
                {
                    fileService.DeleteFile(ImagesDir, filename);
                }

                foreach (var sizeName in SizeNames)
                {
                    var dir = fileService.ConcatDirs(ImagesDir, sizeName);
                    if (fileService.ExistFolder(dir))
                    {
                        fileService.DeleteFile(dir, filename);
                    }
                }
                return true;
            }
            
            return false;
        }

        /// <summary>
        /// Resizes Image and crops image to specified height and width.
        /// </summary>
        /// <param name="image">Instance of image.</param>
        /// <param name="newWidth">The width to resize to.</param>
        /// <param name="newHeight">The height to resize to.</param>
        /// <returns>New resized image instance.</returns>
        private byte[] ResizeImage(byte[] imagebytes, int newWidth, int newHeight)
        {
            var msImage = new MemoryStream(imagebytes);
            var image = Image.FromStream(msImage);
            if (newWidth != image.Width || newHeight != image.Height)
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
            }

            var ms = new MemoryStream();
            image.Save(ms, ImageFormat.Png);
            ms.Position = 0;
            return ms.ToArray();
        }

        /// <summary>
        /// Crops image.
        /// </summary>
        /// <param name="Image">Instance of image.</param>
        /// <param name="newWidth">The width to crop to.</param>
        /// <param name="newHeight">The height to crop to.</param>
        /// <param name="startX">From there to start cropping in x cordinate.</param>
        /// <param name="startY">From there to start cropping in y cordinate.</param>
        /// <returns>Croped image.</returns>
        private static Image Crop(Image Image, int newWidth, int newHeight, int startX = 0, int startY = 0)
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
                    bmp.Save(ms, ImageFormat.Png);
                    Image.Dispose();
                    var outimage = Image.FromStream(ms);
                    return outimage;
                }
            }
        }

        /// <summary>
        /// Compresses image.
        /// </summary>
        /// <param name="image">Instance of image.</param>
        /// <param name="size">Original image size</param>
        /// <returns>Compressed image.</returns>
        private byte[] CompressImage(byte[] imageBytes, long size)
        {
            var msl = new MemoryStream(imageBytes) { Position = 0 };
            var image = Image.FromStream(msl);

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
                quality = quality - 10;

                if (size < MaxImageSize)
                {
                    image.Save(msImage, jpegCodec, encoderParams);
                }

            }

            return msImage.ToArray();
        }

        /// <summary>
        /// Returns the image codec with the given mime type 
        /// </summary>
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

        public string GetImagePath(string id)
        {
            try
            {
                var image = DisplayImageService.GetDisplayImage(id);
                return fileService.ConcatDirs(ImagesDir, image.UniqueImageName + image.MimeType);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        
        //
        //        public string GetMediumImagePath(string id)
        //        {
        //            var image = GetImage(id);
        //            return ImagesDir + "//" + image?.UniqueImageName + ".jpg";
        //        }
        //
        //        public string GetSmallImagePath(string id)
        //        {
        //            var image = GetImage(id);
        //            return ImagesDir + "//" + image?.UniqueImageName + ".jpg";
        //        }
        //
        //        public string GetThumbImagePath(string id)
        //        {
        //            var image = GetImage(id);
        //            return ImagesDir + "//thumbnail//" + image?.UniqueImageName + ".jpg";
        //        }

    }
}
