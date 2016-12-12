using PresentConnection.Internship7.Iot.Domain;
using System.Collections.Generic;

namespace PresentConnection.Internship7.Iot.ServiceModels
{
    public class GetUsersResponse
    {
        public GetUsersResponse()
        {
            Users = new List<User>();
        }
        public List<User> Users { get; set; }      
    }
}