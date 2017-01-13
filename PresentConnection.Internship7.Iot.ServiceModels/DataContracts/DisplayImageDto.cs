using PresentConnection.Internship7.Iot.Domain;

namespace PresentConnection.Internship7.Iot.ServiceModels
{
    public class DisplayImageDto 
    {
        public string Id { get; set; }

        public string AltAttribute { get; set; }

        public string Url { get; set; }

        public string Size { get; set; }

        
        public static explicit operator DisplayImageDto(DisplayImage source)
        {
            if (source == null)
            {
                return null;
            }

            return new DisplayImageDto
            {
                Id = source.Id.ToString(),
                AltAttribute = source.AltAttribute,
                Size = source.Size
            };

        }
    }
}