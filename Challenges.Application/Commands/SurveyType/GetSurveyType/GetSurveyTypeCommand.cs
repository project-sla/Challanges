using FastEndpoints;

namespace Challenges.Application.Commands.SurveyType.GetSurveyType;

public record GetSurveyTypeCommand(
    Guid? Id,
    string? Value
    ) : ICommand<GetSurveyTypeResponse>;