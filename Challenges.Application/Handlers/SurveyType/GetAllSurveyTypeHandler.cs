using Challenges.Application.Commands.Common;
using Challenges.Application.Commands.SurveyType.GetAllSurveyType;
using Challenges.Persistence.Services.SurveyType;
using FastEndpoints;

namespace Challenges.Application.Handlers.SurveyType;

public class GetAllSurveyTypeHandler : ICommandHandler<GetAllSurveyTypeCommand, GetAllSurveyTypeResponse>
{
    private readonly ISurveyTypeService _surveyTypeService;

    public GetAllSurveyTypeHandler(ISurveyTypeService surveyTypeService)
    {
        _surveyTypeService = surveyTypeService;
    }

    public async Task<GetAllSurveyTypeResponse> ExecuteAsync(GetAllSurveyTypeCommand command, CancellationToken ct)
    {
        var surveyTypes = await _surveyTypeService.GetAllSurveyTypesAsync(command.PageNumber, command.PageSize);
        return new GetAllSurveyTypeResponse(new Result(true, "SurveyType found", surveyTypes, 200, "SurveyType found"));
    }
}