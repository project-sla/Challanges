using Challenges.Application.Commands.Survey.GetSurvey;
using FastEndpoints;

namespace Challenges.API.Endpoints.Survey;

public class GetSurvey : Endpoint<GetSurveyCommand,GetSurveyResponse>
{
    public override void Configure()
    {
        Post("survey/get");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetSurveyCommand req, CancellationToken ct)
    {
        var survey = await new GetSurveyCommand(SurveyId: req.SurveyId, UserId: req.UserId)
            .ExecuteAsync(ct: ct);
        await SendAsync(survey, cancellation: ct);
    }
}