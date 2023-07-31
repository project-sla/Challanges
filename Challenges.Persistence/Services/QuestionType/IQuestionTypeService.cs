namespace Challenges.Persistence.Services.QuestionType;

public interface IQuestionTypeService
{
    Task<Domain.Entities.Question.QuestionType?> UpdateAsync(Domain.Entities.Question.QuestionType? questionType);
    Task<Domain.Entities.Question.QuestionType?> CreateAsync(string value);
    Task<Domain.Entities.Question.QuestionType?> GetAsync(Guid id);
    Task<Domain.Entities.Question.QuestionType?> GetAsync(string value);
    Task<List<Domain.Entities.Question.QuestionType>> GetAsync(IEnumerable<Guid> ids);
    Task<List<Domain.Entities.Question.QuestionType>> GetAsync(IEnumerable<string> values);
    Task<List<Domain.Entities.Question.QuestionType>> GetAllAsync();
    Task<List<Domain.Entities.Question.QuestionType>> GetAllAsync(int skip, int take);
    Task<List<Domain.Entities.Question.QuestionType>> GetAllAsync(int skip, int take, string? search);
}