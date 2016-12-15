using ServiceStack;

namespace PresentConnection.Internship7.Iot.ServiceModels
{
    [Route("/connections/{Id}", "DELETE", Summary = "Deletes connection by Id !")]
    public class DeleteConnection : IReturn<DeleteConnectionResponse>
    {
        public string Id { get; set; }
    }
}