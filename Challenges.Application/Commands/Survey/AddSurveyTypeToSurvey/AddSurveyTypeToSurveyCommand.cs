using FastEndpoints;

namespace Challenges.Application.Commands.Survey.AddSurveyTypeToSurvey;

public record AddSurveyTypeToSurveyCommand(
        Guid SurveyId,
        Guid SurveyTypeId
    ) : ICommand<AddSurveyTypeToSurveyResponse>;