using Challenges.Application.Commands.Answer.GetAnswers;
using Challenges.Application.Commands.Common;
using Challenges.Domain.Entities.Question;
using Challenges.Persistence.Services.Answer;
using FastEndpoints;

namespace Challenges.Application.Handlers.Answer;

public class GetAnswersHandler : ICommandHandler<GetAnswersCommand, GetAnswersResponse>
{
    private readonly IAnswerService _answerService;

    public GetAnswersHandler(IAnswerService answerService)
    {
        _answerService = answerService;
    }

    public async Task<GetAnswersResponse> ExecuteAsync(GetAnswersCommand command, CancellationToken ct)
    {
        IEnumerable<QuestionAnswer?> answers = new List<QuestionAnswer?>();
        if (command.AnswerId is not null) answers.Append(await _answerService.GetAnswerById(command.AnswerId.Value));
        if (command.QuestionId is not null)
            answers = await _answerService.GetAnswersByQuestionId(command.QuestionId.Value);
        if (command.AnswerId is null && command.QuestionId is null)
            return new GetAnswersResponse(new Result(false, null, null, 400,
                "QuestionId or AnswerId must be provided"));
        if (command.CreatedBy is not null)
            answers = await _answerService.GetAnswersByCreatedBy(command.CreatedBy.Value);
        return new GetAnswersResponse(new Result(true, null, answers, 200, "Answers retrieved"));
    }
}