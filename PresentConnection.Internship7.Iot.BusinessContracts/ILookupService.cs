using System.Collections.Generic;
using PresentConnection.Internship7.Iot.Domain;

namespace PresentConnection.Internship7.Iot.BusinessContracts
{
    public interface ILookupService
    {
        void CreateLookup(Lookup lookup);
        void UpdateLookup(Lookup lookup);
        bool DeleteLookup(string id);
        List<Lookup> GetAllLookups(string type = "");
        Lookup GetLookup(string id);
    }
}
