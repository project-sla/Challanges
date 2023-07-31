using Challenges.Application.Commands.Answer.GetAnswers;
using FastEndpoints;

namespace Challenges.Application.Commands.Question.GetQuestions;

public record GetQuestionsCommand(
    int? Page,
    int? PageSize,
    Guid? SurveyId = null,
    Guid? CreatedBy = null,
    Guid? QuestionTypeId = null, Guid? QuestionId = null) : ICommand<GetQuestionsResponse>;