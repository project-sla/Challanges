namespace Challenges.Persistence.Services.Question;

public interface IQuestionService
{
    Task<Domain.Entities.Question.Question?> UpdateAsync(Domain.Entities.Question.Question? question);

    Task<Domain.Entities.Question.Question?> UpdateAsync(Domain.Entities.Question.Question? question,
        Domain.Entities.Question.QuestionType questionType);

    Task<Domain.Entities.Question.Question?> CreateAsync(Domain.Entities.Question.QuestionType questionType,
        string value, Guid createdBy);

    Task<Domain.Entities.Question.Question?> CreateAsync(Domain.Entities.Question.QuestionType questionType,
        string value);

    Task<Domain.Entities.Question.Question?> CreateAsync(string value);
    Task<Domain.Entities.Question.Question> CreateAsync(Domain.Entities.Question.Question question);
    Task<List<Domain.Entities.Question.Question>> GetAsync(Guid id);
    Task<Domain.Entities.Question.Question?> GetAsync(string value);

    Task<List<Domain.Entities.Question.Question>?>? GetQuestionsByQuestionType(Guid qGuid, int skip, int take,
        string? search);

    Task<List<Domain.Entities.Question.Question>?>? GetQuestionsByQuestionType(Guid qGuid, int skip, int take);
    Task<List<Domain.Entities.Question.Question>?> GetQuestionsBySurveyIdAsync(Guid surveyId);

    Task<List<Domain.Entities.Question.Question>?> GetQuestionsBySurveyIdAsync(Guid surveyId, int skip, int take,
        string? search);

    Task<List<Domain.Entities.Question.Question>?> GetQuestionsByCreatedByAsync(Guid createdBy);
    Task<List<Domain.Entities.Question.Question>> GetAsync(IEnumerable<Guid> ids);
    Task<List<Domain.Entities.Question.Question>> GetAsync(IEnumerable<string> values);
    Task<List<Domain.Entities.Question.Question>> GetAllAsync();
    Task<List<Domain.Entities.Question.Question>> GetAllAsync(int skip, int take);
    Task<List<Domain.Entities.Question.Question>> GetAllAsync(int skip, int take, string? search);

    Task AddAnswerAsync(Domain.Entities.Question.Question question, Domain.Entities.Question.QuestionAnswer answer,
        int order);

    Task AddAnswersAsync(Domain.Entities.Question.Question question,
        IEnumerable<Domain.Entities.Question.QuestionAnswer> answers);

    Task AddQuestionTypeAsync(Domain.Entities.Question.Question question,
        Domain.Entities.Question.QuestionType questionType);

    Task AddQuestionTypesAsync(Domain.Entities.Question.Question question,
        IEnumerable<Domain.Entities.Question.QuestionType> questionTypes);
}