using Challenges.Application.Commands.Common;
using Challenges.Application.Commands.Question.CreateQuestions;
using Challenges.Persistence.Services.Question;
using Challenges.Persistence.Services.QuestionType;
using FastEndpoints;

namespace Challenges.Application.Handlers.Question;

public class CreateQuestionHandler : ICommandHandler<CreateQuestionsCommand, CreateQuestionsResponse>
{
    private readonly IQuestionService _questionService;
    private readonly IQuestionTypeService _questionTypeService;

    public CreateQuestionHandler(IQuestionService questionService, IQuestionTypeService questionTypeService)
    {
        _questionService = questionService;
        _questionTypeService = questionTypeService;
    }

    public async Task<CreateQuestionsResponse> ExecuteAsync(CreateQuestionsCommand command, CancellationToken ct)
    {
        foreach (var question in command.Questions)
            if (question.QuestionTypeId != null)
            {
                var questionType = await _questionTypeService.GetAsync(question.QuestionTypeId.Value);
                if (questionType == null)
                    return new CreateQuestionsResponse(new Result(false, null, null, 400, "Question type not found"));
                if (question.Value != null)
                    await _questionService.CreateAsync(value: question.Value, questionType: questionType);
            }
            else
            {
                if (question.Value != null)
                    await _questionService.CreateAsync(question.Value);
            }

        return new CreateQuestionsResponse(new Result(true, null, null, 200, "Questions created"));
    }
}