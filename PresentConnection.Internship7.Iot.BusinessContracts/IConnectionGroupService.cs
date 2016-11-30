using System.Collections.Generic;
using PresentConnection.Internship7.Iot.Domain;

namespace PresentConnection.Internship7.Iot.BusinessContracts
{
    public interface IConnectionGroupService
    {
        void CreateConnectionGroup(ConnectionGroup connectionGroup);
        void UpdateConnectionGroup(ConnectionGroup connectionGroup);
        bool DeleteConnectionGroup(string id);
        List<ConnectionGroup> GetAllConnectionGroups();
        ConnectionGroup GetConnectionGroup(string id);
    }
}
