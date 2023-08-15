using Challenges.Application.Extensions.ServiceRegistrations;
using Challenges.Persistence.Context;
using Challenges.Persistence.Extensions;
using FastEndpoints;
using FastEndpoints.Security;
using FastEndpoints.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
// Fetch RSA keys
var rsaKeySection = builder.Configuration.GetSection("RsaKey");
//var publicKey = rsaKeySection["PublicKey"];
var privateKey = rsaKeySection["PrivateKey"];
services.AddPersistenceServices(builder.Configuration);
services.AddDbContext<ChallengeDbContext>();
services.AddApplicationServices();
services.AddFastEndpoints();
builder.Services.AddJWTBearerAuth(privateKey ?? throw new InvalidOperationException());
services.SwaggerDocument(o =>
{
    o.DocumentSettings = s =>
    {
        s.Title = "Challenges Service";
        s.Version = "v1";
    };
});
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

builder.Services.AddCors(p => p.AddPolicy("corspolicy",
    build =>
    {
        build.WithOrigins("*")
            .AllowAnyMethod()
            .AllowAnyHeader();
    }));
var app = builder.Build();
app.UseCors("corspolicy");
app.UseResponseCaching();
app.UseAuthentication();
app.UseAuthorization();
app.UseFastEndpoints(c => { c.Endpoints.RoutePrefix = "api"; });
app.UseSwaggerGen();
app.Run();