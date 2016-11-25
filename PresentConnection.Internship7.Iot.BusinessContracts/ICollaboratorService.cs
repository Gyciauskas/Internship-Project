using PresentConnection.Internship7.Iot.Domain;
using System.Collections.Generic;

namespace PresentConnection.Internship7.Iot.BusinessContracts
{
    public interface ICollaboratorService
    {
        string CreateCollaborator(Collaborator collaborator);
        void UpdateCollaborator(Collaborator collaborator);
        bool DeleteCollaborator(string id);
        List<Collaborator> GetAllCollaborator(string name = "");
        Collaborator GetCollaborator(string id);
    }
}
