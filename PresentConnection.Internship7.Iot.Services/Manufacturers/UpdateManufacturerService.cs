using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.ServiceModels;
using ServiceStack;

namespace PresentConnection.Internship7.Iot.Services
{
    public class UpdateManufacturerService : Service
    {
        public IManufacturerService ManufacturerService { get; set; }

        public UpdateManufacturerResponse Any(UpdateManufacturer request)
        {
            var response = new UpdateManufacturerResponse();


            var manufacturer = ManufacturerService.GetManufacturer(request.Id).PopulateWith(request);
            ManufacturerService.UdpdateManufacturer(manufacturer);

            return response;
        }
    }
}
