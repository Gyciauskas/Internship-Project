using CodeMash.Net.DataContracts;
using ServiceStack;

namespace PresentConnection.Internship7.Iot.ServiceModels
{
    [Route("/devices", "GET", Summary = "Get all devices")]
    public class GetDevices : ListRequestBase, IReturn<GetDevicesResponse>
    {
        public string Name { get; set; }
    }
}
