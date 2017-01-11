using System.Web.Mvc;
using PresentConnection.Internship7.Iot.ServiceModels;
using PresentConnection.Internship7.Iot.Services;
using PresentConnection.Internship7.Iot.WebApp.Extensions;
using PresentConnection.Internship7.Iot.WebApp.Models;

namespace PresentConnection.Internship7.Iot.WebApp.Controllers
{
    public class ManufacturerController : ControllerBase
    {

        public ActionResult List(string name = "")
        {
            var viewModel = new ListViewModelBase<GetManufacturersDto>();

            using (var service = ResolveService<GetManufacturersService>())
            {
                var request = new GetManufacturers
                {
                    Name = name
                };

                var response = service.Any(request) as GetManufacturersResponse;
                if (response.HasData())
                {
                    viewModel.Items = response.Result;
                }
            }

            return View(viewModel);
        }

        public ActionResult Create()
        {
            var viewModel = new CreateManufacturerViewModel();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(CreateManufacturerViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                using (var service = ResolveService<CreateManufacturerService>())
                {
                    var request = new CreateManufacturer
                    {
                        Name = viewModel.Name
                    };

                    if (viewModel.Image != null && viewModel.Image.ContentLength > 0)
                    {
                        request.Image = viewModel.Image.InputStream.ReadFully();
                    }

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
            var viewModel = new EditManufacturerViewModel(string.Empty);

            using (var service = ResolveService<GetManufacturerService>())
            {
                var request = new GetManufacturer
                {
                    Id = id
                };
                var response = service.Any(request) as GetManufacturerResponse;
                if (response.HasData())
                {
                    viewModel = new EditManufacturerViewModel(response.Result.UniqueName)
                    {
                        Id = id,
                        Name = response.Result.Name,
                        ImagePath = response.Result.ImagePath
                    };
                }
            }
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Update(EditManufacturerViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                using (var service = ResolveService<UpdateManufacturerService>())
                {
                    var request = new UpdateManufacturer
                    {
                        Name = viewModel.Name,
                        Id = viewModel.Id,
                        UniqueName = viewModel.UniqueName
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

        public ActionResult Delete(string id)
        {
            using (var service = ResolveService<DeleteManufacturerService>())
            {
                var request = new DeleteManufacturer
                {
                    Id = id
                };
                service.Any(request);
            }
            return Redirect("/manufacturers");
        }
    }
}