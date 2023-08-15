using FastEndpoints;

namespace Challenges.Application.Commands.PrepareQuestions;

public record PrepareQuestionsCommand(
    SurveyDto Survey
) : ICommand<PrepareQuestionsCommandResponse>;

public record SurveyDto(
    string Content,
    Guid CreatedBy,
    Guid ReceivedBy,
    Guid SurveyTypeId,
    List<QuestionDto> Questions
);

public record QuestionDto(
    string Content,
    Guid QuestionTypeId,
    List<AnswerDto> Answers
);

public record AnswerDto(
    string Content,
    int Order,
    bool IsCorrect
);