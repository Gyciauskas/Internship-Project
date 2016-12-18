using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.ServiceModels;
using ServiceStack;

namespace PresentConnection.Internship7.Iot.Services
{
    public class GetCollaboratorService : Service
    {
        public ICollaboratorService CollaboratorService { get; set; }

        public GetCollaboratorResponse Any(GetCollaborator request)
        {
            var response = new GetCollaboratorResponse
            {
                Result = CollaboratorService.GetCollaborator(request.Id)
            };

            return response;
        }
    }
}
