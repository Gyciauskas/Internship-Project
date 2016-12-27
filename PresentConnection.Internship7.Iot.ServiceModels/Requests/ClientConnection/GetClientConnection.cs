using ServiceStack;

namespace PresentConnection.Internship7.Iot.ServiceModels
{
    [Route("/clientconnections/{Id}", "GET", Summary = "Get clientconnection by Id")]
    public class GetClientConnection : IReturn<GetManufacturerResponse>
    {
        public string Id { get; set; }
    }
}
