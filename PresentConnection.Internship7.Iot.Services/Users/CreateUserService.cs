using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.Domain;
using PresentConnection.Internship7.Iot.ServiceModels;
using ServiceStack;

namespace PresentConnection.Internship7.Iot.Services
{
    public class CreateUserService : Service
    {
        public IUserService UserService { get; set; }

        public CreateUserResponse Any(CreateUser request)
        {
            var response = new CreateUserResponse();

            var user = new User
            {
                FullName = request.FullName
            };

            UserService.CreateUser(user);

            return response;
        }
    }
}
