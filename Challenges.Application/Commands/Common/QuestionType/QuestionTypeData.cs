namespace Challenges.Application.Commands.Common.QuestionType;

public record QuestionTypeData(
    Guid? Id,
    string? Value,
    Guid? CreatedBy
);