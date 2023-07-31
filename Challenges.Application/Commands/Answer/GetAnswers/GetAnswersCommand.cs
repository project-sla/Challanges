using FastEndpoints;

namespace Challenges.Application.Commands.Answer.GetAnswers;

public record GetAnswersCommand(
            Guid? QuestionId = null,
            Guid? AnswerId = null,
            Guid? CreatedBy = null
) : ICommand<GetAnswersResponse>;