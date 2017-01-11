using CodeMash.Net.DataContracts;
using ServiceStack;

namespace PresentConnection.Internship7.Iot.ServiceModels
{
    [Route("/manufacturers", "GET", Summary = "Gets all manufacturers!")]
    public class GetManufacturers : ListRequestBase, IReturn<GetManufacturersResponse>
    {
        public string Name { get; set; }
    }
}