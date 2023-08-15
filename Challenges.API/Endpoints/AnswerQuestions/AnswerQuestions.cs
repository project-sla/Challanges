using Challenges.Application.Commands.AnswerQuestions;
using FastEndpoints;

namespace Challenges.API.Endpoints.AnswerQuestions;

public class AnswerQuestions : Endpoint<AnswerQuestionsCommand, AnswerQuestionsResponse>
{
    public override void Configure()
    {
        Post("/answer-questions");
        AllowAnonymous();
    }

    public override async Task HandleAsync(AnswerQuestionsCommand req, CancellationToken ct)
    {
        var response = await new AnswerQuestionsCommand(req.Survey).ExecuteAsync(ct);
        await SendAsync(response, cancellation: ct);
    }
}