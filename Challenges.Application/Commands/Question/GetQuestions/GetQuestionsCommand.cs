using Challenges.Application.Commands.Answer.GetAnswers;
using FastEndpoints;

namespace Challenges.Application.Commands.Question.GetQuestions;

public record GetQuestionsCommand(
            Guid? QuestionId,
            Guid? CreatedBy,
            Guid? QuestionTypeId,
            Guid? SurveyId,
            int? Page,
            int? PageSize
) : ICommand<GetQuestionsResponse>;