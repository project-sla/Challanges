using FastEndpoints;

namespace Challenges.Application.Commands.Survey.AddTagsToSurvey;

public record AddTagsToSurveyCommand(
    Guid SurveyId,
    IEnumerable<Guid> TagIds
) : ICommand<AddTagsToSurveyResponse>;