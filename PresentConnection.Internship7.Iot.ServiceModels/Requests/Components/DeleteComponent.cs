using ServiceStack;

namespace PresentConnection.Internship7.Iot.ServiceModels
{
    [Route("/components/{Id}", "DELETE", Summary = "Delete component")]
    public class DeleteComponent : IReturn<DeleteComponentResponse>
    {
        public string Id { get; set; }
    }
}
