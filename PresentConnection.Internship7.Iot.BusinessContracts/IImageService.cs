using System.Collections.Generic;
using PresentConnection.Internship7.Iot.Domain;

namespace PresentConnection.Internship7.Iot.BusinessContracts
{
    public interface IImageService
    {
        string InsertImage(DisplayImage displayImage);
        List<DisplayImage> GetAllImages();
        DisplayImage GetImage(string id);
        bool DeleteImage(string id);
        string AddImage(DisplayImage displayImage, byte[] imageBytes);
        string GetOriginalImagePath(string id);
        string GetMediumImagePath(string id);
        string GetSmallImagePath(string id);
        string GetThumbImagePath(string id);
    }
}
