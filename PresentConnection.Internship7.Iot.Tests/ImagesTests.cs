using System.Configuration;
using System.IO;
using System.Linq;
using CodeMash.Net;
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
        private static readonly string testImagePath = Directory.EnumerateFiles("~/testImages".MapHostAbsolutePath()).Last();
        private readonly byte[] imageBytes = File.ReadAllBytes(testImagePath);

        private IImageService imageService;
        private readonly string imagesDir = ConfigurationManager.AppSettings["ImagesPath"].MapHostAbsolutePath();
        private DisplayImage goodDisplayImage;

        [SetUp]
        public void SetUp()
        {
            imageService = new ImageService
            {
                FileService = new FileService()
            };

            goodDisplayImage = new DisplayImage()
            {
                SeoFileName = Path.GetFileNameWithoutExtension(testImagePath),
                MimeType = Path.GetExtension(testImagePath),
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
        public void Can_store_image()
        {
            imageService.AddImage(goodDisplayImage, imageBytes);
            goodDisplayImage.ShouldNotBeNull();
            goodDisplayImage.Id.ShouldNotBeNull();

            string goodImagePath = Path.Combine(imagesDir, goodDisplayImage.UniqueImageName + goodDisplayImage.MimeType);
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
        public void Cannot_insert_image_to_database_when_mimetype_is_not_provided()
        {
            goodDisplayImage.MimeType = null;

            var exception = typeof(BusinessException).ShouldBeThrownBy(() => imageService.AddImage(goodDisplayImage, imageBytes));
            exception.Message.ShouldEqual("Cannot insert image to database");
        }

        [Test]
        [Category("IntegrationTests")]
        [Category("Images")]
        public void Can_delete_image()
        {
            imageService.AddImage(goodDisplayImage, imageBytes);
            goodDisplayImage.ShouldNotBeNull();
            goodDisplayImage.Id.ShouldNotBeNull();

            string goodImagePath = Path.Combine(imagesDir, goodDisplayImage.UniqueImageName + goodDisplayImage.MimeType);

            File.Exists(goodImagePath).ShouldBeTrue();
            imageService.DeleteImage(goodDisplayImage.Id.ToString());
            File.Exists(goodImagePath).ShouldBeFalse();
        }

        [TearDown]
        public void Dispose()
        {
            var images = Db.Find<DisplayImage>(_ => true);

            foreach (var image in images)
            {
                imageService.DeleteImage(image.Id.ToString());
            }                                      
        }
    }
}
