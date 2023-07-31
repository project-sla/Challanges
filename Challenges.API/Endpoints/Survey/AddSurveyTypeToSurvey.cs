using Challenges.Application.Commands.Survey.AddSurveyTypeToSurvey;
using FastEndpoints;

namespace Challenges.API.Endpoints.Survey;

public class AddSurveyTypeToSurvey : Endpoint<AddSurveyTypeToSurveyCommand,AddSurveyTypeToSurveyResponse>
{
    public override void Configure()
    {
        Post("survey/addSurveyType");
        AllowAnonymous();
    }

    public override async Task HandleAsync(AddSurveyTypeToSurveyCommand req, CancellationToken ct)
    {
         var survey = await new AddSurveyTypeToSurveyCommand(req.SurveyId, req.SurveyTypeId).ExecuteAsync(ct: ct);
            await SendAsync(survey, cancellation: ct);
    }
}