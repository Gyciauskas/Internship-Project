using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.ServiceModels;
using ServiceStack;

namespace PresentConnection.Internship7.Iot.Services
{
    public class UpdateCollaboratorService : ServiceBase
    {
        public ICollaboratorService CollaboratorService { get; set; }

        public UpdateCollaboratorResponse Any(UpdateCollaborator request)
        {
            var response = new UpdateCollaboratorResponse();

            var collaborator = CollaboratorService.GetCollaborator(request.Id);
            var collaboratorName = string.Empty;

            if (collaborator != null)
            {
                collaboratorName = collaborator.Name;
                collaborator = collaborator.PopulateWith(request);
            }

            CollaboratorService.UpdateCollaborator(collaborator);

            var cacheKeyForListWithName = CacheKeys.Collaborators.ListWithProvidedName.Fmt(collaboratorName);
            var cacheKeyForList = CacheKeys.Collaborators.List;
            var cacheKeyForItem = CacheKeys.Collaborators.Item.Fmt(request.Id);

            Request.RemoveFromCache(Cache, cacheKeyForList);
            Request.RemoveFromCache(Cache, cacheKeyForListWithName);
            Request.RemoveFromCache(Cache, cacheKeyForItem);

            response.Result = collaborator;
            return response;
        }
    }
}
