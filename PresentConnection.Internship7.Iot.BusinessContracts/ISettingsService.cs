using PresentConnection.Internship7.Iot.Domain;
using System.Collections.Generic;

namespace PresentConnection.Internship7.Iot.BusinessContracts
{
    public interface ISetingsService
    {
        string CreateSetting(Setting setting);
        void UpdateSetting(Setting setting);
        bool DeleteSetting(string id);
        Setting GetSetting(string id);
        List<Setting> GetAllSettings(string name="");
       
    }
}

