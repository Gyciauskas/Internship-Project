using System.Collections.Generic;
using PresentConnection.Internship7.Iot.Domain;

namespace PresentConnection.Internship7.Iot.BusinessContracts
{
    public interface IConnectionService
    {
        void CreateConnection(Connection connection);
        void UpdateConnection(Connection connection);
        bool DeleteConnection(string id);
        List<Connection> GetAllConnections(string name = "");
        Connection GetConnection(string id);
    }
}
