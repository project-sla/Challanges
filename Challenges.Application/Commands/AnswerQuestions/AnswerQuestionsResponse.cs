using Challenges.Application.Commands.Common;
using Challenges.Application.Commands.Common.Answer;
using Challenges.Application.Commands.Common.Question;

namespace Challenges.Application.Commands.AnswerQuestions;

public record AnswerQuestionsResponse(
    Result Result
);

public record AnswerResultData(
    QuestionData Question
);