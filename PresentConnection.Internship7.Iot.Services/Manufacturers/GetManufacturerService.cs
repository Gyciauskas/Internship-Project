using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.ServiceModels;
using ServiceStack;

namespace PresentConnection.Internship7.Iot.Services
{
    public class GetManufacturerService : ServiceBase
    {
        public IManufacturerService ManufacturerService { get; set; }

        public GetManufacturerResponse Any(GetManufacturer request)
        {
            var response = new GetManufacturerResponse
            {
                Result = ManufacturerService.GetManufacturer(request.Id)
            };
            return response;
        }
    }
}
