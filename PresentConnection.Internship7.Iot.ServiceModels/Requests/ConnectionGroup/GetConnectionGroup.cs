using ServiceStack;

namespace PresentConnection.Internship7.Iot.ServiceModels
{
    [Route("/connectionGroups/{Id}", "GET", Summary = "Get Connection Group by Id")]
    public class GetConnectionGroup : IReturn<GetConnectionGroupResponse>
    {
        public string Id { get; set; }
    }
}
