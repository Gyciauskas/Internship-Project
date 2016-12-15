using ServiceStack;

namespace PresentConnection.Internship7.Iot.ServiceModels
{
    [Route("/collaborators", "POST", Summary = "Create collaborator")]
    public class CreateCollaborator : IReturn<CreateCollaboratorResponse>
    {
        // To pass validator  are required these properties
        public string Email { get; set; }
        public string UserId { get; set; }
    }
}
