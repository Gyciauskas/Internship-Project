using System;
using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.ServiceModels;
using ServiceStack;

namespace PresentConnection.Internship7.Iot.Services
{
    public class GetManufacturerService : ServiceBase
    {
        public IManufacturerService ManufacturerService { get; set; }
        
        public object Any(GetManufacturer request)
        {
            var expireInTimespan = new TimeSpan(1, 0, 0);

            return Request.ToOptimizedResultUsingCache(
                
                Cache, 
                CacheKeys.Manufacturers.Item.Fmt(request.Id),
                expireInTimespan,
                 
                () => {
                    var response = new GetManufacturerResponse
                    {
                        Result = ManufacturerService.GetManufacturer(request.Id)
                    };
                    return response;
                });
        }
    }
}
