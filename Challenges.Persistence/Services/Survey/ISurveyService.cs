using Challenges.Domain.Entities;

namespace Challenges.Persistence.Services.Survey;

public interface ISurveyService
{
    Task<Domain.Entities.Survey.Survey?> UpdateAsync(Domain.Entities.Survey.Survey? survey);
    Task<Domain.Entities.Survey.Survey?> UpdateAsync(Domain.Entities.Survey.Survey? survey,Domain.Entities.Survey.SurveyType surveyType);

    Task<Domain.Entities.Survey.Survey> CreateAsync(Domain.Entities.Survey.Survey survey);
    Task<Domain.Entities.Survey.Survey?> CreateAsync(Domain.Entities.Survey.SurveyType surveyType,string value,Guid createdBy);
    Task<Domain.Entities.Survey.Survey?> CreateAsync(Domain.Entities.Survey.SurveyType surveyType,string value);
    Task<Domain.Entities.Survey.Survey?> CreateAsync(string value);
    Task<Domain.Entities.Survey.Survey?> GetAsync(Guid id);
    Task<List<Challenges.Domain.Entities.Survey.Survey>?>? GetByUserIdAsync(Guid createdBy);
    Task<List<Domain.Entities.Survey.Survey>> GetAsync(string value);
    Task<List<Domain.Entities.Survey.Survey>> GetAsync(IEnumerable<Guid> ids);
    Task<List<Domain.Entities.Survey.Survey>> GetAsync(IEnumerable<string> values);
    Task<List<Domain.Entities.Survey.Survey>> GetAllAsync();
    Task<List<Domain.Entities.Survey.Survey>> GetAllAsync(int skip, int take, bool includeQuestions = false,bool includeTags = false,bool includeGenres = false);
    Task<List<Domain.Entities.Survey.Survey>> GetAllAsync(int skip, int take, string? search, bool includeQuestions = false,bool includeTags = false,bool includeGenres = false);
    Task AddQuestionAsync(Domain.Entities.Survey.Survey survey, Domain.Entities.Question.Question question, int order);
    Task AddQuestionsAsync(Domain.Entities.Survey.Survey survey, List<Domain.Entities.Question.Question> questions);
    Task AddTagAsync(Domain.Entities.Survey.Survey survey,Domain.Entities.Survey.SurveyTag tag);
    Task AddTagsAsync(Domain.Entities.Survey.Survey survey,IEnumerable<Tag> tags);
    Task AddGenreAsync(Domain.Entities.Survey.Survey survey,Domain.Entities.Survey.SurveyGenre genre);
    Task AddGenresAsync(Domain.Entities.Survey.Survey survey,IEnumerable<Domain.Entities.Genre> genres);
    Task AddSurveyTypeAsync(Domain.Entities.Survey.Survey survey,Domain.Entities.Survey.SurveyType surveyType);
    Task AddSurveyTypesAsync(Domain.Entities.Survey.Survey survey,IEnumerable<Domain.Entities.Survey.SurveyType> surveyTypes);
}