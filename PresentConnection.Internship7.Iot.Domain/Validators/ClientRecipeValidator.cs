using FluentValidation;

namespace PresentConnection.Internship7.Iot.Domain
{
    public class ClientRecipeValidator : AbstractValidator<ClientRecipe>
    {
        public ClientRecipeValidator(string clientId)
        {
            RuleFor(r => r.RecipeId).NotEmpty();
            RuleFor(r => r.ClientId).NotEmpty();
            RuleFor(r => r).CheckAccessPermissions(clientId);
        }
    }
}
