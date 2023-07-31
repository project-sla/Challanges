namespace Challenges.Application.Commands.Common.Survey;

public record SurveyTypeData(
    Guid? Id, 
    string Value, 
    Guid CreatedBy
);