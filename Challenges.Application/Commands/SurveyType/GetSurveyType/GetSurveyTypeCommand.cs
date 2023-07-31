using FastEndpoints;

namespace Challenges.Application.Commands.SurveyType.GetSurveyType;

public record GetSurveyTypeCommand(
    Guid? Id = null,
    string? Value = null
    ) : ICommand<GetSurveyTypeResponse>;