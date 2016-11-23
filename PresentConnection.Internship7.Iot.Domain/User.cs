using CodeMash.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentConnection.Internship7.Iot.Domain
{
    [CollectionName("User")]
    public class User : EntityBase
    {
        public  User()
        {
            Rules = new List<string>();
            Permissions = new List<string>();

        }

        public string FullName { get; set; }
        public List<string> Rules { get; set; }
        public List<string> Permissions { get; set; }
        public string Email { get; set; }

        }
}
