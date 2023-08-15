namespace Challenges.Application.Commands.Common.Survey;

public record SurveyData(
    Guid? Id,
    SurveyTypeData SurveyType,
    string Content,
    Guid? CreatedBy
);