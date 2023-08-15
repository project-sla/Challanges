using Challenges.Application.Commands.Survey.AddQuestionsToSurvey;
using FastEndpoints;

namespace Challenges.API.Endpoints.Survey;

public class AddQuestionsToSurvey : Endpoint<AddQuestionsToSurveyCommand, AddQuestionsToSurveyResponse>
{
    public override void Configure()
    {
        Post("survey/addQuestions");
        AllowAnonymous();
    }

    public override async Task HandleAsync(AddQuestionsToSurveyCommand req, CancellationToken ct)
    {
        var survey = await new AddQuestionsToSurveyCommand(req.SurveyId, req.Questions).ExecuteAsync(ct);
        await SendAsync(survey, cancellation: ct);
    }
}