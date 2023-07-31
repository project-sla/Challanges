using Challenges.Application.Commands.Common.Question;
using FastEndpoints;

namespace Challenges.Application.Commands.Question.CreateQuestions;

public record CreateQuestionsCommand(
    List<QuestionData> Questions
) : ICommand<CreateQuestionsResponse>;