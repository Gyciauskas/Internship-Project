using System.Collections.Generic;
using System.IO;
using System.Linq;
using MongoDB.Bson;
using NUnit.Framework;
using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.BusinessImplementation;
using PresentConnection.Internship7.Iot.Domain;
using PresentConnection.Internship7.Iot.Utils;
using ServiceStack;

namespace PresentConnection.Internship7.Iot.Tests
{
    [TestFixture]
    public class ImagesTests
    {
        private IImageService imageService;
        private readonly string imagesDir = "~/images".MapHostAbsolutePath();
        private static readonly string testImagePath = Directory.EnumerateFiles("~/testImages".MapHostAbsolutePath()).Last();
        private readonly byte[] imageBytes = File.ReadAllBytes(testImagePath);
        private DisplayImage goodDisplayImage;

        [SetUp]
        public void SetUp()
        {
            imageService = new ImageService();
            goodDisplayImage = new DisplayImage()
            {
                SeoFileName = Path.GetFileNameWithoutExtension(testImagePath),
                MimeType = Path.GetExtension(testImagePath)
            };
        }

        [Test]
        [Category("IntegrationTests")]
        [Category("Images")]
        public void Can_insert_image_to_database()
        {
            imageService.AddImage(goodDisplayImage, imageBytes);

            goodDisplayImage.ShouldNotBeNull();
            goodDisplayImage.Id.ShouldNotBeNull();
        }

        [Test]
        [Category("IntegrationTests")]
        [Category("Images")]
        public void Can_store_image_in_server_when_inserting_to_database()
        {
            imageService.AddImage(goodDisplayImage, imageBytes);
            goodDisplayImage.ShouldNotBeNull();
            goodDisplayImage.Id.ShouldNotBeNull();

            string goodImagePath = imagesDir + "\\" + goodDisplayImage.UniqueImageName + goodDisplayImage.MimeType;
            File.Exists(goodImagePath).ShouldBeTrue();
        }

        [Test]
        [Category("IntegrationTests")]
        [Category("Images")]
        public void Cannot_insert_image_to_database_when_seofilename_is_not_provided()
        {
            goodDisplayImage.SeoFileName = null;

            var exception = typeof(BusinessException).ShouldBeThrownBy(() => imageService.AddImage(goodDisplayImage, imageBytes));
            exception.Message.ShouldEqual("Cannot insert image to database");
        }

        [Test]
        [Category("IntegrationTests")]
        [Category("Images")]
        public void Can_get_image_by_id()
        {
            imageService.AddImage(goodDisplayImage, imageBytes);

            goodDisplayImage.ShouldNotBeNull();
            goodDisplayImage.Id.ShouldNotBeNull();

            var imageFromDb = imageService.GetImage(goodDisplayImage.Id.ToString());
            imageFromDb.ShouldNotBeNull();
            imageFromDb.Id.ShouldNotBeNull();
            imageFromDb.SeoFileName.ShouldEqual(goodDisplayImage.SeoFileName);
        }

        [Test]
        [Category("IntegrationTests")]
        [Category("Images")]
        public void Can_get_all_images()
        {
            imageService.AddImage(goodDisplayImage, imageBytes);
            var image2 = new DisplayImage
            {
                SeoFileName = "tempName",
                MimeType = Path.GetExtension(testImagePath)
            };

            imageService.AddImage(image2, imageBytes);
            image2.ShouldNotBeNull();
            image2.Id.ShouldNotBeNull();

            var images = imageService.GetAllImages();
            images.ShouldBe<List<DisplayImage>>();
            (images.Count > 0).ShouldBeTrue();
        }

        [Test]
        [Category("IntegrationTests")]
        [Category("Images")]
        public void Can_delete_image_from_database()
        {
            imageService.AddImage(goodDisplayImage, imageBytes);

            goodDisplayImage.ShouldNotBeNull();
            goodDisplayImage.Id.ShouldNotBeNull();

            imageService.DeleteImage(goodDisplayImage.Id.ToString());

            var imageFromDb = imageService.GetImage(goodDisplayImage.Id.ToString());

            imageFromDb.ShouldNotBeNull();
            imageFromDb.Id.ShouldEqual(ObjectId.Empty);
        }

        [Test]
        [Category("IntegrationTests")]
        [Category("Images")]
        public void Can_delete_image_from_server_when_deleting_from_database()
        {
            imageService.AddImage(goodDisplayImage, imageBytes);
            goodDisplayImage.ShouldNotBeNull();
            goodDisplayImage.Id.ShouldNotBeNull();

            string goodImagePath = imagesDir + "\\" + goodDisplayImage.UniqueImageName + goodDisplayImage.MimeType;
            File.Exists(goodImagePath).ShouldBeTrue();
            imageService.DeleteImage(goodDisplayImage.Id.ToString());
            File.Exists(goodImagePath).ShouldBeFalse();
        }

        [TearDown]
        public void Dispose()
        {
            var images = imageService.GetAllImages();
            foreach (var image in images)
            {
                imageService.DeleteImage(image.Id.ToString());
            }
        }
    }
}
