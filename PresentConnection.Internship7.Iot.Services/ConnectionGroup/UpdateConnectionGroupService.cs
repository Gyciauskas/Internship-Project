using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.ServiceModels;
using ServiceStack;

namespace PresentConnection.Internship7.Iot.Services
{
    public class UpdateConnectionGroupervice : ServiceBase
    {
        public IConnectionGroupService ConnectionGroupService { get; set; }

        public UpdateConnectionGroupResponse Any(UpdateConnectionGroup request)
        {
            var response = new UpdateConnectionGroupResponse();


            var connectionGroup = ConnectionGroupService.GetConnectionGroup(request.Id).PopulateWith(request);
            ConnectionGroupService.UpdateConnectionGroup(connectionGroup);

            return response;
        }
    }
}
