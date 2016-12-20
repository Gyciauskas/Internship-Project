using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.Domain;
using PresentConnection.Internship7.Iot.ServiceModels;
using ServiceStack;

namespace PresentConnection.Internship7.Iot.Services
{
    public class CreateCollaboratorService : Service
    {
        public ICollaboratorService CollaboratorService { get; set; }

        public CreateCollaboratorResponse Any(CreateCollaborator request)
        {
            var response = new CreateCollaboratorResponse();

            var collaborator = new Collaborator
            {
                Email = request.Email,
                UserId = request.UserId
            };

            CollaboratorService.CreateCollaborator(collaborator);

            return response;
        }
    }
}
