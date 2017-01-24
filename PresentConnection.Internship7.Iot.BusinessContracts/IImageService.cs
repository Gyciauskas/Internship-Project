using PresentConnection.Internship7.Iot.Domain;
using System.Collections.Generic;

namespace PresentConnection.Internship7.Iot.BusinessContracts
{
    public interface IImageService
    {
        List<string> GenerateImagesIds(string fileName, byte[] imageByteArray);
        string AddImage(DisplayImage displayImage, byte[] image);
        bool DeleteImage(string id);
        DisplayImage GetImage(string id);
    }
}