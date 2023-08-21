using Challenges.Application.Commands.Common;

namespace Challenges.Application.Commands.PrepareQuestions;

public record PrepareQuestionsCommandResponse(
    Result Result,
    SurveyResponse? Survey
);

public record SurveyResponse(
    Guid Id,
    string? Content,
    Guid CreatedBy,
    Guid SurveyTypeId,
    double Time,
    int TrueQuestionsToWin,
    List<QuestionResponse> Questions
);

public record QuestionResponse(
    Guid Id,
    string? Content,
    Guid QuestionTypeId,
    List<AnswerResponse> Answers
);

public record AnswerResponse(
    Guid Id,
    string? Content,
    int Order,
    bool IsCorrect
);