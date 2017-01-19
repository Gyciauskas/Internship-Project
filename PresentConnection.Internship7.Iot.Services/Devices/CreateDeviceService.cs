using System;
using System.Collections.Generic;
using System.IO;
using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.BusinessImplementation;
using PresentConnection.Internship7.Iot.Domain;
using PresentConnection.Internship7.Iot.ServiceModels;
using PresentConnection.Internship7.Iot.Utils;
using ServiceStack;

namespace PresentConnection.Internship7.Iot.Services
{
    public class CreateDeviceService : ServiceBase
    {
        public IDeviceService DeviceService { get; set; }
        public IImageService ImagesService { get; set; }

        public CreateDeviceResponse Any(CreateDevice request)
        {
            var response = new CreateDeviceResponse();

            var sizes = new List<string> { "standard", "medium", "thumbnail" };
            var imageIds = new List<string>();

            // Original image
            var image = new DisplayImage
            {
                SeoFileName = Path.GetFileNameWithoutExtension(request.FileName),
                MimeType = Path.GetExtension(request.FileName),
            };
            imageIds.Add(ImagesService.AddImage(image, request.Image));

            // Different sizes
            foreach (var size in sizes)
            {
                image = new DisplayImage
                {
                    SeoFileName = Path.GetFileNameWithoutExtension(request.FileName),
                    MimeType = Path.GetExtension(request.FileName),
                    Size = size
                };
                imageIds.Add(ImagesService.AddImage(image, request.Image));
            }

            var device = new Device
            {
                ModelName = request.ModelName,
                UniqueName = SeoService.GetSeName(request.ModelName),
                Images = imageIds
            };

            try
            {
                DeviceService.CreateDevice(device);
            }
            catch (BusinessException e)
            {
                foreach (var imageId in device.Images)
                {
                    ImagesService.DeleteImage(imageId);
                }
                throw;
            }            

            var casheKey = CacheKeys.Devices.List;
            Request.RemoveFromCache(Cache, casheKey);

            response.Result = device.Id.ToString();
            return response;
        }
    }
}
