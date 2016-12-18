using ServiceStack;

namespace PresentConnection.Internship7.Iot.ServiceModels
{
    [Route("/users/{Id}", "DELETE", Summary = "Delete user")]
    public class DeleteUser : IReturn<DeleteUserResponse>
    {
        public string Id { get; set; }
    }
}
