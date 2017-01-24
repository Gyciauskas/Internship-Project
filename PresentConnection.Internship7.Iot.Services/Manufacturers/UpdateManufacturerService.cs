using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.ServiceModels;
using ServiceStack;

namespace PresentConnection.Internship7.Iot.Services
{
    public class UpdateManufacturerService : ServiceBase
    {
        public IManufacturerService ManufacturerService { get; set; }

        public UpdateManufacturerResponse Any(UpdateManufacturer request)
        {
            var response = new UpdateManufacturerResponse();

            var manufacturer = ManufacturerService.GetManufacturer(request.Id);
            var manufacturerName = string.Empty;
            
            if (manufacturer != null)
            {
                manufacturerName = manufacturer.Name;
                manufacturer = manufacturer.PopulateWith(request);
            }
            
            ManufacturerService.UdpdateManufacturer(manufacturer);

            var cacheKeyForListWithName = CacheKeys.Manufacturers.ListWithProvidedName.Fmt(manufacturerName);
            var cacheKeyForList = CacheKeys.Manufacturers.List;
            var cacheKeyForItem = CacheKeys.Manufacturers.Item.Fmt(request.Id);
            
            Request.RemoveFromCache(Cache, cacheKeyForList);
            Request.RemoveFromCache(Cache, cacheKeyForListWithName);
            Request.RemoveFromCache(Cache, cacheKeyForItem);

            response.Result = true;
            return response;
        }
    }
}
