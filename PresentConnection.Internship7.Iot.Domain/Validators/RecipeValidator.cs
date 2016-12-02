using FluentValidation;

namespace PresentConnection.Internship7.Iot.Domain.Validators
{
    public class RecipeValidator : AbstractValidator<Recipe>
    {
        public RecipeValidator()
        {
            RuleFor(r => r.Name).NotEmpty();
            RuleFor(r => r.UniqueName).NotEmpty();
            RuleFor(x => x).UniqueNameIsInCorrectFormatAndUnique();
            RuleFor(r => r.Images).MustContainAtLeastOneItem();
        }
    }
}
