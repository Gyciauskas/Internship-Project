using ServiceStack;

namespace PresentConnection.Internship7.Iot.Services
{
    public class ServiceBase : Service
    {
        internal static class CacheKeys
        {
            internal static class Manufacturers
            {
                public static readonly string List = "urn:manufacturers";
                public static readonly string ListWithProvidedName = "urn:manufacturers:name-{0}";
                public static readonly string Item = "urn:manufacturers:{0}";
            }

            internal static class Collaborators
            {
                public static readonly string List = "urn:collaborators";
                public static readonly string ListWithProvidedName = "urn:collaborators:name-{0}";
                public static readonly string Item = "urn:collaborators:{0}";
            }
            
        }

        

        public AuthUserSession UserSession => SessionAs<AuthUserSession>();
    }
}