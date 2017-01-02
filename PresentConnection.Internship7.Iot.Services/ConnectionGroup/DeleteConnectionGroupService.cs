using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.ServiceModels;
using ServiceStack;

namespace PresentConnection.Internship7.Iot.Services
{
    public class DeleteConnectionGroupService : ServiceBase
    {
        public IConnectionGroupService ConnectionGroupService { get; set; }

        public DeleteConnectionGroupResponse Any(DeleteConnectionGroup request)
        {
            var response = new DeleteConnectionGroupResponse
            {
                Result = ConnectionGroupService.DeleteConnectionGroup(request.Id)
            };
            
            return response;
        }
    }
}
