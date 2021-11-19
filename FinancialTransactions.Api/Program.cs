using FinancialTransactions;
using FinancialTransactions.Api;
using FinancialTransactions.Api.Controllers;
using FinancialTransactions.Services.Abstractions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.OData;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using Microsoft.OpenApi.Models;
using System;
using System.Linq;
using System.Reflection;

const string VERSION = "0.1.0";
const string APP_NAME = "v1";

var applicationBuilder = WebApplication.CreateBuilder(args);

// Add services to the container.
var app = AddServices(applicationBuilder).Build();

Run(app);

static WebApplicationBuilder AddServices(WebApplicationBuilder builder)
{
    var mvcBuilder = builder.Services.AddControllers();
    mvcBuilder.AddNewtonsoftJson(options =>
       options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
    );
    mvcBuilder.AddOData(opt => opt.AddRouteComponents("odata", GetEdmModel()).Select().Expand().Filter().OrderBy().SetMaxTop(50).Count());

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc(APP_NAME, new OpenApiInfo { Title = "FinancialTransactions.Api", Version = VERSION });
    });

    builder.Services.AddTestServices();
    builder.Services.AddCors();
    builder.Services.AddLogging((loggingBuilder) =>
    {
        loggingBuilder.AddDebug();
    });
    Jwt.AddJwtAuthentication(builder.Services, builder.Configuration);

    return builder;
}

static void Run(WebApplication app)
{
    //app.Services.GetRequiredService<ISeedService>().Seed();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint($"/swagger/{APP_NAME}/swagger.json?version={VERSION}", "FinancialTransactions.Api"));
    }
    app.UseCors(x => x
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .SetIsOriginAllowed(origin => true) // allow any origin
                    .AllowCredentials()); // allow credentials

    app.UseHttpsRedirection();

    app.UseAuthentication();//must be before UseAuthorization
    app.UseAuthorization();

    app.UseMiddleware<RequestMiddleware>();

    app.MapControllers();
    app.MapGet("/version", async context =>
    {
        await context.Response.WriteAsync(VERSION);
    });

    app.Run();
}

static IEdmModel GetEdmModel()
{
    var builder = new ODataConventionModelBuilder();
    builder.EnableLowerCamelCase();
    var odataControllers = Assembly.GetExecutingAssembly().GetTypes().Where(e => e.BaseType.Name == typeof(DataController<>).Name);
    foreach (var odataController in odataControllers)
    {
        var entityType = odataController.BaseType.GenericTypeArguments.FirstOrDefault();
        EntitySet(builder, entityType, odataController.Name);
    }
    var edmModel = builder.GetEdmModel();
    return edmModel;
}

static void EntitySet(ODataConventionModelBuilder builder, Type entitySetType, string controllerName)
{
    var entitySetName = controllerName.Replace("Controller", "");
    typeof(ODataConventionModelBuilder)
        .GetMethod(nameof(ODataConventionModelBuilder.EntitySet))
        .MakeGenericMethod(entitySetType).Invoke(builder, new[] { entitySetName });
}
