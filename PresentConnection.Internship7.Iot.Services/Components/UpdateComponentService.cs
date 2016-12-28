using PresentConnection.Internship7.Iot.BusinessContracts;
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


            var component = ComponentService.GetComponent(request.Id).PopulateWith(request);
            ComponentService.UpdateComponent(component);

            return response;
        }
    }
}
