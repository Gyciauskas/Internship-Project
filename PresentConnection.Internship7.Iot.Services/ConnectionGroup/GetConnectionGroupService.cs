using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.ServiceModels;
using ServiceStack;

namespace PresentConnection.Internship7.Iot.Services
{
    public class GetConnectionGroupService : ServiceBase
    {
        public IConnectionGroupService ConnectionGroupService { get; set; }

        public GetConnectionGroupResponse Any(GetConnectionGroup request)
        {
            var response = new GetConnectionGroupResponse
            {
                Result = ConnectionGroupService.GetConnectionGroup(request.Id)
            };
            return response;
        }
    }
}
