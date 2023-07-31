using FastEndpoints;

namespace Challenges.Application.Commands.SurveyType.GetAllSurveyType;

public record GetAllSurveyTypeCommand(
        int PageNumber,
        int PageSize
    ) : ICommand<GetAllSurveyTypeResponse>;