using Challenges.Application.Commands.Common.Survey;
using FastEndpoints;

namespace Challenges.Application.Commands.Survey.CreateSurvey;

public record CreateSurveyCommand(
    SurveyData SurveyData
) : ICommand<CreateSurveyResponse>;