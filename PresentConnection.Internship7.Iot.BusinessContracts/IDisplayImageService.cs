using PresentConnection.Internship7.Iot.Domain;

namespace PresentConnection.Internship7.Iot.BusinessContracts
{
    public interface IDisplayImageService
    {
        string CreateDisplayImage(DisplayImage displayImage);
        void UpdateDisplayImage(DisplayImage displayImage);
        DisplayImage GetDisplayImage(string id);
        bool DeleteDisplayImage(string id);
    }
}
