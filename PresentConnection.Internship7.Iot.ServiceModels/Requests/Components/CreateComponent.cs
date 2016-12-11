using ServiceStack;

namespace PresentConnection.Internship7.Iot.ServiceModels
{
    [Route("/components", "POST", Summary = "Create component")]
    public class CreateComponent : IReturn<CreateManufacturerResponse>
    {
        public string ModelName { get; set; }
        public string UniqueName { get; set; }
    }
}
