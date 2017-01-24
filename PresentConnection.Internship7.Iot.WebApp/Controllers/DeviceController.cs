using System.Web;
using System.Web.Mvc;
using PresentConnection.Internship7.Iot.ServiceModels;
using PresentConnection.Internship7.Iot.Services;
using PresentConnection.Internship7.Iot.WebApp.Extensions;
using PresentConnection.Internship7.Iot.WebApp.Models;
using ServiceStack;

namespace PresentConnection.Internship7.Iot.WebApp.Controllers
{
    public class DeviceController : ControllerBase
    {
        public ActionResult List(string name = "")
        {
            var viewModel = new ListViewModelBase<DeviceDto>();

            using (var service = ResolveService<GetDevicesService>())
            {
                var request = new GetDevices { Name = name };

                var response = service.Any(request) as GetDevicesResponse;

                if (response.HasData())
                {
                    viewModel.TotalCount = response.TotalCount;
                    viewModel.Items = response.Result;
                }
            }

            return View(viewModel);
        }

        public ActionResult Create()
        {
            var viewModel = new DeviceDto();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DeviceDto viewModel, HttpPostedFileBase file)
        {
            if (file == null)
            {
                ModelState.AddModelError("file-upload", "Please upload file");
            }
            else
            {
                var extension = file.ContentType;

                if (extension != MimeTypes.ImageJpg && extension != MimeTypes.ImagePng)
                {
                    ModelState.AddModelError("file-extension", "Please upload right format file");
                }
            }

            if (ModelState.IsValid)
            {
                using (var service = ResolveService<CreateDeviceService>())
                {
                    var request = new CreateDevice
                    {
                        ModelName = viewModel.ModelName,
                        FileName = file.FileName,
                        Image = ReadFileExtensions.ReadFully(file.InputStream)
                    };

                    var response = service.Any(request);
                    if (response.HasData())
                    {
                        return Redirect("/devices");
                    }
                }
            }
            return View(viewModel);
        }

        public ActionResult Update(string id)
        {
            var viewModel = new DeviceDto();

            using (var service = ResolveService<GetDeviceService>())
            {
                var request = new GetDevice { Id = id };
                var response = service.Any(request) as GetDeviceResponse;

                if (response.HasData())
                {
                    viewModel = response.Result;
                }
            }
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(DeviceDto viewModel)
        {
            if (ModelState.IsValid)
            {
                using (var service = ResolveService<UpdateDeviceService>())
                {
                    var request = new UpdateDevice
                    {
                        Id = viewModel.Id,
                        ModelName = viewModel.ModelName,
                        UniqueName = viewModel.UniqueName
                    };

                    var response = service.Any(request);
                    if (response.WasOk())
                    {
                        return Redirect("/devices");
                    }
                }
            }
            return View(viewModel);

        }

        public ActionResult Delete(string id)
        {
            using (var service = ResolveService<DeleteDeviceService>())
            {
                var request = new DeleteDevice { Id = id };
                var response = service.Any(request);
                if (response.WasOk())
                {
                    return Redirect("/devices");
                }
            }
            return Redirect("/devices");
        }
    }
}