using Challenges.Application.Commands.Question.CreateQuestions;
using FastEndpoints;

namespace Challenges.API.Endpoints.Question;

public class CreateQuestion : Endpoint<CreateQuestionsCommand,CreateQuestionsResponse>
{
    public override void Configure()
    {
        Post("question/create");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateQuestionsCommand req, CancellationToken ct)
    {
        var question = await new CreateQuestionsCommand(req.Questions).ExecuteAsync(ct: ct);
        await SendAsync(question, cancellation: ct);
    }
}