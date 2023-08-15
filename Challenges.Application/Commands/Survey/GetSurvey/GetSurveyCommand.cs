using FastEndpoints;

namespace Challenges.Application.Commands.Survey.GetSurvey;

public record GetSurveyCommand(
    Guid? SurveyId
    ) :ICommand<GetSurveyResponse>;