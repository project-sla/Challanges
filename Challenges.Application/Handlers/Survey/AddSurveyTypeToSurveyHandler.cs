using Challenges.Application.Commands.Common;
using Challenges.Application.Commands.Survey.AddSurveyTypeToSurvey;
using Challenges.Persistence.Services.Survey;
using Challenges.Persistence.Services.SurveyType;
using FastEndpoints;

namespace Challenges.Application.Handlers.Survey;

public class AddSurveyTypeToSurveyHandler : ICommandHandler<AddSurveyTypeToSurveyCommand,AddSurveyTypeToSurveyResponse>
{
    private readonly ISurveyService _surveyService;
    private readonly ISurveyTypeService _surveyTypeService;

    public AddSurveyTypeToSurveyHandler(ISurveyService surveyService, ISurveyTypeService surveyTypeService)
    {
        _surveyService = surveyService;
        _surveyTypeService = surveyTypeService;
    }

    public async Task<AddSurveyTypeToSurveyResponse> ExecuteAsync(AddSurveyTypeToSurveyCommand command, CancellationToken ct)
    {
        var survey = await _surveyService.GetAsync(command.SurveyId);
        if (survey == null)
            return new AddSurveyTypeToSurveyResponse(new Result(false, null, null, 400, "Survey not found"));
        var surveyType = await _surveyTypeService.GetSurveyTypeAsync(command.SurveyTypeId);
        if (surveyType == null)
            return new AddSurveyTypeToSurveyResponse(new Result(false, null, null, 400, "SurveyType not found"));
        await _surveyService.AddSurveyTypeAsync(survey, surveyType);
        return new AddSurveyTypeToSurveyResponse(new Result(true, null, survey, 200, "SurveyType added to survey"));
    }
}