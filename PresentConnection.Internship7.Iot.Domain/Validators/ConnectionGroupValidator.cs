using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace PresentConnection.Internship7.Iot.Domain
{
    public class ConnectionGroupValidator : AbstractValidator<ConnectionGroup>
    {
        public ConnectionGroupValidator()
        {
            RuleFor(r => r.UniqueName).NotEmpty();
            RuleFor(x => x).UniqueNameIsInCorrectFormatAndUnique();
            RuleFor(r => r.Name).NotEmpty();
            RuleFor(r => r.Images).MustContainAtLeastOneItem();
        }
    }
}
