using ServiceStack;

namespace PresentConnection.Internship7.Iot.ServiceModels
{
    [Route("/connection-groups", "GET", Summary = "Gets all Connection Groups")]
    public class GetConnectionGroups : IReturn<GetConnectionGroupsResponse>
    {
        
    }
}