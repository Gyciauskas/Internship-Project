using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.ServiceModels;
using ServiceStack;

namespace PresentConnection.Internship7.Iot.Services
{
    public class UpdateCollaboratorService : Service
    {
        public ICollaboratorService CollaboratorService { get; set; }

        public UpdateCollaboratorResponse Any(UpdateCollaborator request)
        {
            var response = new UpdateCollaboratorResponse();

            // Get and replace
            var collaborator = CollaboratorService.GetCollaborator(request.Id).PopulateWith(request);

            CollaboratorService.UpdateCollaborator(collaborator);

            return response;
        }
    }
}
