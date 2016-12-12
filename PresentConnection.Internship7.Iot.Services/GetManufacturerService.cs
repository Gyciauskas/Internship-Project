using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.ServiceModels;
using ServiceStack;

namespace PresentConnection.Internship7.Iot.Services
{
    public class GetManufacturerService : Service
    {
        public IManufacturerService ManufacturerService { get; set; }

        public GetManufacturerResponse Any(GetManufacturer request)
        {
            var response = new GetManufacturerResponse
            {
                Manufacturer = ManufacturerService.GetManufacturer(request.Id)
            };
            return response;
        }
    }
}
