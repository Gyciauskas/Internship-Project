using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.ServiceModels;
using ServiceStack;

namespace PresentConnection.Internship7.Iot.Services
{
    public class DeleteRecipeConnectionService : ServiceBase
    {
        public IRecipeConnectionService RecipeConnectionService { get; set; }
        public IImageService ImagesService { get; set; }

        public DeleteRecipeConnectionResponse Any(DeleteRecipeConnection request)
        {
            var recipeConnection = RecipeConnectionService.GetRecipeConnection(request.Id);
            var recipeConnectionName = string.Empty;

            if (recipeConnection != null)
            {
                recipeConnectionName = recipeConnection.Name;

                // Delete all images before delete recipe connection
                if (recipeConnection.Images != null)
                {
                    foreach (var imageId in recipeConnection.Images)
                    {
                        // what if delete image fails? 
                        ImagesService.DeleteImage(imageId);
                    }
                }
            }

            var response = new DeleteRecipeConnectionResponse
            {
                Result = RecipeConnectionService.DeleteRecipeConnection(request.Id)
            };

            if (response.Result)
            {
                var cacheKeyForListWithName = CacheKeys.RecipeConnections.ListWithProvidedName.Fmt(recipeConnectionName);
                var cacheKeyForList = CacheKeys.RecipeConnections.List;
                var cacheKeyForItem = CacheKeys.RecipeConnections.Item.Fmt(request.Id);

                Request.RemoveFromCache(Cache, cacheKeyForList);
                Request.RemoveFromCache(Cache, cacheKeyForListWithName);
                Request.RemoveFromCache(Cache, cacheKeyForItem);
            }

            return response;
        }
    }
}
