using Challenges.Application.Commands.Survey.AddGenresToSurvey;
using FastEndpoints;

namespace Challenges.API.Endpoints.Survey;

public class AddGenresToSurvey : Endpoint<AddGenresToSurveyCommand, AddGenresToSurveyResponse>
{
    public override void Configure()
    {
        Post("survey/addGenres");
        AllowAnonymous();
    }

    public override async Task HandleAsync(AddGenresToSurveyCommand req, CancellationToken ct)
    {
        var survey = await new AddGenresToSurveyCommand(req.SurveyId, req.GenreIds).ExecuteAsync(ct);
        await SendAsync(survey, cancellation: ct);
    }
}