﻿using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.Domain;
using PresentConnection.Internship7.Iot.ServiceModels;
using ServiceStack;

namespace PresentConnection.Internship7.Iot.Services
{
    public class CreateManufacturerService : ServiceBase
    {
        public IManufacturerService ManufacturerService { get; set; }

        public CreateManufacturerResponse Any(CreateManufacturer request)
        {
            var response = new CreateManufacturerResponse();

            var manufacturer = new Manufacturer
            {
                Name = request.Name,
                UniqueName = request.UniqueName,
                Images = request.Images
            };

            ManufacturerService.CreateManufacturer(manufacturer);

            return response;
        }
    }
}