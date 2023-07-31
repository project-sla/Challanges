using Challenges.Application.Commands.Common;
using Challenges.Application.Commands.Survey.GetSurvey;
using Challenges.Persistence.Services.Survey;
using FastEndpoints;

namespace Challenges.Application.Handlers.Survey;

public class GetAllSurveyHandler : ICommandHandler<GetSurveyCommand,GetSurveyResponse>
{
    private readonly ISurveyService _surveyService;

    public GetAllSurveyHandler(ISurveyService surveyService)
    {
        _surveyService = surveyService;
    }

    public async Task<GetSurveyResponse> ExecuteAsync(GetSurveyCommand command, CancellationToken ct)
    {
        List<Domain.Entities.Survey.Survey>? survey;
        if (command.Search is not null)
        {
            survey = await _surveyService.GetAllAsync(
                skip:(int)command.Page,
                take:(int)command.PageSize,
                search:command.Search,
                includeQuestions:command.IncludeQuestions!.Value,
                includeTags:command.IncludeTags!.Value,
                includeGenres:command.IncludeGenres!.Value);
            return survey.Count == 0  ? new GetSurveyResponse(new Result(false, null, null, 400, "Survey not found")) : new GetSurveyResponse(new Result(true, null, survey, 200, $"{survey.Count} Survey found"));
        }
        survey = await _surveyService.GetAllAsync(
            (int)command.Page,
            (int)command.PageSize,
            command.IncludeQuestions!.Value,
            command.IncludeTags!.Value,
            command.IncludeGenres!.Value);
        return survey.Count == 0  ? new GetSurveyResponse(new Result(false, null, null, 400, "Survey not found")) : new GetSurveyResponse(new Result(true, null, survey, 200, $"{survey.Count} Survey found"));
    }
}