using System.Collections.Generic;
using Funq;
using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.BusinessImplementation;
using PresentConnection.Internship7.Iot.Services;
using ServiceStack;
using ServiceStack.Caching;
using ServiceStack.Text;
using ServiceStack.Validation;

namespace PresentConnection.Internship7.Iot.WebApp
{
    public class AppHost : AppHostBase
    {
        /// <summary>
        /// Base constructor requires a Name and Assembly where web service implementation is located
        /// </summary>
        public AppHost()
            : base("WebApp", typeof(GetManufacturersService).Assembly) { }

        /// <summary>
        /// Application specific configuration
        /// This method should initialize any IoC resources utilized by your web service classes.
        /// </summary>
        public override void Configure(Container container)
        {

            //Set JSON web services to return idiomatic JSON camelCase properties
            JsConfig.EmitCamelCaseNames = true;

            const Feature disabledFeatures = Feature.Jsv | Feature.Soap11; // | Feature.Metadata | Feature.Csv

            SetConfig(new HostConfig
            {
                // DefaultRedirectPath = "/default.html", // Default redirect path for API
                EnableFeatures = Feature.All.Remove(disabledFeatures),
                GlobalResponseHeaders =
                        {
                            {"Access-Control-Allow-Origin", "*"},
                            {"Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS"},
                            {"Access-Control-Allow-Headers", "Content-Type, X-Requested-With, Accept"},
                        },
                AppendUtf8CharsetOnContentTypes = new HashSet<string> { "html" },
                // WsdlSoapActionNamespace = "http://internship7.com/types",

#if DEBUG
                DebugMode = true, //Show StackTraces when developing
#endif
                WriteErrorsToResponse = true,           // Disable exception handling
                DefaultContentType = "json",  // Change default content type

                // AllowJsonpRequests = true

            });
            
            
            // reusable services

            container.Register<IManufacturerService>(new ManufacturerService());
            container.Register<IDeviceService>(new DeviceService());
            container.Register<IConnectionService>(new ConectionService());
            container.Register<IRecipeService>(new RecipeService());
            container.Register<ICollaboratorService>(new CollaboratorService());
            container.Register<IRecipeConnectionService>(new RecipeConnectionService());


            // Caching
            // TODO replace with Redis when have Docker prepared
            container.Register<ICacheClient>(new MemoryCacheClient());

            //ConfigureAuthAsync(container, database);
            

            // Plugins
            Plugins.Add(new CorsFeature(
                allowedMethods: "GET, POST, PUT, DELETE, OPTIONS",
                allowedHeaders: "Content-Type, X-Requested-With, X-FormSchemaApiToken",
                allowCredentials: true));
            Plugins.Add(new PostmanFeature());

            // Plugins.Add(new RegistrationFeature()); 

            //Plugins.Add(new ValidationFeature());
            //container.RegisterValidators(typeof(CreateManufacturerService).Assembly, typeof(CreateManufacturer).Assembly);
            
            //Set MVC to use the same Funq IOC as ServiceStack
            // ControllerBuilder.Current.SetControllerFactory(new FunqControllerFactory(container));
            // ServiceStackController.CatchAllController = reqCtx => container.TryResolve<HomeController>();


            //Config examples
            Plugins.Add(new PostmanFeature());
            //this.Plugins.Add(new CorsFeature());
        }
    }
}