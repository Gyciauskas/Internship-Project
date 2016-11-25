using CodeMash.Net;
using System.Collections.Generic;

namespace PresentConnection.Internship7.Iot.Domain
{
    [CollectionName("Collaborator")]
    public class Collaborator : EntityBase
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public List<string> PermissionsSet { get; set; }

        public Collaborator()
        {
            PermissionsSet = new List<string>();
        }
    }
}
