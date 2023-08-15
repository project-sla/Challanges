using Challenges.Application.Commands.Common;
using Challenges.Application.Commands.SurveyType.GetSurveyType;
using Challenges.Persistence.Services.SurveyType;
using FastEndpoints;

namespace Challenges.Application.Handlers.SurveyType;

public class GetSurveyTypeHandler : ICommandHandler<GetSurveyTypeCommand, GetSurveyTypeResponse>
{
    private readonly ISurveyTypeService _surveyTypeService;

    public GetSurveyTypeHandler(ISurveyTypeService surveyTypeService)
    {
        _surveyTypeService = surveyTypeService;
    }

    public async Task<GetSurveyTypeResponse> ExecuteAsync(GetSurveyTypeCommand command, CancellationToken ct)
    {
        if (command.Id is not null)
        {
            var surveyType = await _surveyTypeService.GetSurveyTypeAsync(command.Id.Value);
            return surveyType is null
                ? new GetSurveyTypeResponse(
                    new Result(false, "SurveyType not found", null, 404, "SurveyType not found"))
                : new GetSurveyTypeResponse(new Result(true, "SurveyType found", surveyType, 200, "SurveyType found"));
        }

        if (command.Value is null)
            return new GetSurveyTypeResponse(new Result(false, "Id or Value must be provided", null, 400,
                "Id or Value must be provided"));
        {
            var surveyType = await _surveyTypeService.GetSurveyTypeAsync(command.Value);
            return surveyType is null
                ? new GetSurveyTypeResponse(
                    new Result(false, "SurveyType not found", null, 404, "SurveyType not found"))
                : new GetSurveyTypeResponse(new Result(true, "SurveyType found", surveyType, 200, "SurveyType found"));
        }
    }
}