using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.Domain;


namespace PresentConnection.Internship7.Iot.ServiceModels
{
    
    public class ManufacturerDto 
    {
        public string Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        public string UniqueName { get; set; }
        public PictureDto Picture { get; set; }
        
        public static explicit operator ManufacturerDto(Manufacturer source)
        {
            if (source == null)
            {
                return null;
            }

            return new ManufacturerDto
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

            public ManufacturerDto ManufacturerDto { get; set; }

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
                        var imageDto = (DisplayImageDto) image;

//                        var pathToTheImages = ConfigurationManager.AppSettings["ImagesPath"]
//                            .MapHostAbsolutePath().SplitOnLast(Path.DirectorySeparatorChar).Last();
                        var pathToTheImages = "images";
                        imageDto.Url = Path.Combine(pathToTheImages, image.UniqueImageName + image.MimeType);

                        Images.Add(imageDto);
                    }
                }
                return this;
            }

            public Builder Map(Manufacturer manufacturer)
            {
                ManufacturerDto = (ManufacturerDto) manufacturer;
                return this;
            }

            public ManufacturerDto Build()
            {
                if (ManufacturerDto != null)
                {
                    ManufacturerDto.Picture = (PictureDto)Images;
                }
                return ManufacturerDto;
            }
        }

    }
}