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

            List<string> imageIds = new List<string>();
            foreach (var image in request.Images.Where(image => image.Value.Length > 0))
            {
                imageIds.Add(ImagesService.InsertImage(image.Key, image.Value));
            }

            var manufacturer = new Manufacturer
            {
                Name = request.Name,
                UniqueName = request.UniqueName,
                Images =  imageIds
            };

            ManufacturerService.CreateManufacturer(manufacturer);

            return response;
        }
    }
}