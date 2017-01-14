using System.Collections.Generic;
using System.IO;
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
                SeoFileName = Path.GetFileNameWithoutExtension(request.FileName),
                MimeType = Path.GetExtension(request.FileName)
            };

            string imageId = ImagesService.AddImage(image, request.Image);

            var manufacturer = new Manufacturer
            {
                Name = request.Name,
                UniqueName = request.Name, // TODO - make unique name from Name
                Images = { imageId }
            };

            ManufacturerService.CreateManufacturer(manufacturer);

            var cacheKey = CacheKeys.Manufacturers.List;
            Request.RemoveFromCache(Cache, cacheKey);
            

            response.Result = manufacturer.Id.ToString();
            return response;
        }
    }
}