using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentConnection.Internship7.Iot.Domain.Validators
{
    public class SettingValidator:AbstractValidator<Setting>
    {
        public SettingValidator()
            {
            RuleFor(r => r.SettingsAsJson).NotEmpty();

        }


    }
}
