using Challenges.Application.Commands.QuestionType.GetQuestionType;
using FastEndpoints;

namespace Challenges.API.Endpoints.QuestionType;

public class GetQuestionType : Endpoint<GetQuestionTypeCommand,GetQuestionTypeResponse>
{
    public override void Configure()
    {
        Post("question-type/get");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetQuestionTypeCommand req, CancellationToken ct)
    {
        var questionType = await new GetQuestionTypeCommand(
            req.Id,
            req.SearchTerm,
            req.PageNumber,
            req.PageSize
        ).ExecuteAsync(ct);
        await SendAsync(questionType, cancellation: ct);
    }
}
