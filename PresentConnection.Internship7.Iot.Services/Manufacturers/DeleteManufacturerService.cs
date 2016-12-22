using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.ServiceModels;
using ServiceStack;

namespace PresentConnection.Internship7.Iot.Services
{
    public class DeleteManufacturerService : ServiceBase
    {
        public IManufacturerService ManufacturerService { get; set; }

        public DeleteManufacturerResponse Any(DeleteManufacturer request)
        {
            var response = new DeleteManufacturerResponse
            {
                Result = ManufacturerService.DeleteManufacturer(request.Id)
            };
            
            return response;
        }
    }
}
