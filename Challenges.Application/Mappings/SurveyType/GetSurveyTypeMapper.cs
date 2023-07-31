using Challenges.Application.Commands.SurveyType.GetSurveyType;
using FastEndpoints;

namespace Challenges.Application.Mappings.SurveyType;

public class GetSurveyTypeMapper : IMapper
{
    public static GetSurveyTypeResponse ToResponseEntity(GetSurveyTypeResponse surveyType)
    {
        return surveyType;
    }
}