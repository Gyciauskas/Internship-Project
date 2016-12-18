using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.ServiceModels;
using ServiceStack;

namespace PresentConnection.Internship7.Iot.Services
{
    public class GetUserService : Service
    {
        public IUserService UserService { get; set; }

        public GetUserResponse Any(GetUser request)
        {
            var response = new GetUserResponse
            {
                Result = UserService.GetUser(request.Id)
            };

            return response;
        }
    }
}
