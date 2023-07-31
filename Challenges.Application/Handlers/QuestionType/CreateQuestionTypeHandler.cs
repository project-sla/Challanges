using Challenges.Application.Commands.Common;
using Challenges.Application.Commands.QuestionType.CreateQuestionType;
using Challenges.Persistence.Services.QuestionType;
using FastEndpoints;

namespace Challenges.Application.Handlers.QuestionType;

public class CreateQuestionTypeHandler : ICommandHandler<CreateQuestionTypeCommand, CreateQuestionTypeResponse>
{
    private readonly IQuestionTypeService _questionTypeService;

    public CreateQuestionTypeHandler(IQuestionTypeService questionTypeService)
    {
        _questionTypeService = questionTypeService;
    }

    public async Task<CreateQuestionTypeResponse> ExecuteAsync(CreateQuestionTypeCommand command, CancellationToken ct)
    {
        var questionType = await _questionTypeService.GetAsync(command.Value);
        if (questionType is not null)
            return new CreateQuestionTypeResponse(new Result(false, null, null, 409, "Conflict"));
        questionType = await _questionTypeService.CreateAsync(command.Value);
        return new CreateQuestionTypeResponse(new Result(true, null, questionType, 200, "Question type created"));
    }
}