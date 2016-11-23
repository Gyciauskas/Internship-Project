using FluentValidation;

namespace PresentConnection.Internship7.Iot.Domain
{
    public class ClientConnectionValidator : AbstractValidator<ClientConnection>
    {
        public ClientConnectionValidator()
        {
            RuleFor(r => r.ClientId).NotEmpty();
            RuleFor(r => r.ConnectionId).NotEmpty();
        }

    }
}
