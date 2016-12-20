using ServiceStack;

namespace PresentConnection.Internship7.Iot.ServiceModels
{
    [Route("/users/{Id}", "GET", Summary = "Get user by Id")]
    public class GetUser : IReturn<GetUserResponse>
    {
        public string Id { get; set; }
    }
}
