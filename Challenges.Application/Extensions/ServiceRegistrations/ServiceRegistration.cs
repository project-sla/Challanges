﻿using Challenges.Application.Business;
using Microsoft.Extensions.DependencyInjection;

namespace Challenges.Application.Extensions.ServiceRegistrations;

public static class ServiceRegistration
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<GenreBusinessRules>();
        services.AddScoped<TagBusinessRules>();
        services.AddScoped<SurveyTypeBusinessRules>();
    }
}