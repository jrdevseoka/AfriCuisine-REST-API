using Africuisine.Application;
using Africuisine.Application.Config;
using Africuisine.Infrastructure;
using Africuisine.Infrastructure.Helpers;
using NLog;

var Logger = LogManager.Setup().LoadConfigurationFromFile("NLog.config").GetCurrentClassLogger();
try
{
    var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers( opts => {
    opts.Filters.Add<NlogFolderFilter>();
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.RegisterSwaggerGeneration();
//Custom Service Injections
Database database = builder.Configuration.GetSection("ConnectionStrings:AfricuisineConnection").Get<Database>();
JWTBearer jwtOptions = builder.Configuration.GetSection("JWT").Get<JWTBearer>();
builder.Services.RegisterApplicationInjections();
builder.Services.APIVersionInjection();
builder.Services.RegisterOptionsConfigurations(builder.Configuration);
builder.Services.RegisterIdentity();
builder.Services.RegisterAuthInjections(jwtOptions);
builder.Services.RegisterDBContext(database);
builder.Services.RegisterServiceInjection();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI( opts => {
        var descriptions = app.DescribeApiVersions();
            foreach (var description in descriptions)
            {
                var url = $"/swagger/{description.GroupName}/swagger.json";
                var name = description.GroupName.ToUpperInvariant();
                opts.SwaggerEndpoint(url, name);
            }
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
}
catch(Exception exception)
{
    Logger.Error(exception);
}
finally
{
    LogManager.Shutdown();
}