using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.ServiceModels;
using ServiceStack;

namespace PresentConnection.Internship7.Iot.Services
{
    public class GetCollaboratorsService : ServiceBase
    {
        public ICollaboratorService CollaboratorService { get; set; }

        public GetCollaboratorsResponse Any(GetCollaborators request)
        {
            var response = new GetCollaboratorsResponse
            {
                Result = CollaboratorService.GetAllCollaborators(request.Name)
            };

            return response;
        }
    }
}
