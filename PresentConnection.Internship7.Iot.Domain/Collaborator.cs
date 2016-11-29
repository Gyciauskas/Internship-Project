using CodeMash.Net;
using System.Collections.Generic;

namespace PresentConnection.Internship7.Iot.Domain
{
    [CollectionName(Statics.Collections.Collaborators)]
    public class Collaborator : EntityBase
    {
        public Collaborator()
        {
            Permissions = new List<string>();
        }

        public string UserId { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public List<string> Permissions { get; set; }

        
    }
}
