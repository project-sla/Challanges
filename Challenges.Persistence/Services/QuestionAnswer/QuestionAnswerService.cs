using Challenges.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Challenges.Persistence.Services.QuestionAnswer;

public class QuestionAnswerService : IQuestionAnswerService
{
    private readonly ChallengeDbContext _context;

    public QuestionAnswerService(ChallengeDbContext context)
    {
        _context = context;
    }

    public async Task<Domain.Entities.Question.QuestionAnswer?> CreateAsync(
        Domain.Entities.Question.QuestionAnswer questionAnswer)
    {
        if (await _context.QuestionAnswers.AnyAsync(x => x.Id == questionAnswer.Id)) return null;
        await _context.QuestionAnswers.AddAsync(questionAnswer);
        await _context.SaveChangesAsync();
        return questionAnswer;
    }

    public async Task<Domain.Entities.Question.QuestionAnswer?> CreateAsync(
        Domain.Entities.Question.QuestionAnswer questionAnswer, Domain.Entities.Question.Question question)
    {
        if (await _context.QuestionAnswers.AnyAsync(x => x.Id == questionAnswer.Id)) return null;
        await _context.Entry(questionAnswer).Reference(q => q.Question).LoadAsync();
        questionAnswer.Question = question;
        await _context.QuestionAnswers.AddAsync(questionAnswer);
        await _context.SaveChangesAsync();
        return questionAnswer;
    }

    public async Task<Domain.Entities.Question.QuestionAnswer?> UpdateAsync(
        Domain.Entities.Question.QuestionAnswer? questionAnswer)
    {
        if (questionAnswer == null) return null;
        if (await _context.QuestionAnswers.AnyAsync(x => x.Id == questionAnswer.Id)) return null;
        _context.QuestionAnswers.Update(questionAnswer);
        await _context.SaveChangesAsync();
        return questionAnswer;
    }

    public async Task<Domain.Entities.Question.QuestionAnswer?> GetAsync(Guid id)
    {
        return await _context.QuestionAnswers.FindAsync(id);
    }

    public async Task<Domain.Entities.Question.QuestionAnswer?> GetAsync(string value)
    {
        return await _context.QuestionAnswers.FirstOrDefaultAsync(x => x.Content == value);
    }

    public async Task<List<Domain.Entities.Question.QuestionAnswer?>> GetAsync(IEnumerable<Guid> ids)
    {
        return await _context.QuestionAnswers.Where(x => ids.Contains(x.Id)).ToListAsync();
    }

    public async Task<List<Domain.Entities.Question.QuestionAnswer?>> GetAsync(IEnumerable<string> values)
    {
        return await _context.QuestionAnswers.Where(x => values.Contains(x.Content)).ToListAsync();
    }

    public async Task<List<Domain.Entities.Question.QuestionAnswer?>> GetAllAsync()
    {
        return await _context.QuestionAnswers.ToListAsync();
    }

    public async Task<List<Domain.Entities.Question.QuestionAnswer?>> GetAllAsync(int skip, int take)
    {
        return await _context.QuestionAnswers.Skip(skip).Take(take).ToListAsync();
    }

    public async Task<List<Domain.Entities.Question.QuestionAnswer?>> GetAllAsync(int skip, int take, string? search)
    {
        if (search == null) return await _context.QuestionAnswers.Skip(skip).Take(take).ToListAsync();
        return await _context.QuestionAnswers.Where(x => x.Content != null && x.Content.Contains(search)).Skip(skip)
            .Take(take).ToListAsync();
    }
}