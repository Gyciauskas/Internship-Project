using FluentValidation;
using CodeMash.Net;

namespace PresentConnection.Internship7.Iot.Domain
{
    public class UserDeviceValidators : AbstractValidator<UserDevice>
    {
        public UserDeviceValidators()
        {
            RuleFor(r => r.UserId).NotEmpty();
            RuleFor(r => r.DeviceDisplayId).NotEmpty();
            RuleFor(r => r.DeviceId).NotEmpty();
            RuleFor(r => r.Longitude).NotEmpty();
            RuleFor(r => r.Latitude).NotEmpty();
            RuleFor(r => r.AuthKey1).NotEmpty();
            RuleFor(r => r.AuthKey2).NotEmpty();
            // is unique  RuleFor(r => r.DeviceDisplayId). 
            RuleFor(x => x).Must(ValidDeviceDisplayId).WithMessage("Name is not unique");
        }
        private bool ValidDeviceDisplayId(UserDevice userDevice)
        {
            var UserDevicesFromDb = Db.FindOne<UserDevice>(x => x.DeviceDisplayId == userDevice.DeviceDisplayId);
            if ((UserDevicesFromDb.DeviceDisplayId == userDevice.DeviceDisplayId) && (UserDevicesFromDb.Id != userDevice.Id))
            {
                return false;
            }
            else
            {
                if (Db.Count<UserDevice>(x => x.DeviceDisplayId == userDevice.DeviceDisplayId) >= 2)
                {
                    return false;
                }
                return true;
            }
        }
    }
}

