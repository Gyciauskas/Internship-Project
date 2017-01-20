using System.Collections.Generic;
using System.IO;
using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.BusinessImplementation;
using PresentConnection.Internship7.Iot.Domain;
using PresentConnection.Internship7.Iot.ServiceModels;
using PresentConnection.Internship7.Iot.Utils;
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

            var sizes = new List<string> { "standard", "medium", "thumbnail"};
            var imageIds = new List<string>();

            // Original image
            var image = new DisplayImage
            {
                SeoFileName = Path.GetFileNameWithoutExtension(request.FileName),
                MimeType = Path.GetExtension(request.FileName),
            };
            imageIds.Add(ImagesService.AddImage(image, request.Image));

            // Different sizes
            foreach (var size in sizes)
            {
                image = new DisplayImage
                {
                    SeoFileName = Path.GetFileNameWithoutExtension(request.FileName),
                    MimeType = Path.GetExtension(request.FileName),
                    Size = size
                };
                imageIds.Add(ImagesService.AddImage(image, request.Image));
            }
            
            var manufacturer = new Manufacturer
            {
                Name = request.Name,
                UniqueName = SeoService.GetSeName(request.Name), 
                Images = imageIds
            };

            // If can't create manufacturer delete images form storage and db
            try
            {
                ManufacturerService.CreateManufacturer(manufacturer);
            }
            catch (BusinessException e)
            {
                foreach (var imageId in manufacturer.Images)
                {
                    ImagesService.DeleteImage(imageId);
                }
                throw;
            }

            var cacheKey = CacheKeys.Manufacturers.List;
            Request.RemoveFromCache(Cache, cacheKey);
            

            response.Result = manufacturer.Id.ToString();
            return response;
        }
    }
}