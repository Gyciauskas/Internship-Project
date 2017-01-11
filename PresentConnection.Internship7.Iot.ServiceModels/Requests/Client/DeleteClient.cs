using ServiceStack;
namespace PresentConnection.Internship7.Iot.ServiceModels
{
    [Route("/clients/{Id}", "DELETE", Summary = "Delete client")]
    public class DeleteClient : IReturn<DeleteClientResponse>
    {
        public string Id { get; set; }
    }
}
