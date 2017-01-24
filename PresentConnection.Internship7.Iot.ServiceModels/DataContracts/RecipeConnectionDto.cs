using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.Domain;
using ServiceStack;

namespace PresentConnection.Internship7.Iot.ServiceModels
{
    public class RecipeConnectionDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string UniqueName { get; set; }
        public PictureDto Picture { get; set; }

        public static explicit operator RecipeConnectionDto(RecipeConnection source)
        {
            if (source == null)
            {
                return null;
            }

            return new RecipeConnectionDto
            {
                Id = source.Id.ToString(),
                Name = source.Name,
                UniqueName = source.UniqueName
            };
        }

        public static Builder With(IImageService imageService)
        {
            return new Builder(imageService);
        }

        public class Builder
        {
            private readonly IImageService imageService;

            public List<DisplayImageDto> Images { get; set; }

            public RecipeConnectionDto RecipeConnectionDto { get; set; }

            public Builder(IImageService imageService)
            {
                Images = new List<DisplayImageDto>();
                this.imageService = imageService;
            }

            public Builder ApplyImages(List<string> imageIds)
            {
                if (imageIds != null && imageIds.Any())
                {
                    foreach (var imageId in imageIds)
                    {
                        var image = imageService.GetImage(imageId);
                        var imageDto = (DisplayImageDto)image;

                        var pathToTheImages = "images";
                        imageDto.Url = Path.Combine(pathToTheImages, image.UniqueImageName + image.MimeType);

                        Images.Add(imageDto);
                    }
                }

                return this;
            }

            public Builder Map(RecipeConnection recipeConnection)
            {
                RecipeConnectionDto = (RecipeConnectionDto)recipeConnection;
                return this;
            }

            public RecipeConnectionDto Build()
            {
                if (RecipeConnectionDto != null)
                {
                    RecipeConnectionDto.Picture = (PictureDto)Images;
                }

                return RecipeConnectionDto;
            }
        }
    }
}

