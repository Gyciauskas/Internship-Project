using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.BusinessImplementation;
using PresentConnection.Internship7.Iot.ServiceModels;
using ServiceStack;

namespace PresentConnection.Internship7.Iot.Services
{
    public class UpdateComponentService : Service
    {
        public IComponentService ComponentService { get; set; }

        public UpdateComponentResponse Any(UpdateComponent request)
        {
            var response = new UpdateComponentResponse();

            var component = ComponentService.GetComponent(request.Id);

            if (component != null)
            {
                component = component.PopulateWith(request);
                component.UniqueName = SeoService.GetSeName(request.UniqueName);
            }

            ComponentService.UpdateComponent(component);

            return response;
        }
    }
}
