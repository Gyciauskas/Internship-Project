using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace PresentConnection.Internship7.Iot.Domain
{
    public class LookupValidator : AbstractValidator<Lookup>
    {
        public LookupValidator()
        {
            RuleFor(r => r.Name).NotEmpty();
            RuleFor(r => r.Type).NotEmpty();
        }
    }
}
