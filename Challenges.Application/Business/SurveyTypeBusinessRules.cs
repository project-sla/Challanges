using Challenges.Persistence.Services.SurveyType;

namespace Challenges.Application.Business;

public class SurveyTypeBusinessRules
{
    private readonly ISurveyTypeService _surveyTypeService;

    public SurveyTypeBusinessRules(ISurveyTypeService surveyTypeService)
    {
        _surveyTypeService = surveyTypeService;
    }

    public async Task<bool> SurveyTypeExistsAsync(string value)
    {
        var surveyType = await _surveyTypeService.GetSurveyTypeAsync(value);
        if (surveyType is not null) return true;
        return false;
    }
}