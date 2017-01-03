using System;
using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.ServiceModels;
using ServiceStack;

namespace PresentConnection.Internship7.Iot.Services
{
    public class GetManufacturersService : ServiceBase
    {
        public IManufacturerService ManufacturerService { get; set; }

        public object Any(GetManufacturers request)
        {
            var expireInTimespan = new TimeSpan(1, 0, 0);

            var cacheKey = CacheKeys.Manufacturers.List;

            if (!string.IsNullOrEmpty(request.Name))
            {
                cacheKey = CacheKeys.Manufacturers.ListWithProvidedName.Fmt(request.Name);
            }

            return Request.ToOptimizedResultUsingCache(

                Cache,
                cacheKey,
                expireInTimespan,

                () => {
                    var response = new GetManufacturersResponse
                    {
                        Result = ManufacturerService.GetAllManufacturers(request.Name)
                    };

                    return response;
                });

           
        }
    }
}