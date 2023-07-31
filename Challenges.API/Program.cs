using Challenges.Application.Extensions.ServiceRegistrations;
using Challenges.Persistence.Context;
using Challenges.Persistence.Extensions;
using FastEndpoints;
using FastEndpoints.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;


var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
services.AddPersistenceServices(builder.Configuration);
services.AddDbContext<ChallengeDbContext>();
services.AddApplicationServices();
services.AddFastEndpoints();
services.SwaggerDocument(o =>
{
    o.DocumentSettings = s =>
    {
        s.Title = "Challenges Service";
        s.Version = "v1";
    };
});
var app = builder.Build();
app.UseFastEndpoints(c => { c.Endpoints.RoutePrefix = "api"; });
app.UseSwaggerGen();
app.Run();
