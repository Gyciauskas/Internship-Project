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
            RuleFor(x => x).Must(ValidUniqueName).WithMessage("Name is not unique");
        }

        private bool ValidUniqueName(Device device)
        {
            var deviceFromDb = Db.FindOne<Device>(x => x.UniqueName == device.UniqueName);
            if ( (deviceFromDb.UniqueName == device.UniqueName) && (deviceFromDb.Id != device.Id))
            {
                return false;
            }
            else
            {
                if (Db.Count<Device>(x => x.UniqueName == device.UniqueName) >= 2)
                {
                    return false;
                }
                return true;
            }
        }
    }
}
