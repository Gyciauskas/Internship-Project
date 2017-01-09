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
            var manufacturer = ManufacturerService.GetManufacturer(request.Id);
            var manufacturerName = string.Empty;

            if (manufacturer != null)
            {
                manufacturerName = manufacturer.Name;
            }

//            if (manufacturer?.Images != null)
//            {
                foreach (var imageId in manufacturer.Images)
                {
                    ImagesService.DeleteImage(imageId);
                }
//            }

            var response = new DeleteManufacturerResponse
            {
                Result = ManufacturerService.DeleteManufacturer(request.Id)
            };

            if (response.Result)
            {
                var cacheKeyForListWithName = CacheKeys.Manufacturers.ListWithProvidedName.Fmt(manufacturerName);
                var cacheKeyForList = CacheKeys.Manufacturers.List;
                var cacheKeyForItem = CacheKeys.Manufacturers.Item.Fmt(request.Id);

                Request.RemoveFromCache(Cache, cacheKeyForList);
                Request.RemoveFromCache(Cache, cacheKeyForListWithName);
                Request.RemoveFromCache(Cache, cacheKeyForItem);
            }
            
            return response;
        }
    }
}
