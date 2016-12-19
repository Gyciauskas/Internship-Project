using System.Collections.Generic;
using System.Linq;
using FluentValidation.Validators;

namespace PresentConnection.Internship7.Iot.Domain
{
    public class DeviceStatusConstraintsValidator<T> : PropertyValidator
    {
        private readonly DeviceStatus deviceStatus;

        public DeviceStatusConstraintsValidator(DeviceStatus deviceStatus)
            : base("Can't add device status {deviceStatus}")
        {
            this.deviceStatus = deviceStatus;
        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            var deviceStatusList = context.PropertyValue as IList<DeviceStatus>;
            bool notAllowedFirstElement = deviceStatusList.Count == 0 && deviceStatus != DeviceStatus.Registered;          
            if (deviceStatusList.Any())
            {
                bool notAlreadyOnState = deviceStatusList.Last() != deviceStatus;
                bool addOnUnregistered = deviceStatusList.Last() == DeviceStatus.Unregistered &&
                                         (deviceStatus == DeviceStatus.Connected ||
                                          deviceStatus == DeviceStatus.Disconnected);
                return notAlreadyOnState && !addOnUnregistered;
            }
            return !notAllowedFirstElement;
        }
    }
}
