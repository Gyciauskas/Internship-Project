using System;
using System.Collections.Generic;
using System.Linq;

namespace PresentConnection.Internship7.Iot.ServiceModels
{
    public class PictureDto
    {
        private List<DisplayImageDto> Images { get; }

        private readonly Func<List<DisplayImageDto>, string, DisplayImageDto> GetImage = (images, size) =>
        {
            if (images != null && images.Any())
            {
                // TODO - change it with constant
                var image = images.FirstOrDefault(x => x.Size == size);
                if (image != null)
                {
                    return image;
                }
            }
            return ReturnImageNotSet(size);
        };

        private static DisplayImageDto ReturnImageNotSet(string size = "")
        {
            return new DisplayImageDto
            {
                // TODO change path with path from web config
                Url = $"path_to_image_not_set_{size}.jpg"
            };
        }

        public PictureDto(List<DisplayImageDto> images)
        {
            Images = images;
        }

        public DisplayImageDto Thumbnail => GetImage(Images, "thumbnail");
        public DisplayImageDto Standard => GetImage(Images, "standard");
        public DisplayImageDto Medium => GetImage(Images, "medium");


        public static explicit operator PictureDto(List<DisplayImageDto> source)
        {
            return new PictureDto(source);
        }
    }
}