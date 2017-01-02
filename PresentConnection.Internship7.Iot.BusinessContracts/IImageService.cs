using System.Collections.Generic;
using PresentConnection.Internship7.Iot.Domain;

namespace PresentConnection.Internship7.Iot.BusinessContracts
{
    public interface IImageService
    {
        string InsertImage(DisplayImage displayImage, byte[] image);
        List<DisplayImage> GetAllImages();
        DisplayImage GetImage(string id);
        bool DeleteImage(string id);
    }
}
