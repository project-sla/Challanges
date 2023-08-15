using Challenges.Application.Commands.PrepareQuestions;
using FastEndpoints;

namespace Challenges.API.Endpoints.PrepareQuestions;

public class PrepareQuestions : Endpoint<PrepareQuestionsCommand, PrepareQuestionsCommandResponse>
{
    public override void Configure()
    {
        Post("prepare-questions");
        AllowAnonymous();
        Summary(s =>
        {
            s.Summary = "Prepare questions";
            s.Description = "Prepare questions with the parameters below.";
        });
    }

    public override async Task HandleAsync(PrepareQuestionsCommand req, CancellationToken ct)
    {
        var questions = await new PrepareQuestionsCommand(req.Survey).ExecuteAsync(ct);
        await SendAsync(questions, cancellation: ct);
    }
}