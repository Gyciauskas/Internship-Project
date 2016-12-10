using PresentConnection.Internship7.Iot.Domain;

namespace PresentConnection.Internship7.Iot.ServiceModels
{
    public class GetConnectionResponse
    {
        public GetConnectionResponse()
        {
            ConnectionById = new Connection();
        }
        public Connection ConnectionById { get; set; }
    }
}