using FastEndpoints;

namespace Challenges.Application.Commands.Survey.AddGenresToSurvey;

public record AddGenresToSurveyCommand(
    Guid SurveyId,
    List<Guid> GenreIds
) : ICommand<AddGenresToSurveyResponse>;