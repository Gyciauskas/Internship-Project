using PresentConnection.Internship7.Iot.Domain;
using System.Collections.Generic;

namespace PresentConnection.Internship7.Iot.BusinessContracts
{
    public interface IUserDeviceService
    {
        string CreateUserDevice(UserDevices userDevice);
        void UpdateUserDevice(UserDevices userDevice);
        bool DeleteUserDevice(string id);
        List<UserDevices> GetAllUserDevices(string name = "");
        UserDevices GetUserDevice(string id);
    }
}
