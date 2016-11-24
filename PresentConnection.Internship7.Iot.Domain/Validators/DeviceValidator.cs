using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace PresentConnection.Internship7.Iot.Domain
{
    public class DeviceValidator : AbstractValidator<Device>
    {
        public DeviceValidator()
        {
            RuleFor(r => r.ModelName).NotEmpty();
            RuleFor(r => r.UniqueName).NotEmpty();
            RuleFor(r => r.Images.Count).GreaterThan(0);
        }
    }
}
