using ServiceStack;

namespace PresentConnection.Internship7.Iot.ServiceModels
{
    [Route("/clientconnections/{Id}", "DELETE", Summary = "Delete clientconnection")]
    public class DeleteClientConnection : IReturn<DeleteClientConnectionResponse>
    {
        public string Id { get; set; }
    }
}