using System.Web;
using System.Web.Mvc;
using PresentConnection.Internship7.Iot.ServiceModels;
using PresentConnection.Internship7.Iot.Services;
using PresentConnection.Internship7.Iot.WebApp.Extensions;
using PresentConnection.Internship7.Iot.WebApp.Models;
using ServiceStack;

namespace PresentConnection.Internship7.Iot.WebApp.Controllers
{
    public class ManufacturerController : ControllerBase
    {
        public ActionResult List(string name = "")
        {
            var viewModel = new ListViewModelBase<ManufacturerDto>();

            using (var service = ResolveService<GetManufacturersService>())
            {
                var request = new GetManufacturers { Name = name };

                var response = service.Any(request) as GetManufacturersResponse;
                
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
            var viewModel = new ManufacturerDto();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ManufacturerDto viewModel, HttpPostedFileBase file)
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
                using (var service = ResolveService<CreateManufacturerService>())
                {
                    var request = new CreateManufacturer
                    {
                        Name = viewModel.Name,
                        FileName = file.FileName,
                        Image = ReadFileExtensions.ReadFully(file.InputStream)
                    };

                    var response = service.Any(request);
                    if (response.HasData())
                    {
                        return Redirect("/manufacturers");
                    }
                }
            }
            return View(viewModel);
        }


        public ActionResult Update(string id)
        {
            var viewModel = new ManufacturerDto();

            using (var service = ResolveService<GetManufacturerService>())
            {
                var request = new GetManufacturer { Id = id };
                var response = service.Any(request) as GetManufacturerResponse;

                if (response.HasData())
                {
                    viewModel = response.Result;
                }
            }
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(ManufacturerDto viewModel)
        {
            if (ModelState.IsValid)
            {
                using (var service = ResolveService<UpdateManufacturerService>())
                {
                    var request = new UpdateManufacturer
                    {
                        Id = viewModel.Id,
                        Name = viewModel.Name,
                        UniqueName = viewModel.UniqueName
                    };

                    var response = service.Any(request);
                    if (response.WasOk())
                    {
                        return Redirect("/manufacturers");
                    }
                }
            }
            return View(viewModel);

        }

        public ActionResult Delete(string id)
        {
            using (var service = ResolveService<DeleteManufacturerService>())
            {
                var request = new DeleteManufacturer { Id = id };
                var response = service.Any(request);
                if (response.WasOk())
                {
                    return Redirect("/manufacturers");
                }
            }
            return Redirect("/manufacturers");
        }
    }
}