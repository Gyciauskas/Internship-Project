﻿using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.ServiceModels;
using ServiceStack;

namespace PresentConnection.Internship7.Iot.Services
{
    public class DeleteUserService : ServiceBase
    {
        public IUserService UserService { get; set; }

        public DeleteUserResponse Any(DeleteUser request)
        {
            var response = new DeleteUserResponse
            {
                Result = UserService.DeleteUser(request.Id)
            };

            return response;
        }
    }
}
