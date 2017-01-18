using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.Domain;
using PresentConnection.Internship7.Iot.ServiceModels;
using ServiceStack;
using System.Collections.Generic;
using System.IO;

namespace PresentConnection.Internship7.Iot.Services
{
    public class CreateRecipeConnectionService : ServiceBase
    {
        public IRecipeConnectionService RecipeConnectionService { get; set; }
        public IImageService ImagesService { get; set; }

        public CreateRecipeConnectionResponse Any(CreateRecipeConnnection request)
        {
            var response = new CreateRecipeConnectionResponse();

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

            var recipeConnection = new RecipeConnection
            {
                Name = request.Name,
                UniqueName = request.Name.ToLower().Replace(" ", "-"), // TODO - make unique name from Name
                Images = imageIds
            };

            RecipeConnectionService.CreateRecipeConnection(recipeConnection);

            var cacheKey = CacheKeys.RecipeConnections.List;
            Request.RemoveFromCache(Cache, cacheKey);

            // now, return not recipe connection object, but just Id
            response.Result = recipeConnection.Id.ToString();
            return response;
        }
    }
}
