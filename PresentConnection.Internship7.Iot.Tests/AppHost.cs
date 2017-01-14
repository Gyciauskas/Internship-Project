using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Funq;
using NUnit.Framework;
using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.BusinessImplementation;
using PresentConnection.Internship7.Iot.Services;
using ServiceStack;
using ServiceStack.Caching;
using ServiceStack.Data;
using ServiceStack.DataAnnotations;

namespace PresentConnection.Internship7.Iot.Tests
{
    //Create your ServiceStack AppHost with only the dependencies it needs
    public class AppHost : AppSelfHostBase
    {
        public AppHost() : base("IoT Service", typeof(CreateManufacturerService).Assembly) { }

        public override void Configure(Container container)
        {
            // reusable services

            container.Register<IManufacturerService>(new ManufacturerService());
            container.Register<IDeviceService>(new DeviceService());
            container.Register<IConnectionService>(new ConectionService());
            container.Register<IRecipeService>(new RecipeService());
            container.Register<ICollaboratorService>(new CollaboratorService());
            container.Register<IRecipeConnectionService>(new RecipeConnectionService());
            container.Register<IImageService>(new ImageService
            {
                DisplayImageService = new DisplayImageService(),
                FileService = new FileService()
            });
            container.Register<IDisplayImageService>(new DisplayImageService());
            container.Register<IComponentService>(new ComponentService());

            // Caching
            // TODO replace with Redis when have Docker prepared
            container.Register<ICacheClient>(new MemoryCacheClient());

            //ConfigureAuthAsync(container, database);


            // Plugins
            Plugins.Add(new CorsFeature(
                allowedMethods: "GET, POST, PUT, DELETE, OPTIONS",
                allowedHeaders: "Content-Type, X-Requested-With, X-FormSchemaApiToken",
                allowCredentials: true));
        }
    }
}
