using FluentValidation;

namespace PresentConnection.Internship7.Iot.Domain
{
    public class CollaboratorValidator : AbstractValidator<Collaborator>
    {
        public CollaboratorValidator()
        {
            RuleFor(r => r.Email).NotEmpty();
            RuleFor(r => r.UserId).NotEmpty();
        }
        
    }
}
