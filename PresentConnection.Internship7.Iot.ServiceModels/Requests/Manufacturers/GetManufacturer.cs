using ServiceStack;

namespace PresentConnection.Internship7.Iot.ServiceModels
{
    [Route("/manufacturers/{Id}", "GET", Summary = "Get manufacturer by Id")]
    public class GetManufacturer : IReturn<GetManufacturerResponse>
    {
        public string Id { get; set; }
    }
}
