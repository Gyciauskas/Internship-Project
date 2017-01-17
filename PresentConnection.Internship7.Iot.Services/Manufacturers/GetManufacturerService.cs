using System;
using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.ServiceModels;
using ServiceStack;

namespace PresentConnection.Internship7.Iot.Services
{
    public class GetManufacturerService : ServiceBase
    {
        public IManufacturerService ManufacturerService { get; set; }
        public IImageService ImageService { get; set; }
        
        public object Any(GetManufacturer request)
        {
            var expireInTimespan = new TimeSpan(1, 0, 0);

            Request.ToOptimizedResultUsingCache(
                
                Cache, 
                CacheKeys.Manufacturers.Item.Fmt(request.Id),
                expireInTimespan,
                 
                () =>
                {

                    var manufacturer = ManufacturerService.GetManufacturer(request.Id);

                    var response = new GetManufacturerResponse();

                    if (manufacturer != null)
                    {


                        response.Result = ManufacturerDto.With(ImageService)
                                                .Map(manufacturer)
                                                .ApplyImages(manufacturer.Images)
                                                .Build();
                    }
                    
                    return response;
                });

                return Request.GetCacheClient().Get<GetManufacturerResponse>(CacheKeys.Manufacturers.Item.Fmt(request.Id));
        }
    }
}
