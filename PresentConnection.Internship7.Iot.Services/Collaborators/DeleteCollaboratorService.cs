using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.ServiceModels;
using ServiceStack;

namespace PresentConnection.Internship7.Iot.Services
{
    public class DeleteCollaboratorService : Service
    {
        public ICollaboratorService CollaboratorService { get; set; }

        public DeleteCollaboratorResponse Any(DeleteCollaborator request)
        {
            var response = new DeleteCollaboratorResponse
            {
                IsDeleted = CollaboratorService.DeleteCollaborator(request.Id)
            };

            return response;
        }
    }
}
