using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.ServiceModels;
using ServiceStack;
using ServiceStack.Auth;

namespace PresentConnection.Internship7.Iot.Services
{
   public class DeleteClientConnectionService : Service
    {
        public IClientConnectionService ClienteConnectionService { get; set; }
        public IAuthSession AuthSession { get; set; }
        public DeleteClientConnectionResponse Any(DeleteClientConnection request)
        {
            var response = new DeleteClientConnectionResponse
            {
                IsDeleted = ClienteConnectionService.DeleteClientConnection(request.Id, AuthSession.UserAuthId)
            };

            return response;
        }
    }
}
