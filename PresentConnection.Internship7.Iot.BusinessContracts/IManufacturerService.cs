using PresentConnection.Internship7.Iot.Domain;
using System.Collections.Generic;

namespace PresentConnection.Internship7.Iot.BusinessContracts
{
    public interface IManufacturerService
    {
        string CreateManufacturer(Manufacturer manufacturer);
        void UdpdateManufacturer(Manufacturer manufacturer);
        bool DeleteManufacturer(string id);
        List<Manufacturer> GetAllManufacturers(string name = "");
        Manufacturer GetManufacturer(string id);
    }
}
