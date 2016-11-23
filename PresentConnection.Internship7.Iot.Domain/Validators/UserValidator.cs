using FluentValidation;

namespace PresentConnection.Internship7.Iot.Domain.Validators
{
    public class UserValidator:AbstractValidator<User>
    {
      public UserValidator()
        {
            RuleFor(r => r.FullName).NotEmpty();
            RuleFor(r => r.Rules).NotEmpty();
            RuleFor(r => r.Permissions).NotEmpty();
            RuleFor(r => r.Email).NotEmpty();
        }

    }
}
