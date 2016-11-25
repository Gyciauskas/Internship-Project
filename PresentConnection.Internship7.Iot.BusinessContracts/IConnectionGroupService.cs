using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PresentConnection.Internship7.Iot.Domain;

namespace PresentConnection.Internship7.Iot.BusinessContracts
{
    public interface IConnectionGroupService
    {
        string CreateConnectionGroup(ConnectionGroup connectionGroup);
        void UpdateConnectionGroup(ConnectionGroup connectionGroup);
        bool DeleteConnectionGroup(string id);
        List<ConnectionGroup> GetAllConnectionGroups();
        ConnectionGroup GetConnectionGroup(string id);
    }
}
