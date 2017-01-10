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

            internal static class Devices
            {
                public static readonly string List = "urn:devices";
                public static readonly string ListWithProvidedName = "urn:devices:name-{0}";
                public static readonly string Item = "urn:devices:{0}";
            }

            internal static class RecipeConnections
            {
                public static readonly string List = "urn:recipeConnections";
                public static readonly string ListWithProvidedName = "urn:recipeConnections:name-{0}";
                public static readonly string Item = "urn:recipeConnections:{0}";

            }
        }
        

        public AuthUserSession UserSession => SessionAs<AuthUserSession>();
    }
}