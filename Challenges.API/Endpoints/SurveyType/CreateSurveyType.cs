using Challenges.Application.Commands.SurveyType.CreateSurveyType;
using FastEndpoints;

namespace Challenges.API.Endpoints.SurveyType;

public class CreateSurveyType : Endpoint<CreateSurveyTypeCommand,CreateSurveyTypeResponse>
{
    public override void Configure()
    {
        Post("surveyType/create");
        AllowAnonymous();
        Validator<CreateSurveyTypeValidator>();
    }

    public override async Task HandleAsync(CreateSurveyTypeCommand req, CancellationToken ct)
    {
        var surveyType = await new CreateSurveyTypeCommand(
            req.SurveyTypeData
        ).ExecuteAsync(ct);
        await SendAsync(surveyType, cancellation: ct);
    }
}