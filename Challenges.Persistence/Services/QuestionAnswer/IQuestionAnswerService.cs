namespace Challenges.Persistence.Services.QuestionAnswer;

public interface IQuestionAnswerService
{
    Task<Domain.Entities.Question.QuestionAnswer?> CreateAsync(Domain.Entities.Question.QuestionAnswer questionAnswer);

    Task<Domain.Entities.Question.QuestionAnswer?> CreateAsync(Domain.Entities.Question.QuestionAnswer questionAnswer,
        Domain.Entities.Question.Question question);

    Task<Domain.Entities.Question.QuestionAnswer?> UpdateAsync(Domain.Entities.Question.QuestionAnswer? questionAnswer);
    Task<Domain.Entities.Question.QuestionAnswer?> GetAsync(Guid id);
    Task<Domain.Entities.Question.QuestionAnswer?> GetAsync(string value);
    Task<List<Domain.Entities.Question.QuestionAnswer?>> GetAsync(IEnumerable<Guid> ids);
    Task<List<Domain.Entities.Question.QuestionAnswer?>> GetAsync(IEnumerable<string> values);
    Task<List<Domain.Entities.Question.QuestionAnswer?>> GetAllAsync();
    Task<List<Domain.Entities.Question.QuestionAnswer?>> GetAllAsync(int skip, int take);
    Task<List<Domain.Entities.Question.QuestionAnswer?>> GetAllAsync(int skip, int take, string? search);
}