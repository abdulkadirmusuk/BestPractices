using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Refit;
using RefitRestfulAPITutorial.Business.Abstract;
using RefitRestfulAPITutorial.Services;
using System;

namespace RefitRestfulAPITutorial
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "RefitRestfulAPITutorial", Version = "v1" });
            });
            services.AddSingleton<ITodoService, TodoManager>(); //todoService injection dependency added
            //Aşağıdaki şekilde konfigurasyon yapılır
            services.AddRefitClient<ITodoAPI>().ConfigureHttpClient(
                    c => c.BaseAddress = new Uri("http://jsonplaceholder.typicode.com")
                );
            services.AddControllers();
            //services.AddMvc(options => options.EnableEndpointRouting = false);

            /* Bu şekilde refit settingste verilebilir
            services.AddRefitClient<ITodoAPI>(new RefitSettings { CollectionFormat = CollectionFormat.Csv })
            .ConfigureHttpClient(c => c.BaseAddress = new Uri("http://jsonplaceholder.typicode.com"));
            services.AddControllers(); 

             */
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RefitRestfulAPITutorial v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            //app.UseMvc(ConfigureRoute);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static void ConfigureRoute(IRouteBuilder routeBuilder)
        {
            routeBuilder.MapRoute("Default", "api/{controller}/{action}");
        }
    }
}
