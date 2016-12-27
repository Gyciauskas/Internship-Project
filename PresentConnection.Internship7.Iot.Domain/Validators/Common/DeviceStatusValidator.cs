using System.Collections.Generic;
using FluentValidation;

namespace PresentConnection.Internship7.Iot.Domain
{
    public class DeviceStatusValidator : AbstractValidator<List<DeviceStatus>>
    {
        public DeviceStatusValidator(DeviceStatus deviceStatus)
        {
            RuleFor(x => x).AddDeviceStatusPermissions(deviceStatus);
        }
    }
}
