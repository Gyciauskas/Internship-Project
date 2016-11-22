using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PresentConnection.Internship7.Iot.Domain;

namespace PresentConnection.Internship7.Iot.BusinessContracts
{
    public interface ILookupService
    {
        string CreateLookup(Lookup lookup);
        void UpdateLookup(Lookup lookup);
        bool DeleteLookup(string id);
        List<Lookup> GetAllLookups(string type = "");
        Lookup GetLookup(string id);
    }
}
