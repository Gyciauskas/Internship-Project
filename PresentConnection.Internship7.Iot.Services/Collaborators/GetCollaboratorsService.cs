using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.ServiceModels;
using ServiceStack;
using System;

namespace PresentConnection.Internship7.Iot.Services
{
    public class GetCollaboratorsService : ServiceBase
    {
        public ICollaboratorService CollaboratorService { get; set; }

        public object Any(GetCollaborators request)
        {
            var expireInTimespan = new TimeSpan(1, 0, 0);

            var cacheKey = CacheKeys.Collaborators.List;

            if (!string.IsNullOrEmpty(request.Name))
            {
                cacheKey = CacheKeys.Collaborators.ListWithProvidedName.Fmt(request.Name);
            }

            return Request.ToOptimizedResultUsingCache(

                Cache,
                cacheKey,
                expireInTimespan,

                () => {
                    var response = new GetCollaboratorsResponse
                    {
                        Result = CollaboratorService.GetAllCollaborators(request.Name)
                    };

                    return response;
                });
        }
    }
}
