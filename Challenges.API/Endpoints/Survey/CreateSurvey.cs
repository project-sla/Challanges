using Challenges.Application.Commands.Survey.CreateSurvey;
using FastEndpoints;

namespace Challenges.API.Endpoints.Survey;

public class CreateSurvey : Endpoint<CreateSurveyCommand,CreateSurveyResponse>
{
    public override void Configure()
    {
        Post("survey/create");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateSurveyCommand req, CancellationToken ct)
    {
        var survey = await new CreateSurveyCommand(req.SurveyData).ExecuteAsync(ct: ct);
        await SendAsync(survey, cancellation: ct);
    }
}