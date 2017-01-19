using ServiceStack;

namespace PresentConnection.Internship7.Iot.ServiceModels
{
    [Route("/components", "POST", Summary = "Create component")]
    public class CreateComponent : IReturn<CreateComponentResponse>
    {
        public string ModelName { get; set; }
    }
}
