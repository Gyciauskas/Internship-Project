using System.Collections.Generic;
using FluentValidation;

namespace PresentConnection.Internship7.Iot.Domain
{
    public class RecordsPermissionValidator : AbstractValidator<List<IEntityWithSensitiveData>>
    {
        public RecordsPermissionValidator(string clientId)
        {
            RuleFor(x => x).CheckAccessPermissions(clientId);
        }
    }
}