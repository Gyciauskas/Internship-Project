using ServiceStack;

namespace PresentConnection.Internship7.Iot.ServiceModels
{
    [Route("/components/{Id}", "PUT", Summary = "Update component")]
    public class UpdateComponent : IReturn<UpdateComponentResponse>
    {
        public string Id { get; set; }
        public string ModelName { get; set; }
        public string UniqueName { get; set; }
    }
}
