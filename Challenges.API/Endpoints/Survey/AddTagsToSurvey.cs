using Challenges.Application.Commands.Survey.AddTagsToSurvey;
using FastEndpoints;

namespace Challenges.API.Endpoints.Survey;

public class AddTagsToSurvey : Endpoint<AddTagsToSurveyCommand, AddTagsToSurveyResponse>
{
    public override void Configure()
    {
        Post("survey/addTags");
        AllowAnonymous();
    }

    public override async Task HandleAsync(AddTagsToSurveyCommand req, CancellationToken ct)
    {
        var survey = await new AddTagsToSurveyCommand(req.SurveyId, req.TagIds).ExecuteAsync(ct);
        await SendAsync(survey, cancellation: ct);
    }
}