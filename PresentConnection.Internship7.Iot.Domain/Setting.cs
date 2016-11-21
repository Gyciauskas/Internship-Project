using CodeMash.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentConnection.Internship7.Iot.Domain
{

    [CollectionName("Settings")]
    public class Setting : EntityBase
    {
        public string SettingsAsJson { get; set; }

    }
}
