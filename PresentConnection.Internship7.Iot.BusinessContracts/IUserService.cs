using System.Collections.Generic;
using PresentConnection.Internship7.Iot.Domain;

namespace PresentConnection.Internship7.Iot.BusinessContracts
{
    public interface  IUserService
    {
        void CreateUser(User user);
        void UpdateUser(User user);
        bool DeleteUser(string id);
        List<User> GetAllUsers(string name = "");
        User GetUser(string id);
    }
}
