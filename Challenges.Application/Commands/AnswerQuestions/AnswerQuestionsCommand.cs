using Challenges.Application.Commands.Common.Answer;
using FastEndpoints;

namespace Challenges.Application.Commands.AnswerQuestions;

public record AnswerQuestionsCommand(
        SurveyDto Survey
) : ICommand<AnswerQuestionsResponse>;
    
public record SurveyDto(
        Guid Id,
        List<QuestionDto> Questions
    );
public record QuestionDto(
        Guid Id,
        Guid AnswerId
    );