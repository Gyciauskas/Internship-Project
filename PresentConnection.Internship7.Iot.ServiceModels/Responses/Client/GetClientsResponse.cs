using PresentConnection.Internship7.Iot.Domain;
using System.Collections.Generic;


namespace PresentConnection.Internship7.Iot.ServiceModels
{
    public class GetClientsResponse
    {
        public GetClientsResponse()
        {
            clients = new List<Client>();
        }
        public List<Client> clients { get; set; }
    }
}
