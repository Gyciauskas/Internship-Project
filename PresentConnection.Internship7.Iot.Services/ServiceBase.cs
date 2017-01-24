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


            internal static class ClientDevices
            {
                public static readonly string List = "urn:clientDevice";
                public static readonly string ListWithProvidedName = "urn:clientDevice:name-{0}";
                public static readonly string Item = "urn:clientDevice:{0}";
            }

            internal static class ClientRecipes
            {
                public static readonly string List = "urn:clientRecipe";
                public static readonly string ListWithProvidedName = "urn:clientRecipe:name-{0}";
                public static readonly string Item = "urn:clientRecipe:{0}";
            }

            internal static class ConnectionGroup
            {
                public static readonly string List = "urn:connectionGroup";
                public static readonly string ListWithProvidedName = "urn:connectionGroup:name-{0}";
                public static readonly string Item = "urn:connectionGroup:{0}";

                
            }

            internal static class Recipes
            {
                public static readonly string List = "urn:recipe";
                public static readonly string ListWithProvidedName = "urn:recipe:name-{0}";
                public static readonly string Item = "urn:recipe:{0}";
            }

        }

        public AuthUserSession UserSession => SessionAs<AuthUserSession>();
    }
}