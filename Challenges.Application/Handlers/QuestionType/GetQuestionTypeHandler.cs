using Challenges.Application.Commands.Common;
using Challenges.Application.Commands.QuestionType.GetQuestionType;
using Challenges.Persistence.Services.QuestionType;
using FastEndpoints;

namespace Challenges.Application.Handlers.QuestionType;

public class GetQuestionTypeHandler : ICommandHandler<GetQuestionTypeCommand,GetQuestionTypeResponse>
{
    private readonly IQuestionTypeService _questionTypeService;

    public GetQuestionTypeHandler(IQuestionTypeService questionTypeService)
    {
        _questionTypeService = questionTypeService;
    }

    public async Task<GetQuestionTypeResponse> ExecuteAsync(GetQuestionTypeCommand command, CancellationToken ct)
    {
        Domain.Entities.Question.QuestionType? questionType;
        if (command.Id is not null)
        {
            questionType = await _questionTypeService.GetAsync(command.Id.Value);
            return questionType is null ? new GetQuestionTypeResponse(new Result(false, null, null, 404, "Question type not found")) : new GetQuestionTypeResponse(new Result(true, null, questionType, 200, "Question type retrieved"));
        }
        if (command.SearchTerm is not null)
        {
            var questionTypes = await _questionTypeService.GetAllAsync( (int)command.PageNumber, (int)command.PageSize,command.SearchTerm);
            return new GetQuestionTypeResponse(new Result(true, null, questionTypes, 200, "Question types retrieved"));
        }
        questionType = await _questionTypeService.GetAsync(command.Id!.Value);
        if (command.Id is not null || command.SearchTerm is not null)
            return questionType is null
                ? new GetQuestionTypeResponse(new Result(false, null, null, 404, "Question type not found"))
                : new GetQuestionTypeResponse(new Result(true, null, questionType, 200, "Question type retrieved"));
        {
            var questionTypes = await _questionTypeService.GetAllAsync( (int)command.PageNumber, (int)command.PageSize);
            return new GetQuestionTypeResponse(new Result(true, null, questionTypes, 200, "Question types retrieved"));
        }
    }
}