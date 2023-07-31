namespace Challenges.Application.Commands.Common.Answer;

public record AnswerData(
    Guid? Id,
    Guid? QuestionId,
    int Order,
    string? Value,
    Guid? CreatedBy
);