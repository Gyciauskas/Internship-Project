using FluentValidation;


namespace PresentConnection.Internship7.Iot.Domain
{
    public class DashboardValidator : AbstractValidator<Dashboard>
    {
        public DashboardValidator()
        {
            RuleFor(r => r.ClientId).NotEmpty();
        }
    }
}