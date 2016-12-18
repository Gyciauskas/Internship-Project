using ServiceStack;

namespace PresentConnection.Internship7.Iot.ServiceModels
{
    [Route("/collaborators", "GET", Summary = "Get all collaborators")]
    public class GetCollaborators : IReturn<GetCollaboratorsResponse>
    {
        public string Name { get; set; }
    }
}
