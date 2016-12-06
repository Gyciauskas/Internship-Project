using PresentConnection.Internship7.Iot.Domain;
using System.Collections.Generic;

namespace PresentConnection.Internship7.Iot.ServiceModels
{
    public class GetManufacturersResponse
    {
        public GetManufacturersResponse()
        {
            Manufacturers = new List<Manufacturer>();
        }
        public List<Manufacturer> Manufacturers { get; set; }
        
    }
}