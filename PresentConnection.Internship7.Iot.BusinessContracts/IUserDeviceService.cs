using PresentConnection.Internship7.Iot.Domain;
using System.Collections.Generic;

namespace PresentConnection.Internship7.Iot.BusinessContracts
{
    public interface IUserDeviceService
    {
        string CreateUserDevice(UserDevice userDevice);
        void UpdateUserDevice(UserDevice userDevice);
        bool DeleteUserDevice(string id);
        List<UserDevice> GetAllUserDevices(string name = "");
        UserDevice GetUserDevice(string id);
    }
}
