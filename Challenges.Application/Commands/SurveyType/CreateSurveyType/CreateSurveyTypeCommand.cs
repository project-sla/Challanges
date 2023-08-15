using Challenges.Application.Commands.Common.Survey;
using FastEndpoints;

namespace Challenges.Application.Commands.SurveyType.CreateSurveyType;

public record CreateSurveyTypeCommand(
    SurveyTypeData SurveyTypeData
) : ICommand<CreateSurveyTypeResponse>;