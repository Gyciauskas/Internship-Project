using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.ServiceModels;
using ServiceStack;
using System;

namespace PresentConnection.Internship7.Iot.Services
{
    public class GetCollaboratorService : ServiceBase
    {
        public ICollaboratorService CollaboratorService { get; set; }

        public object Any(GetCollaborator request)
        {
            var expireInTimespan = new TimeSpan(1, 0, 0);

            return Request.ToOptimizedResultUsingCache(

                Cache,
                CacheKeys.Collaborators.Item.Fmt(request.Id),
                expireInTimespan,

                () => {
                    var response = new GetCollaboratorResponse
                    {
                        Result = CollaboratorService.GetCollaborator(request.Id)
                    };
                    return response;
                });
        }
    }
}
