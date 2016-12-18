using ServiceStack;

namespace PresentConnection.Internship7.Iot.ServiceModels
{
    [Route("/collaborators/{Id}", "GET", Summary = "Get collaborator")]
    public class GetCollaborator : IReturn<GetCollaboratorResponse>
    {
        public string Id { get; set; }
    }
}
