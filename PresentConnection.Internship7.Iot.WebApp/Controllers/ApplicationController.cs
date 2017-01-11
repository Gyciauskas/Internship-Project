using ServiceStack;
using ServiceStack.Mvc;

namespace PresentConnection.Internship7.Iot.WebApp.Controllers
{
    public class ApplicationController : ServiceStackController<AuthUserSession>
    {
        public AuthUserSession SsSession => UserSession;

    }
}