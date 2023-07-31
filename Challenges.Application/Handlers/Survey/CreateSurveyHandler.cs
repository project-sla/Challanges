using Challenges.Application.Commands.Common;
using Challenges.Application.Commands.Survey.CreateSurvey;
using Challenges.Persistence.Services.Survey;
using Challenges.Persistence.Services.SurveyType;
using FastEndpoints;

namespace Challenges.Application.Handlers.Survey;

public class CreateSurveyHandler : ICommandHandler<CreateSurveyCommand,CreateSurveyResponse>
{
    private readonly ISurveyService _surveyService;
    private readonly ISurveyTypeService _surveyTypeService;
    public CreateSurveyHandler(ISurveyService surveyService, ISurveyTypeService surveyTypeService)
    {
        _surveyService = surveyService;
        _surveyTypeService = surveyTypeService;
    }

    public async Task<CreateSurveyResponse> ExecuteAsync(CreateSurveyCommand command, CancellationToken ct)
    {
        if (command.SurveyData.SurveyType.Id == null)
            return new CreateSurveyResponse(new Result(false, null, null, 400, "Survey type id cannot be null"));
        var surveyType = await _surveyTypeService.GetSurveyTypeAsync(command.SurveyData.SurveyType.Id.Value);
        if (surveyType == null)
            return new CreateSurveyResponse(new Result(false, null, null, 400, "Survey type not found"));
        var result = await _surveyService.CreateAsync(surveyType, command.SurveyData.Content, command.SurveyData.CreatedBy!.Value);
        return result == null ? new CreateSurveyResponse(new Result(false, null, null, 400, "Survey could not be created")) : new CreateSurveyResponse(new Result(true,null,result,200,"Survey created successfully"));
    }
}