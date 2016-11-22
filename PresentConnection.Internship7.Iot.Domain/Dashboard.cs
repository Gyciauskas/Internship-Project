using CodeMash.Net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentConnection.Internship7.Iot.Domain
{
    [CollectionName("Dashboard")]
    public class Dashboard : EntityBase
    {
        public Dashboard()
        {
            Widgets = new List<Widget>();
            
        }


        public int Order { get; set; }
        public string UserId { get; set; } // e.g. Raspberry PI
        public List<Widget> Widgets { get; set; }
       


    }
}
