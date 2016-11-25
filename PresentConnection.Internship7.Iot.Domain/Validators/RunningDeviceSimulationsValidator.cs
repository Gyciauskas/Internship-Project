using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace PresentConnection.Internship7.Iot.Domain
{
    public class RunningDeviceSimulationsValidator : AbstractValidator<RunningDeviceSimulations>
    {
        public RunningDeviceSimulationsValidator()
        {
            RuleFor(r => r.DeviceId).NotEmpty();
            RuleFor(r => r.SimulationType).NotEmpty();
        }
    }
}
