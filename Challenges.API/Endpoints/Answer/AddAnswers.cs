using Challenges.Application.Commands.Answer.AddAnswers;
using FastEndpoints;

namespace Challenges.API.Endpoints.Answer;

public class AddAnswers : Endpoint<AddAnswersCommand,AddAnswerResponse>
{
    public override void Configure()
    {
        Post("answer/add");
        AllowAnonymous();
    }

    public override async Task HandleAsync(AddAnswersCommand req, CancellationToken ct)
    {
        var answer = await new AddAnswersCommand(req.Answers).ExecuteAsync(ct: ct);
        await SendAsync(answer, cancellation: ct);
    }
}