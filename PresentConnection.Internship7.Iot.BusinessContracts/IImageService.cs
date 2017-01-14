using PresentConnection.Internship7.Iot.Domain;

namespace PresentConnection.Internship7.Iot.BusinessContracts
{
    public interface IImageService
    {
        string AddImage(DisplayImage displayImage, byte[] image);
        bool DeleteImage(string id);
    }
}