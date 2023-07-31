using Challenges.Application.Commands.SurveyType.GetAllSurveyType;
using Challenges.Application.Mappings.SurveyType;
using FastEndpoints;

namespace Challenges.API.Endpoints.SurveyType;

public class GetAllSurveyType : Endpoint<GetAllSurveyTypeCommand,GetAllSurveyTypeResponse,GetAllSurveyTypeMapper>
{
    public override void Configure()
    {
        Post("surveyType/getAll");
        AllowAnonymous();
        Validator<GetAllSurveyTypeValidator>();
    }

    public override async Task HandleAsync(GetAllSurveyTypeCommand req, CancellationToken ct)
    {
        var surveyType = await new GetAllSurveyTypeCommand(
            req.PageNumber,
            req.PageSize
        ).ExecuteAsync(ct);
        await SendAsync(GetAllSurveyTypeMapper.ToResponseEntity(surveyType), cancellation: ct);
    }
}