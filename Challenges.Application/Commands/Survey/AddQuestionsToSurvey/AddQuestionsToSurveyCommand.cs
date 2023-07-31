using Challenges.Application.Commands.Common.Question;
using FastEndpoints;

namespace Challenges.Application.Commands.Survey.AddQuestionsToSurvey;

public record AddQuestionsToSurveyCommand(
    Guid? SurveyId,
    List<QuestionData>? Questions
    ) : ICommand<AddQuestionsToSurveyResponse>;