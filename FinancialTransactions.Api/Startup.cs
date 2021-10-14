using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Linq;
using FinancialTransactions.Api.Controllers;
using Microsoft.OData.Edm;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using FinancialTransactions.Services.Abstractions;
using Microsoft.AspNetCore.OData;
using Microsoft.OData.ModelBuilder;

namespace FinancialTransactions.Api
{
    public class Startup
    {
        const string VERSION = "0.1.0";
        const string APP_NAME = "v1";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTestServices();
            services.AddControllers().AddNewtonsoftJson(options =>
               options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(APP_NAME, new OpenApiInfo { Title = "FinancialTransactions.Api", Version = VERSION });
            });
            services.AddCors();
            services.AddControllers().AddOData(opt => opt.AddRouteComponents("odata", GetEdmModel()).Select().Expand().Filter().OrderBy().SetMaxTop(50).Count());
            services.AddLogging((loggingBuilder) =>
            {
                loggingBuilder.AddDebug();
            });
            Jwt.AddJwtAuthentication(services, Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ISeedService seedService)
        {
            seedService.Seed();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint($"/swagger/{APP_NAME}/swagger.json?version={VERSION}", "FinancialTransactions.Api"));

            // global cors policy
            app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true) // allow any origin
                .AllowCredentials()); // allow credentials

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();//must be before UseAuthorization
            app.UseAuthorization();

            app.UseMiddleware<RequestMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGet("/version", async context =>
                {
                    await context.Response.WriteAsync(VERSION);
                });
            });
        }

        private IEdmModel GetEdmModel()
        {
            var builder = new ODataConventionModelBuilder();
            builder.EnableLowerCamelCase();
            var odataControllers = GetType().Assembly.GetTypes().Where(e => e.BaseType.Name == typeof(DataController<>).Name);
            foreach (var odataController in odataControllers)
            {
                var entityType = odataController.BaseType.GenericTypeArguments.FirstOrDefault();
                EntitySet(builder, entityType, odataController.Name);
            }
            var edmModel = builder.GetEdmModel();
            return edmModel;
        }

        private static void EntitySet(ODataConventionModelBuilder builder, Type entitySetType, string controllerName)
        {
            var entitySetName = controllerName.Replace("Controller","");
            typeof(ODataConventionModelBuilder)
                .GetMethod(nameof(ODataConventionModelBuilder.EntitySet))
                .MakeGenericMethod(entitySetType).Invoke(builder, new[]{ entitySetName });
        }
    }
}
