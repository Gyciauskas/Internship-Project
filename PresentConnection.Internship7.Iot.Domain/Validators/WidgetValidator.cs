using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentConnection.Internship7.Iot.Domain.Validators
{
    public class WidgetValidator : AbstractValidator<Widget>
    {
        public WidgetValidator()
        {
            RuleFor(r => r.Type).NotEmpty();
            RuleFor(r => r.Query).NotEmpty();
            RuleFor(r => r.Configuration).NotEmpty();

        }
    }
}
