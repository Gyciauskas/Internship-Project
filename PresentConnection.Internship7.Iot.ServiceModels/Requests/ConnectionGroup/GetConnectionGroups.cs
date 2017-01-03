using ServiceStack;

namespace PresentConnection.Internship7.Iot.ServiceModels
{
    [Route("/connectionGroups", "GET", Summary = "Gets all Connection Groups")]
    public class GetConnectionGroups : IReturn<GetConnectionGroupsResponse>
    {
        
    }
}