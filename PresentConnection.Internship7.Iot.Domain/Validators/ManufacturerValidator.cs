using FluentValidation;

namespace PresentConnection.Internship7.Iot.Domain
{
    public class ManufacturerValidator : AbstractValidator<Manufacturer>
    {
        public ManufacturerValidator()
        {
            RuleFor(r => r.Name).NotEmpty();
            RuleFor(r => r.UniqueName).NotEmpty();
        }
        
    }
}
