using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.ServiceModels;
using ServiceStack;
using ServiceStack.Auth;

namespace PresentConnection.Internship7.Iot.Services
{
    public class GetClientConnectionService : Service
    {
        public IClientConnectionService ClientConnectionService { get; set; }
        public IAuthSession AuthSession { get; set; }
        public GetClientConnectionResponse Any(GetClientConnection request)
        {
            var response = new GetClientConnectionResponse
            {
                clientconnection = ClientConnectionService.GetClientConnection(request.Id, AuthSession.UserAuthId)
            };
            return response;
        }
    }

}
