using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PresentConnection.Internship7.Iot.Domain;

namespace PresentConnection.Internship7.Iot.BusinessContracts
{
    public interface IConnectionService
    {
        string CreateConnection(Connection connection);
        void UpdateConnection(Connection connection);
        bool DeleteConnection(string id);
        List<Connection> GetAllConnections();
        Connection GetConnection(string id);
    }
}
