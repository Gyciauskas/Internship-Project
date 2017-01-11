using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Web;

namespace PresentConnection.Internship7.Iot.WebApp.Models
{
    public class CreateManufacturerViewModel
    {
        public string Name { get; set; }
        public HttpPostedFileBase Image { get; set; }
    }
}