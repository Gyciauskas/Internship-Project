using FluentValidation;
using CodeMash.Net;

namespace PresentConnection.Internship7.Iot.Domain
{
    public class ClientDeviceValidator : AbstractValidator<ClientDevice>
    {
        public ClientDeviceValidator(string clientId)
        {
            RuleFor(r => r.ClientId).NotEmpty();
            RuleFor(r => r.DeviceDisplayId).NotEmpty();
            RuleFor(r => r.DeviceId).NotEmpty();
            RuleFor(r => r.Longitude).NotEmpty();
            RuleFor(r => r.Latitude).NotEmpty();
            RuleFor(r => r.AuthKey1).NotEmpty();
            RuleFor(r => r.AuthKey2).NotEmpty();
            RuleFor(r => r.IsEnabled).Equal(true);
            RuleFor(r => r.IsConnected).Equal(true);
            RuleFor(r => r).CheckAccessPermissions(clientId);
            RuleFor(x => x).Must(ValidDeviceDisplayId).WithMessage("Name is not unique");
        }

        private static bool ValidDeviceDisplayId(ClientDevice clientDevice)
        {
            var clientDeviceFromDb = Db.FindOne<ClientDevice>(x => x.DeviceDisplayId == clientDevice.DeviceDisplayId);
            if ((clientDeviceFromDb.DeviceDisplayId == clientDevice.DeviceDisplayId) && (clientDeviceFromDb.Id != clientDevice.Id))
            {
                return false;
            }
            return Db.Count<ClientDevice>(x => x.DeviceDisplayId == clientDevice.DeviceDisplayId) < 2;
        }
    }
}

