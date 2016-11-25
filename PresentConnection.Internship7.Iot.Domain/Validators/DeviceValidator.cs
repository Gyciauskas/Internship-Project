using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeMash.Net;
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

    public class DeviceUniqueNameValidator : AbstractValidator<Device>
    {
        public DeviceUniqueNameValidator()
        {
            RuleFor(x => x.UniqueName).Must(ValidUniqueName).WithMessage("Name is not unique");
        }

        private bool ValidUniqueName(string uniqueName)
        {
            var deviceFromDb = Db.FindOne<Device>(x => x.UniqueName == uniqueName);
            return deviceFromDb.UniqueName != uniqueName;
        }
    }
}
