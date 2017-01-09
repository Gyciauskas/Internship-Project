using System.Collections.Generic;
using System.Linq;
using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.Domain;
using PresentConnection.Internship7.Iot.ServiceModels;
using ServiceStack;

namespace PresentConnection.Internship7.Iot.Services
{
    public class CreateManufacturerService : ServiceBase
    {
        public IManufacturerService ManufacturerService { get; set; }
        public IImageService ImagesService { get; set; }

        public CreateManufacturerResponse Any(CreateManufacturer request)
        {
            var response = new CreateManufacturerResponse();
            var image = new DisplayImage
            {
                SeoFileName = request.SeoFileName,
                MimeType = request.MimeType
            };

            string imageId = ImagesService.AddImage(image, request.ImageBytes);

            var manufacturer = new Manufacturer
            {
                Name = request.Name,
                UniqueName = request.UniqueName,
                Images = { imageId }
            };

            ManufacturerService.CreateManufacturer(manufacturer);

            var cacheKey = CacheKeys.Manufacturers.List;
            Request.RemoveFromCache(Cache, cacheKey);
            

            response.Result = manufacturer;
            return response;
        }
    }
}