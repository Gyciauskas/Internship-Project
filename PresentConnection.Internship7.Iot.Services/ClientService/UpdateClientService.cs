using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.ServiceModels;
using ServiceStack;


namespace PresentConnection.Internship7.Iot.Services
{
        public class UpdateClientService : Service
        {
            public IClientService ClientService { get; set; }

            public UpdateClientResponse Any(UpdateClient request)
            {
                var response = new UpdateClientResponse();


                var client= ClientService.GetClient(request.Name).PopulateWith(request);
                ClientService.UpdateClient(client);

                return response;
            }
        }
    
}
