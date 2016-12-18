using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.ServiceModels;
using ServiceStack;

namespace PresentConnection.Internship7.Iot.Services
{
    public class UpdateUserService : Service
    {
        public IUserService UserService { get; set; }

        public UpdateUserResponse Any(UpdateUser request)
        {
            var response = new UpdateUserResponse();

            // Get and replace
            var user = UserService.GetUser(request.Id).PopulateWith(request);

            UserService.UpdateUser(user);

            return response;
        }
    }
}
