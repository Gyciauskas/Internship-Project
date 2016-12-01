using PresentConnection.Internship7.Iot.Domain;

namespace PresentConnection.Internship7.Iot.BusinessContracts
{
    public interface ISetingsService
    {
       void UpdateOrInsertSettings(Settings settings);
       Settings GetSettings();
    }
}

