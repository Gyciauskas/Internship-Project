using FluentValidation;

namespace PresentConnection.Internship7.Iot.Domain
{
    public class DeviceValidator : AbstractValidator<Device>
    {
        public DeviceValidator()
        {
            RuleFor(r => r.ModelName).NotEmpty();
            RuleFor(r => r.UniqueName).NotEmpty();
            RuleFor(r => r.Images).MustContainAtLeastOneItem();
            RuleFor(x => x).UniqueNameIsInCorrectFormatAndUnique();
        }
    }
}
