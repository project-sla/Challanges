namespace Challenges.Persistence.Services.Answer;

public interface IAnswerService
{
    Task AddAnswer(Domain.Entities.Question.QuestionAnswer? answer);
    Task AddAnswers(IEnumerable<Domain.Entities.Question.QuestionAnswer?> answers);
    Task<IEnumerable<Domain.Entities.Question.QuestionAnswer?>> GetAnswersByQuestionId(Guid questionId);
    Task<IEnumerable<Domain.Entities.Question.QuestionAnswer?>> GetAnswersByCreatedBy(Guid createdBy);
    Task<Domain.Entities.Question.QuestionAnswer?> GetAnswerById(Guid answerId);
    Task UpdateAnswer(Domain.Entities.Question.QuestionAnswer answer);
    Task DeleteAnswer(Guid answerId);
}