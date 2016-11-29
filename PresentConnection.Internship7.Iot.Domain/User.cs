using CodeMash.Net;
using System;
using System.Collections.Generic;

namespace PresentConnection.Internship7.Iot.Domain
{
    [CollectionName("Users")]
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
