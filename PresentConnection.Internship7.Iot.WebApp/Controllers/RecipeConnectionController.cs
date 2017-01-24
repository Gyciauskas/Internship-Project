using System.Web;
using System.Web.Mvc;
using PresentConnection.Internship7.Iot.ServiceModels;
using PresentConnection.Internship7.Iot.Services;
using PresentConnection.Internship7.Iot.WebApp.Extensions;
using PresentConnection.Internship7.Iot.WebApp.Models;
using ServiceStack;

namespace PresentConnection.Internship7.Iot.WebApp.Controllers
{
    public class RecipeConnectionController : ControllerBase
    {
        // GET: RecipeConnection
        public ActionResult List(string name = "")
        {
            var viewModel = new ListViewModelBase<RecipeConnectionDto>();

            using (var service = ResolveService<GetRecipeConnectionsService>())
            {
                var request = new GetRecipeConnections { Name = name };

                var response = service.Any(request) as GetRecipeConnectionsResponse;

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
            var viewModel = new RecipeConnectionDto();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RecipeConnectionDto viewModel, HttpPostedFileBase file)
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
                using (var service = ResolveService<CreateRecipeConnectionService>())
                {
                    var request = new CreateRecipeConnnection
                    {
                        Name = viewModel.Name,
                        FileName = file.FileName,
                        Image = ReadFileExtensions.ReadFully(file.InputStream)
                    };

                    var response = service.Any(request);
                    if (response.HasData())
                    {
                        return Redirect("/recipeConnections");
                    }
                }
            }
            return View(viewModel);
        }

        public ActionResult Update(string id)
        {
            var viewModel = new RecipeConnectionDto();

            using (var service = ResolveService<GetRecipeConnectionService>())
            {
                var request = new GetRecipeConnection { Id = id };
                var response = service.Any(request) as GetRecipeConnectionResponse;

                if (response.HasData())
                {
                    viewModel = response.Result;
                }
            }
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(RecipeConnectionDto viewModel)
        {
            if (ModelState.IsValid)
            {
                using (var service = ResolveService<UpdateRecipeConnectionService>())
                {
                    var request = new UpdateRecipeConnection
                    {
                        Id = viewModel.Id,
                        Name = viewModel.Name,
                        UniqueName = viewModel.UniqueName
                    };

                    var response = service.Any(request);
                    if (response.WasOk())
                    {
                        return Redirect("/recipeConnections");
                    }
                }
            }
            return View(viewModel);

        }

        public ActionResult Delete(string id)
        {
            using (var service = ResolveService<DeleteRecipeConnectionService>())
            {
                var request = new DeleteRecipeConnection { Id = id };
                var response = service.Any(request);
                if (response.WasOk())
                {
                    return Redirect("/recipeConnections");
                }
            }

            return Redirect("/recipeConnections");
        }
    }
}