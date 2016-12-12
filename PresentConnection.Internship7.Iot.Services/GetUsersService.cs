using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.ServiceModels;
using ServiceStack;

namespace PresentConnection.Internship7.Iot.Services
{
    public class GetUsersService : Service
    {
        public IUserService UserService { get; set; }

        public GetUsersResponse Any(GetUsers request)
        {
            var response = new GetUsersResponse
            {
                Users = UserService.GetAllUsers(request.FullName)
            };

            return response;
        }
    }
}
