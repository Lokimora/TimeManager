using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mongo;
using TimeManager.Database;
using TimeManager.Model;

namespace TimeManager
{
    public class Startup
    {

        public IConfiguration Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {


            services.AddMvc();
            services.AddRouting();
            services.AddLogging();
            services.AddSingleton<LocalDb>(p => new LocalDb(Configuration.Get("Database:TimeManagerDb")));

            var builder = services.BuildServiceProvider();

            services.AddTransient<DbCollection<TestModel>>(
                p => new DbCollection<TestModel>(builder.GetService<LocalDb>()));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {



            var configBuilder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json");

            Configuration = configBuilder.Build();

            app.UseIISPlatformHandler();

            app.UseDeveloperExceptionPage();

            app.UseMvcWithDefaultRoute();



            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }


        private void BuildConfig()
        {

        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
