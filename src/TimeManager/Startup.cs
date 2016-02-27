using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Auth;
using Auth.Model;
using Auth.Services;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc.Controllers;
using Microsoft.AspNet.Mvc.ViewComponents;
using Microsoft.AspNet.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mongo;
using SimpleInjector;
using SimpleInjector.Integration.AspNet;
using TimeManager.Database;
using TimeManager.Model;

namespace TimeManager
{
    public class Startup
    {
        private Container container = new Container();

        public IConfiguration Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddRouting();
            services.AddLogging();
            services.AddAuthorization();

            services.AddInstance<IControllerActivator>(new SimpleInjectorControllerActivator(container));
            services.AddInstance<IViewComponentInvokerFactory>(new SimpleInjectorViewComponentInvokerFactory(container));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            var configBuilder = new ConfigurationBuilder()
              .AddJsonFile("appsettings.json");

            Configuration = configBuilder.Build();

            container.Options.DefaultScopedLifestyle = new AspNetRequestLifestyle();

            app.UseSimpleInjectorAspNetRequestScoping(container);
            
            InitializeContainer(app);

            container.RegisterAspNetControllers(app);

            container.Verify();

            app.UseCookieAuthentication(p =>
            {
                p.AuthenticationScheme = "CustomSheme";
                p.LoginPath = "/login";
                p.AutomaticAuthenticate = true;
                p.AutomaticChallenge = true;
            });

            app.UseStaticFiles();
            app.UseIISPlatformHandler();
            app.UseDeveloperExceptionPage();
            app.UseMvc(ConfigureRoutes);


        }

        private void ConfigureRoutes(IRouteBuilder routeBuilder)
        {

            routeBuilder.MapRoute(
             name: "Home",
             template: "",
             defaults: new { controller = "Home", action = "Index" });


            routeBuilder.MapRoute(
                name: "registration",
                template: "registration",
                defaults: new {controller = "Registration", action = "Registration" });

            routeBuilder.MapRoute(
                name: "login",
                template: "login",
                defaults: new {controller = "Login", action = "Login"});

            routeBuilder.MapRoute(
                name: "default",
                template: "{controller}/{action}");


        }

        private void InitializeContainer(IApplicationBuilder app)
        {
            container.RegisterSingleton(() => new LocalDb(Configuration["Database:TimeManagerDb"]));
            container.Register<IUserService, UserService>();
            container.Register(() => new UserManager(container.GetRequiredService<IUserService>()));
            container.Register(() => new DbCollection<User>(container.GetService<LocalDb>()), Lifestyle.Transient);

        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
