using Challenges.Application.Commands.Common.Answer;

namespace Challenges.Application.Commands.Common.Question;

public record QuestionData(
    Guid? Id,
    Guid? QuestionTypeId,
    string? Value,
    Guid? CreatedBy,
    int? Order,
    List<AnswerData>? Answers
);