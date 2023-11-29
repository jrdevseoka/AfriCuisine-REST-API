using Africuisine.Application.Config;
using Africuisine.Infrastructure;
using Africuisine.Infrastructure.Helpers;
using Microsoft.AspNetCore.Mvc;
[assembly: ApiController]
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers( opts => {
    opts.Filters.Add<NlogFolderFilter>();
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//Custom Service Injections
Database database = builder.Configuration.GetSection("ConnectionStrings:AfricuisineConnection").Get<Database>();
JWTBearer jwtOptions = builder.Configuration.GetSection("JWT").Get<JWTBearer>();
builder.Services.RegisterAuthInjections(jwtOptions);
builder.Services.RegisterOptionsConfigurations(builder.Configuration);
builder.Services.RegisterIdentity();
builder.Services.RegisterAuthInjections(jwtOptions);
builder.Services.RegisterDBContext(database);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
