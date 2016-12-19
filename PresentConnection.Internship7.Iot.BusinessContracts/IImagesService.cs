using PresentConnection.Internship7.Iot.Domain;

namespace PresentConnection.Internship7.Iot.BusinessContracts
{
    public interface IImagesService
    {
        string InsertImage(byte[] image);
    }
}
