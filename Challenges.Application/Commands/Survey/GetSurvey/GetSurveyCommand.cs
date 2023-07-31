using FastEndpoints;

namespace Challenges.Application.Commands.Survey.GetSurvey;

public record GetSurveyCommand(
        Guid? SurveyId,
        Guid? UserId,
        Guid? QuestionId = null,
        Guid? AnswerId = null,
        bool? IncludeQuestions = null,
        bool? IncludeTags = null,
        bool? IncludeGenres = null,
        string? Search = null,
        int? Page = null,
        int? PageSize = null
    ) :ICommand<GetSurveyResponse>;