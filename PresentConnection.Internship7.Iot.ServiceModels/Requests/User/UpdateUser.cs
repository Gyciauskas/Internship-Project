using ServiceStack;

namespace PresentConnection.Internship7.Iot.ServiceModels
{
    [Route("/users/{Id}", "PUT", Summary = "Update user")]
    public class UpdateUser : IReturn<UpdateUserResponse>
    {
        public string Id { get; set; }
        public string FullName { get; set; }

    }
}
