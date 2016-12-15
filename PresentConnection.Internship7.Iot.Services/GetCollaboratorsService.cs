using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.ServiceModels;
using ServiceStack;

namespace PresentConnection.Internship7.Iot.Services
{
    public class GetCollaboratorsService : Service
    {
        public ICollaboratorService CollaboratorService { get; set; }

        public GetCollaboratorsResponse Any(GetCollaborators request)
        {
            var response = new GetCollaboratorsResponse
            {
                Collaborators = CollaboratorService.GetAllCollaborators(request.Name)
            };

            return response;
        }
    }
}
