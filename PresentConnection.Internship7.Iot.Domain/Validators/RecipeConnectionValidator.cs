using FluentValidation;

namespace PresentConnection.Internship7.Iot.Domain
{
    public class RecipeConnectionValidator : AbstractValidator<RecipeConnection>
    {
        public RecipeConnectionValidator()
        {
            RuleFor(r => r.Name).NotEmpty();
            RuleFor(r => r.UniqueName).NotEmpty();
            RuleFor(x => x).UniqueNameIsInCorrectFormatAndUnique();
        }
    }
}
