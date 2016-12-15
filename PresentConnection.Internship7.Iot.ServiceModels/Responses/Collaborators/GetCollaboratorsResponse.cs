﻿using PresentConnection.Internship7.Iot.Domain;
using System.Collections.Generic;

namespace PresentConnection.Internship7.Iot.ServiceModels
{
    public class GetCollaboratorsResponse
    {
        public GetCollaboratorsResponse()
        {
            Collaborators = new List<Collaborator>();
        }
        public List<Collaborator> Collaborators { get; set; }
    }
}
