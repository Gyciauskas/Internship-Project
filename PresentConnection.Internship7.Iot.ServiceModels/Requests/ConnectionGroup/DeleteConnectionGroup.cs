using ServiceStack;

namespace PresentConnection.Internship7.Iot.ServiceModels
{
    [Route("/connection-groups/{Id}", "DELETE", Summary = "Delete Connection Group")]
    public class DeleteConnectionGroup : IReturn<DeleteConnectionGroupResponse>
    {
        public string Id { get; set; }
    }
}
