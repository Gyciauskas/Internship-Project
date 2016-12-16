﻿using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.Domain;
using PresentConnection.Internship7.Iot.ServiceModels;
using ServiceStack;

namespace PresentConnection.Internship7.Iot.Services
{
    public class CreateDeviceService : Service
    {
        public IDeviceService DeviceService { get; set; }

        public CreateDeviceResponse Any(CreateDevice request)
        {
            var response = new CreateDeviceResponse();

            var device = new Device().PopulateWith(request);
            DeviceService.CreateDevice(device);

            return response;
        }
    }
}