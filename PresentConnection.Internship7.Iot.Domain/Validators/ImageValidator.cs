using FluentValidation;

namespace PresentConnection.Internship7.Iot.Domain
{
    public class ImageValidator : AbstractValidator<DisplayImage>
    {
        public ImageValidator()
        {
            RuleFor(r => r.SeoFileName).NotEmpty();
            RuleFor(r => r.MimeType).NotEmpty();
        }
    }
}
