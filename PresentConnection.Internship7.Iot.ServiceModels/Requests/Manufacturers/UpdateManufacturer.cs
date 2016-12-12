using ServiceStack;

namespace PresentConnection.Internship7.Iot.ServiceModels
{
    [Route("/manufacturers/{Id}", "PUT", Summary = "Update manufacturer")]
    public class UpdateManufacturer : IReturn<UpdateManufacturerResponse>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string UniqueName { get; set; }
    }
}
