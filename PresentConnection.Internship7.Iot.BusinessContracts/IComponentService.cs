using PresentConnection.Internship7.Iot.Domain;
using System.Collections.Generic;

namespace PresentConnection.Internship7.Iot.BusinessContracts
{
    public interface IComponentService
    {
        void CreateComponent(Component component);
        void UpdateComponent(Component component);
        bool DeleteComponent(string id);
        List<Component> GetAllComponents(string name = "");
        Component GetComponent(string id);


    }
}
