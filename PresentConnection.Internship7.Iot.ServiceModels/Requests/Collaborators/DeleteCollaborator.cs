using ServiceStack;

namespace PresentConnection.Internship7.Iot.ServiceModels
{
    [Route("/collaborators", "DELETE", Summary = "Delete collaborator")]
    public class DeleteCollaborator : IReturn<DeleteCollaboratorResponse>
    {
        public string Id { get; set; }
    }
}
