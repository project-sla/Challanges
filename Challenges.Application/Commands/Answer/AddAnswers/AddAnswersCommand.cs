using Challenges.Application.Commands.Common.Answer;
using FastEndpoints;

namespace Challenges.Application.Commands.Answer.AddAnswers;

public record AddAnswersCommand(
    List<AnswerData> Answers
) : ICommand<AddAnswerResponse>;