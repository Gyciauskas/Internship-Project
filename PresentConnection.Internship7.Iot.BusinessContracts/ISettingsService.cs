using PresentConnection.Internship7.Iot.Domain;
using System.Collections.Generic;
using System;

namespace PresentConnection.Internship7.Iot.BusinessContracts
{
    public interface ISetingsService
    {
       void UpdateOrInsertSettings(Settings settings);
       Settings GetSettings();
    }
}

