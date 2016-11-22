using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentConnection.Internship7.Iot.Domain
{
    public class Invoice
    {
        public string FullName { get; set; }
        public DateTime CreatedOn { get; set; }
        public string FileName { get; set; }
        public string CustomerName { get; set; }
        public string VATCode { get; set; }
        public string ChamberOfComerce { get; set; }
        public double Amount { get; set; }
        public double VAT { get; set; }
        public string Country { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }
        public string ZipCode { get; set; }
        public string State { get; set; }
        public string City { get; set; }
    }
}
