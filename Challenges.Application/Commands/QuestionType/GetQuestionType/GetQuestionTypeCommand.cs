using FastEndpoints;

namespace Challenges.Application.Commands.QuestionType.GetQuestionType;

public record GetQuestionTypeCommand(
    int? PageNumber,
    int? PageSize,
    string? SearchTerm = null, 
    Guid? Id = null) : ICommand<GetQuestionTypeResponse>;