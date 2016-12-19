using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.ServiceModels;
using ServiceStack;

namespace PresentConnection.Internship7.Iot.Services
{
    public class DeleteClientDeviceService : Service
    {
        public IClientDeviceService ClientDeviceService { get; set; }

        public DeleteClientDeviceResponse Any(DeleteClientDevice request)
        {
            var response = new DeleteClientDeviceResponse
            {
                IsDeleted = ClientDeviceService.DeleteClientDevice(request.Id, request.ClientId)
            };
            
            return response;
        }
    }
}
