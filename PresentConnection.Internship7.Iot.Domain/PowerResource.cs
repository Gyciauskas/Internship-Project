using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentConnection.Internship7.Iot.Domain
{
    public class PowerResource
    {
        public enum PowerResourceType { Battery, Voltage }
        public int PercentageValue { get; set; }
    }
}
