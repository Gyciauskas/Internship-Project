using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace PresentConnection.Internship7.Iot.Domain
{
    public class ConnectionValidator : AbstractValidator<Connection>
    {
        public ConnectionValidator()
        {
            RuleFor(r => r.UniqueName).NotEmpty();
            RuleFor(r => r.Name).NotEmpty();
            RuleFor(r => r.Images.Count).GreaterThan(0);
            RuleFor(r => r.Url).NotEmpty();
            RuleFor(r => r.Description).NotEmpty();
        }
    }
}
