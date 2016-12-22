using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.ServiceModels;
using ServiceStack;

namespace PresentConnection.Internship7.Iot.Services
{
    public class GetManufacturersService : ServiceBase
    {
        public IManufacturerService ManufacturerService { get; set; }

        public GetManufacturersResponse Any(GetManufacturers request)
        {
            var response = new GetManufacturersResponse
            {
                Result = ManufacturerService.GetAllManufacturers(request.Name)
            };

            return response;
        }
    }
}