using Challenges.Application.Commands.SurveyType.GetAllSurveyType;
using FastEndpoints;

namespace Challenges.Application.Mappings.SurveyType;

public class GetAllSurveyTypeMapper : IMapper
{
    public static GetAllSurveyTypeResponse ToResponseEntity(GetAllSurveyTypeResponse surveyType)
    {
        return surveyType;
    }
}