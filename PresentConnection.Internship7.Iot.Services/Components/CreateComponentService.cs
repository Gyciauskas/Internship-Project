using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.Domain;
using PresentConnection.Internship7.Iot.ServiceModels;
using ServiceStack;

namespace PresentConnection.Internship7.Iot.Services
{
    public class CreateComponentService : Service
    {
        public IComponentService ComponentService { get; set; }

        public CreateComponentResponse Any(CreateComponent request)
        {
            var response = new CreateComponentResponse();

            var component = new Component
            {
                ModelName = request.ModelName,
                UniqueName = request.UniqueName
            };
            ComponentService.CreateComponent(component);

            return response;
        }

    }
}
