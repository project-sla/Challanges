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
        var survey = await _surveyService.GetAllAsync(command.Page!.Value, command.PageSize!.Value, command.Search,command.IncludeQuestions!.Value, command.IncludeTags!.Value, command.IncludeGenres!.Value);
        return survey.Count == 0  ? new GetSurveyResponse(new Result(false, null, null, 400, "Survey not found")) : new GetSurveyResponse(new Result(true, null, survey, 200, $"{survey.Count} Survey found"));
    }
}