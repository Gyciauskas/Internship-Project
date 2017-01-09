using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.Domain;
using PresentConnection.Internship7.Iot.ServiceModels;
using ServiceStack;

namespace PresentConnection.Internship7.Iot.Services
{
    public class CreateCollaboratorService : ServiceBase
    {
        public ICollaboratorService CollaboratorService { get; set; }

        public CreateCollaboratorResponse Any(CreateCollaborator request)
        {
            var response = new CreateCollaboratorResponse();

            var collaborator = new Collaborator
            {
                Email = request.Email,
                UserId = request.UserId,
                Name = request.Name
            };

            CollaboratorService.CreateCollaborator(collaborator);

            var cacheKey = CacheKeys.Collaborators.List;
            Request.RemoveFromCache(Cache, cacheKey);

            response.Result = collaborator;
            return response;
        }
    }
}
