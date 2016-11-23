using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace PresentConnection.Internship7.Iot.Domain
{
    public class ClientValidator : AbstractValidator<Client>
    {
        public ClientValidator()
        {
            RuleFor(r => r.UserId).NotEmpty();
            RuleFor(r => r.Subscriptions.Count).GreaterThan(0);
        }
    }
}
