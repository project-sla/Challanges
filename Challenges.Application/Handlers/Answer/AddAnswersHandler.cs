using Challenges.Application.Commands.Answer.AddAnswers;
using Challenges.Application.Commands.Common;
using Challenges.Domain.Entities.Question;
using Challenges.Persistence.Services.Answer;
using Challenges.Persistence.Services.Question;
using FastEndpoints;

namespace Challenges.Application.Handlers.Answer;

public class AddAnswersHandler : ICommandHandler<AddAnswersCommand,AddAnswerResponse>
{
    private readonly IAnswerService _answerService;
    private readonly IQuestionService _questionService;
    public AddAnswersHandler(IAnswerService answerService, IQuestionService questionService)
    {
        _answerService = answerService;
        _questionService = questionService;
    }

    public async Task<AddAnswerResponse> ExecuteAsync(AddAnswersCommand command, CancellationToken ct)
    {
        var questionAnswerList = new List<QuestionAnswer>();
        foreach (var answer in command.Answers.Where(answer => answer.QuestionId is not null))
        {
            if (answer.QuestionId == null) continue;
            var question = await _questionService.GetAsync(answer.QuestionId.Value);
            if (question is null)
            {
                return new AddAnswerResponse(new Result(false, null, null, 400, "Question not found"));
            }
            if (answer.Value is null) continue;
            if (answer.CreatedBy is not null)
                questionAnswerList.Add(new QuestionAnswer(question, answer.Value, answer.Order,
                    answer.CreatedBy.Value));
        }
        await _answerService.AddAnswers(questionAnswerList);
        return new AddAnswerResponse(new Result(true, null, null, 200, "Answers added"));
    }
}