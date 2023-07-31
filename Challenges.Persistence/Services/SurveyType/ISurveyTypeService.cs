namespace Challenges.Persistence.Services.SurveyType;

public interface ISurveyTypeService
{
    Task<Domain.Entities.Survey.SurveyType> CreateSurveyTypeAsync(Domain.Entities.Survey.SurveyType surveyTypeData);
    Task<Domain.Entities.Survey.SurveyType?> GetSurveyTypeAsync(Guid surveyTypeId);
    Task<Domain.Entities.Survey.SurveyType?> GetSurveyTypeAsync(string value);
    Task<Domain.Entities.Survey.SurveyType> UpdateSurveyTypeAsync(Domain.Entities.Survey.SurveyType surveyTypeData);
    Task DeleteSurveyTypeAsync(Guid surveyTypeId);
    Task<IEnumerable<Domain.Entities.Survey.SurveyType>> GetAllSurveyTypesAsync(int page, int pageSize);
}