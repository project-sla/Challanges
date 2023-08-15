using Challenges.Application.Commands.SurveyType.GetSurveyType;
using Challenges.Application.Mappings.SurveyType;
using FastEndpoints;

namespace Challenges.API.Endpoints.SurveyType;

public class GetSurveyType : Endpoint<GetSurveyTypeCommand, GetSurveyTypeResponse, GetSurveyTypeMapper>
{
    public override void Configure()
    {
        Post("surveyType/get");
        AllowAnonymous();
        Validator<GetSurveyTypeValidator>();
    }

    public override async Task HandleAsync(GetSurveyTypeCommand req, CancellationToken ct)
    {
        var surveyType = await new GetSurveyTypeCommand(
            req.Id,
            req.Value
        ).ExecuteAsync(ct);
        await SendAsync(GetSurveyTypeMapper.ToResponseEntity(surveyType), cancellation: ct);
    }
}