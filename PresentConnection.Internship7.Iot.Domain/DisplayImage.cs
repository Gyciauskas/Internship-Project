using System;
using CodeMash.Net;

namespace PresentConnection.Internship7.Iot.Domain
{
    [CollectionName(Statics.Collections.Images)]
    public class DisplayImage : EntityBase
    {
        public DisplayImage()
        {
            Tag = "Default";
            UniqueImageName = Guid.NewGuid();
        }

        /// <summary>
        /// representative image name. Show when downloading instead of uniquaName
        /// </summary>
        public string SeoFileName { get; set; }

        public string AltAttribute { get; set; }

        public string TitleAttribute { get; set; }
        
        /// <summary>
        /// image extension e.g.: jpg, png, gif
        /// </summary>
        public string MimeType { get; set; }
        public Guid UniqueImageName { get; set; }
        public string Tag { get; set; }

        public string Size { get; set; }
    }
}