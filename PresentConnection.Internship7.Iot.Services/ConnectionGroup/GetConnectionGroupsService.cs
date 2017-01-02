using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.ServiceModels;
using ServiceStack;

namespace PresentConnection.Internship7.Iot.Services
{
    public class GetConnectionGroupsService : ServiceBase
    {
        public IConnectionGroupService ConnectionGroupService { get; set; }

        public GetConnectionGroupsResponse Any(GetConnectionGroups request)
        {
            var response = new GetConnectionGroupsResponse
            {
                Result = ConnectionGroupService.GetAllConnectionGroups()
            };

            return response;
        }
    }
}