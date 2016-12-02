using FluentValidation;

namespace PresentConnection.Internship7.Iot.Domain
{
    public class RecordPermissionValidator : AbstractValidator<IEntityWithSensitiveData>
    {
        public RecordPermissionValidator(string clientId)
        {
            RuleFor(x => x).CheckAccessPermissions(clientId);
        }
    }
}