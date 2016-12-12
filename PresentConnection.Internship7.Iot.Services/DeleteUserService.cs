using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.ServiceModels;
using ServiceStack;

namespace PresentConnection.Internship7.Iot.Services
{
    public class DeleteUserService : Service
    {
        public IUserService UserService { get; set; }

        public DeleteUserResponse Any(DeleteUser request)
        {
            var response = new DeleteUserResponse
            {
                IsDeleted = UserService.DeleteUser(request.Id)
            };

            return response;
        }
    }
}
