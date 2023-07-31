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
        List<Domain.Entities.Question.QuestionType>? questionTypes;
        if (command.SearchTerm is null)
        {
            questionTypes = await _questionTypeService.GetAllAsync((int)command.PageNumber, (int)command.PageSize);
            return new GetQuestionTypeResponse(new Result(true,null, questionTypes, 200, "QuestionTypes retrieved successfully."));
        }
        questionTypes = await _questionTypeService.GetAllAsync((int)command.PageNumber, (int)command.PageSize, command.SearchTerm);
        return new GetQuestionTypeResponse(new Result(true,null, questionTypes, 200, "QuestionTypes retrieved successfully."));
    }
}