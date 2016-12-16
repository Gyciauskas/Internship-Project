using ServiceStack;

namespace PresentConnection.Internship7.Iot.ServiceModels
{
    [Route("/users", "POST", Summary = "Create user")]
    public class CreateUser : IReturn<CreateUserResponse>
    {
        public string FullName { get; set; }
        public string Email { get; set; }
    }
}