using ServiceStack;

namespace PresentConnection.Internship7.Iot.ServiceModels
{
    [Route("/connectionGroups/{Id}", "PUT", Summary = "Update Connection Group")]
    public class UpdateConnectionGroup : IReturn<UpdateConnectionGroupResponse>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string UniqueName { get; set; }
    }
}
