using Autofac;
using Autofac.Integration.Web;
using eShopLegacyWebForms.Models;
using eShopLegacyWebForms.Models.Infrastructure;
using eShopLegacyWebForms.Modules;
using log4net;
using System;
using System.Configuration;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace eShopLegacyWebForms
{
    public class Startup
    {
        RequestDelegate _next = null;
        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        static IContainerProvider _containerProvider;
        IContainer container;
        public IConfiguration Configuration { get; }

        public IWebHostEnvironment Env { get; }

        public IContainerProvider ContainerProvider
        {
            get
            {
                return _containerProvider;
            }
        }

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Env = env;
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseStaticFiles();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
            if (Env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            
            // Code that runs on application startup
            // The following lines were extracted from Application_Start
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ConfigureContainer();
            ConfigDataBase();
            // This code replaces the original handling
            // of the BeginRequest event
            app.Use(async (context, next) =>
            {
                //set the property to our new object
                LogicalThreadContext.Properties["activityid"] = new ActivityIdHelper();
                LogicalThreadContext.Properties["requestinfo"] = new WebRequestInfo();
                _log.Debug("Application_BeginRequest");
                await next();
            });
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();
             // Did not attempt to migrate service layer
            // and configure dependency injection in ConfigureServices(),
            // this must be done manually
        }

        /// <summary>
        /// http://docs.autofac.org/en/latest/integration/webforms.html
        /// </summary>
        private void ConfigureContainer()
        {
            var builder = new ContainerBuilder();
            var mockData = bool.Parse(ConfigurationManager.Configuration.GetSection("appSettings")["UseMockData"]);
            builder.RegisterModule(new ApplicationModule(mockData));
            container = builder.Build();
            _containerProvider = new ContainerProvider(container);
        }

        private void ConfigDataBase()
        {
            var mockData = bool.Parse(ConfigurationManager.Configuration.GetSection("appSettings")["UseMockData"]);
            if (!mockData)
            {
                Database.SetInitializer<CatalogDBContext>(container.Resolve<CatalogDBInitializer>());
            }
        }
        // Unable to migrate the following code, as a result it was removed
        // /// <summary>
/// Track the machine name and the start time for the session inside the current session
/// </summary>
protected void Session_Start(Object sender, EventArgs e)
        // {
        //     HttpContext.Current.Session["MachineName"] = Environment.MachineName;
        //     HttpContext.Current.Session["SessionStartTime"] = DateTime.Now;
        // }
    }
}