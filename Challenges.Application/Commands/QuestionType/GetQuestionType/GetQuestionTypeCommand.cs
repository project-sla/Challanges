using FastEndpoints;

namespace Challenges.Application.Commands.QuestionType.GetQuestionType;

public record GetQuestionTypeCommand(
    Guid? Id,
    string? SearchTerm,
    int? PageNumber,
    int? PageSize
) : ICommand<GetQuestionTypeResponse>;