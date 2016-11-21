using PresentConnection.Internship7.Iot.Domain;
using System.Collections.Generic;

namespace PresentConnection.Internship7.Iot.BusinessContracts
{
    public interface IDashboardService
    {
        string CreateDashboard(Dashboard dashboard);
        void UdpdateDashboard(Dashboard dashboard);
        bool DeleteDashboard(string id);
        List<Dashboard> GetAllDashboards(string userId = "");
        Dashboard GetDashboard(string id);
    }
}
