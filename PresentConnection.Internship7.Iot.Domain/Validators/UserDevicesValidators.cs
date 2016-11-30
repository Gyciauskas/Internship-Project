using FluentValidation;
using CodeMash.Net;

namespace PresentConnection.Internship7.Iot.Domain
{
    public class UserDeviceValidators : AbstractValidator<ClientDevice>
    {
        public UserDeviceValidators()
        {
            RuleFor(r => r.ClientId).NotEmpty();
            RuleFor(r => r.DeviceDisplayId).NotEmpty();
            RuleFor(r => r.DeviceId).NotEmpty();
            RuleFor(r => r.Longitude).NotEmpty();
            RuleFor(r => r.Latitude).NotEmpty();
            RuleFor(r => r.AuthKey1).NotEmpty();
            RuleFor(r => r.AuthKey2).NotEmpty();
            // is unique  RuleFor(r => r.DeviceDisplayId). 
            RuleFor(x => x).Must(ValidDeviceDisplayId).WithMessage("Name is not unique");
        }
        private bool ValidDeviceDisplayId(ClientDevice clientDevice)
        {
            var UserDevicesFromDb = Db.FindOne<ClientDevice>(x => x.DeviceDisplayId == clientDevice.DeviceDisplayId);
            if ((UserDevicesFromDb.DeviceDisplayId == clientDevice.DeviceDisplayId) && (UserDevicesFromDb.Id != clientDevice.Id))
            {
                return false;
            }
            else
            {
                if (Db.Count<ClientDevice>(x => x.DeviceDisplayId == clientDevice.DeviceDisplayId) >= 2)
                {
                    return false;
                }
                return true;
            }
        }
    }
}

