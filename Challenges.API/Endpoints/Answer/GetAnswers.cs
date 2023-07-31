using Challenges.Application.Commands.Answer.GetAnswers;
using FastEndpoints;

namespace Challenges.API.Endpoints.Answer;

public class GetAnswers : Endpoint<GetAnswersCommand,GetAnswersResponse>
{
    public override void Configure()
    {
        Post("answer/get");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetAnswersCommand req, CancellationToken ct)
    {
        var answer = await new GetAnswersCommand(req.QuestionId, req.AnswerId, req.CreatedBy).ExecuteAsync(ct: ct);
        await SendAsync(answer, cancellation: ct);
    }
}