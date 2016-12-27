using ServiceStack;

namespace PresentConnection.Internship7.Iot.Services
{
    public class ServiceBase : Service
    {
        public AuthUserSession UserSession => SessionAs<AuthUserSession>();
    }
}