using ServiceStack;

namespace PresentConnection.Internship7.Iot.ServiceModels
{
    [Route("/users", "GET", Summary = "Gets all user!")]
    public class GetUsers : IReturn<GetUsersResponse>
    {
        public string FullName { get; set; }
    }
}