using PresentConnection.Internship7.Iot.Domain;
using System.Collections.Generic;

namespace PresentConnection.Internship7.Iot.ServiceModels
{
    public class GetComponentsResponse
    {
        public GetComponentsResponse()
        {
            Components = new List<Component>();
        }
        public List<Component> Components { get; set; }
    }
}
