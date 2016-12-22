using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.ServiceModels;
using ServiceStack;

namespace PresentConnection.Internship7.Iot.Services
{
    public class UpdateClientConnectionService : Service
    {
        public IClientConnectionService ClientConnectionService { get; set; }

        public UpdateClientConnectionResponse Any(UpdateClientConnection request)
        {
            var response = new UpdateClientConnectionResponse();


            var clientconnection = ClientConnectionService.GetClientConnection(request.Id,request.ClientId).PopulateWith(request);
            ClientConnectionService.UpdateClientConnection(clientconnection,request.ClientId);

            return response;
        }
    }
}
