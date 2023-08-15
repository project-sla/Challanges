using Challenges.Persistence.Context;
using Challenges.Persistence.Services.Answer;
using Challenges.Persistence.Services.ChallangeRequest;
using Challenges.Persistence.Services.Genre;
using Challenges.Persistence.Services.Question;
using Challenges.Persistence.Services.QuestionAnswer;
using Challenges.Persistence.Services.QuestionType;
using Challenges.Persistence.Services.Survey;
using Challenges.Persistence.Services.SurveyType;
using Challenges.Persistence.Services.Tags;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Challenges.Persistence.Extensions;

public static class ServiceRegistration
{
    public static void AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IGenreService, GenreService>();
        services.AddScoped<ITagService, TagService>();
        services.AddScoped<IQuestionAnswerService, QuestionAnswerService>();
        services.AddScoped<ISurveyService, SurveyService>();
        services.AddScoped<ISurveyTypeService, SurveyTypeService>();
        services.AddScoped<IQuestionTypeService, QuestionTypeService>();
        services.AddScoped<IQuestionService, QuestionService>();
        services.AddScoped<IAnswerService, AnswerService>();
        services.AddScoped<IChallengeRequestService, ChallengeRequestService>();

        services.AddDbContext<ChallengeDbContext>(
            options => options.UseNpgsql(configuration.GetSection("ConnectionStrings:DefaultConnection").Value)
        );
    }
}