using FastEndpoints;

namespace Challenges.Application.Commands.Answer.GetAnswers;

public record GetAnswersCommand(
            Guid? QuestionId,
            Guid? AnswerId,
            Guid? CreatedBy
) : ICommand<GetAnswersResponse>;