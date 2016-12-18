using ServiceStack;

namespace PresentConnection.Internship7.Iot.ServiceModels
{
    [Route("/manufacturers/{Id}", "DELETE", Summary = "Delete manufacturer")]
    public class DeleteManufacturer : IReturn<DeleteManufacturerResponse>
    {
        public string Id { get; set; }
    }
}
