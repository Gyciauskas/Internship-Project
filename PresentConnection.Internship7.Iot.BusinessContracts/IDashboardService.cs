using PresentConnection.Internship7.Iot.Domain;

namespace PresentConnection.Internship7.Iot.BusinessContracts
{
    public interface IDashboardService
    {
        void UpdateDashboard(Dashboard dashboard);
        Dashboard GetDashboard(string clientId);
    }
}
