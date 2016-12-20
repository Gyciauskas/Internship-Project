using ServiceStack;

namespace PresentConnection.Internship7.Iot.ServiceModels
{
    [Route("/collaborators", "PUT", Summary = "Update collaborator")]
    public class UpdateCollaborator : IReturn<UpdateCollaboratorResponse>
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string UserId { get; set; }
    }
}
