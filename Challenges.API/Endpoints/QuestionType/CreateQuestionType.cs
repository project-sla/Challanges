using Challenges.Application.Commands.QuestionType.CreateQuestionType;
using FastEndpoints;

namespace Challenges.API.Endpoints.QuestionType;

public class CreateQuestionType : Endpoint<CreateQuestionTypeCommand,CreateQuestionTypeResponse>
{
    public override void Configure()
    {
        Post("question-type/create");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateQuestionTypeCommand req, CancellationToken ct)
    {
        var questionType = await new CreateQuestionTypeCommand(
            req.Value
        ).ExecuteAsync(ct);
        await SendAsync(questionType, cancellation: ct);
    }
}