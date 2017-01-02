using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.Domain;
using PresentConnection.Internship7.Iot.ServiceModels;
using ServiceStack;

namespace PresentConnection.Internship7.Iot.Services
{
    public class DeleteManufacturerService : ServiceBase
    {
        public IManufacturerService ManufacturerService { get; set; }
        public IImageService ImagesService { get; set; }

        public DeleteManufacturerResponse Any(DeleteManufacturer request)
        {
            Manufacturer manufacturer = ManufacturerService.GetManufacturer(request.Id);
            if (manufacturer.Images != null)
            {
                foreach (var imageId in manufacturer.Images)
                {
                    ImagesService.DeleteImage(imageId);
                }
            }

            var response = new DeleteManufacturerResponse
            {
                Result = ManufacturerService.DeleteManufacturer(request.Id)
            };
            
            return response;
        }
    }
}
