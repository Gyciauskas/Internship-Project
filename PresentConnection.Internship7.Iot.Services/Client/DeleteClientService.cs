using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.ServiceModels;
using ServiceStack;

namespace PresentConnection.Internship7.Iot.Services
{
    public class DeleteClientService : ServiceBase
    {
        public IClientService ClienteService { get; set; }

        public DeleteClientResponse Any(DeleteClient request)
        {
            var response = new DeleteClientResponse
            {
                Result = ClienteService.DeleteClient(request.Id)
            };

            return response;
        }
    }
}
