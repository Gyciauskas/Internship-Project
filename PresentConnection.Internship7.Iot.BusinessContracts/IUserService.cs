using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PresentConnection.Internship7.Iot.Domain;

namespace PresentConnection.Internship7.Iot.BusinessContracts
{
    public interface  IUserService
    {
        string CreateUser(User user);
        void UpdateUser(User user);
        bool DeleteUser(string id);
        List<User> GetAllUsers(string name = "");
        User GetUser(string id);

    }
}
