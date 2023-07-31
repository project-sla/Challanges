using Challenges.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Challenges.Persistence.Services.Answer;

public class AnswerService : IAnswerService
{
    private readonly ChallengeDbContext _context;

    public AnswerService(ChallengeDbContext context)
    {
        _context = context;
    }

    public async Task AddAnswer(Domain.Entities.Question.QuestionAnswer? answer)
    {
        await _context.QuestionAnswers.AddAsync(answer);
        await _context.SaveChangesAsync();
    }

    public async Task AddAnswers(IEnumerable<Domain.Entities.Question.QuestionAnswer?> answers)
    {
        await _context.QuestionAnswers.AddRangeAsync(answers);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Domain.Entities.Question.QuestionAnswer?>> GetAnswersByQuestionId(Guid questionId)
    {
        return await _context.QuestionAnswers.Where(x => x.QuestionId == questionId).ToListAsync();
    }

    public async Task<IEnumerable<Domain.Entities.Question.QuestionAnswer?>> GetAnswersByCreatedBy(Guid createdBy)
    {
        return await _context.QuestionAnswers.Where(x => x != null && x.CreatedBy == createdBy).ToListAsync();
    }

    public async Task<Domain.Entities.Question.QuestionAnswer?> GetAnswerById(Guid answerId)
    {
        return await _context.QuestionAnswers.FirstOrDefaultAsync(x => x != null && x.Id == answerId);
    }

    public async Task UpdateAnswer(Domain.Entities.Question.QuestionAnswer answer)
    {
        _context.QuestionAnswers.Update(answer);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAnswer(Guid answerId)
    {
        var answer = await _context.QuestionAnswers.FirstOrDefaultAsync(x => x != null && x.Id == answerId);
        if (answer != null)
        {
            _context.QuestionAnswers.Remove(answer);
            await _context.SaveChangesAsync();
        }
    }
}