using System.Collections.Generic;
using System.IO;
using System.Linq;
using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.Domain;

namespace PresentConnection.Internship7.Iot.ServiceModels
{
    public class DeviceDto
    {
        public string Id { get; set; }
        public string ModelName { get; set; }
        public string UniqueName { get; set; }
        public PictureDto Picture { get; set; }

        public static explicit operator DeviceDto(Device source)
        {
            if (source == null)
            {
                return null;
            }

            return new DeviceDto()
            {
                Id = source.Id.ToString(),
                ModelName = source.ModelName,
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

            public DeviceDto DeviceDto { get; set; }

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

                        var pathToTheImages = "images";
                        imageDto.Url = Path.Combine(pathToTheImages, image.UniqueImageName + image.MimeType);

                        Images.Add(imageDto);
                    }
                }
                return this;
            }

            public Builder Map(Device device)
            {
                DeviceDto = (DeviceDto)device;
                return this;
            }

            public DeviceDto Build()
            {
                if (DeviceDto != null)
                {
                    DeviceDto.Picture = (PictureDto)Images;
                }
                return DeviceDto;
            }
        }
    }
}
