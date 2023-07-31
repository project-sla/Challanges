using Challenges.Application.Business;
using Challenges.Application.Commands.Common;
using Challenges.Application.Commands.SurveyType.CreateSurveyType;
using Challenges.Persistence.Services.SurveyType;
using FastEndpoints;

namespace Challenges.Application.Handlers.SurveyType;

public class CreateSurveyTypeHandler : ICommandHandler<CreateSurveyTypeCommand,CreateSurveyTypeResponse>
{
    private readonly ISurveyTypeService _surveyTypeService;
    private readonly SurveyTypeBusinessRules _surveyTypeBusinessRules;
    public CreateSurveyTypeHandler(ISurveyTypeService surveyTypeService, SurveyTypeBusinessRules surveyTypeBusinessRules)
    {
        _surveyTypeService = surveyTypeService;
        _surveyTypeBusinessRules = surveyTypeBusinessRules;
    }

    public async Task<CreateSurveyTypeResponse> ExecuteAsync(CreateSurveyTypeCommand command, CancellationToken ct)
    {
        var message = new List<string>();
        var surveyTypeExists = await _surveyTypeBusinessRules.SurveyTypeExistsAsync(command.SurveyTypeData.Value);
        if (surveyTypeExists)
        {
            message.Add($"SurveyType {command.SurveyTypeData.Value} already exists");
            return new CreateSurveyTypeResponse(new Result(false,"SurveyType already exists",command.SurveyTypeData.Value,409,"Conflict"));
        }
        var surveyType = new Domain.Entities.Survey.SurveyType(command.SurveyTypeData.Value, command.SurveyTypeData.CreatedBy);
        await _surveyTypeService.CreateSurveyTypeAsync(surveyType);
        return new CreateSurveyTypeResponse(new Result(true,"SurveyType created",command.SurveyTypeData.Value,201,"Created"));
    }
}