using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.ServiceModels;
using ServiceStack;
using ServiceStack.Auth;

namespace PresentConnection.Internship7.Iot.Services
{
   public class DeleteClientConnectionService : ServiceBase
    {
        public IClientConnectionService ClienteConnectionService { get; set; }
        public DeleteClientConnectionResponse Any(DeleteClientConnection request)
        {
            var response = new DeleteClientConnectionResponse
            {
                Result = ClienteConnectionService.DeleteClientConnection(request.Id, UserSession.UserAuthId)
            };

            return response;
        }
    }
}
