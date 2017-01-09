using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.ServiceModels;
using ServiceStack;

namespace PresentConnection.Internship7.Iot.Services
{
    public class DeleteCollaboratorService : ServiceBase
    {
        public ICollaboratorService CollaboratorService { get; set; }

        public DeleteCollaboratorResponse Any(DeleteCollaborator request)
        {
            var collaborator = CollaboratorService.GetCollaborator(request.Id);
            var collaboratorName = string.Empty;

            if (collaborator != null)
            {
                collaboratorName = collaborator.Name;
            }

            var response = new DeleteCollaboratorResponse
            {
                Result = CollaboratorService.DeleteCollaborator(request.Id)
            };

            if (response.Result)
            {
                var cacheKeyForListWithName = CacheKeys.Collaborators.ListWithProvidedName.Fmt(collaboratorName);
                var cacheKeyForList = CacheKeys.Collaborators.List;
                var cacheKeyForItem = CacheKeys.Collaborators.Item.Fmt(request.Id);

                Request.RemoveFromCache(Cache, cacheKeyForList);
                Request.RemoveFromCache(Cache, cacheKeyForListWithName);
                Request.RemoveFromCache(Cache, cacheKeyForItem);
            }

            return response;
        }
    }
}
