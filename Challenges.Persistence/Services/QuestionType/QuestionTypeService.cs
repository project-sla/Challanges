using Challenges.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Challenges.Persistence.Services.QuestionType;

public class QuestionTypeService : IQuestionTypeService
{
    private readonly ChallengeDbContext _context;

    public QuestionTypeService(ChallengeDbContext context)
    {
        _context = context;
    }

    public async Task<Domain.Entities.Question.QuestionType?> UpdateAsync(Domain.Entities.Question.QuestionType? questionType)
    {
        if (questionType == null) return null;
        _context.QuestionTypes.Update(questionType);
        await _context.SaveChangesAsync();
        return questionType;
    }

    public async Task<Domain.Entities.Question.QuestionType?> CreateAsync(string value)
    {
        if (await _context.QuestionTypes.AnyAsync(qt => qt.Value == value)) return null;
        var questionType = new Domain.Entities.Question.QuestionType(value);
        await _context.QuestionTypes.AddAsync(questionType);
        await _context.SaveChangesAsync();
        return questionType;
    }

    public async Task<Domain.Entities.Question.QuestionType?> GetAsync(Guid id)
    {
        return await _context.QuestionTypes.FindAsync(id);
    }

    public async Task<Domain.Entities.Question.QuestionType?> GetAsync(string value)
    {
        return await _context.QuestionTypes.FirstOrDefaultAsync(qt => qt.Value == value);
    }

    public async Task<List<Domain.Entities.Question.QuestionType>> GetAsync(IEnumerable<Guid> ids)
    {
        return await _context.QuestionTypes.Where(qt => ids.Contains(qt.Id)).ToListAsync();
    }

    public async Task<List<Domain.Entities.Question.QuestionType>> GetAsync(IEnumerable<string> values)
    {
        return await _context.QuestionTypes.Where(qt => values.Contains(qt.Value)).ToListAsync();
    }

    public async Task<List<Domain.Entities.Question.QuestionType>> GetAllAsync()
    {
        return await _context.QuestionTypes.ToListAsync();
    }

    public async Task<List<Domain.Entities.Question.QuestionType>> GetAllAsync(int skip, int take)
    {
        return await _context.QuestionTypes.Skip(skip).Take(take).ToListAsync();
    }

    public async Task<List<Domain.Entities.Question.QuestionType>> GetAllAsync(int skip, int take, string? search)
    {
        if (string.IsNullOrWhiteSpace(search)) return await _context.QuestionTypes.Skip(skip).Take(take).ToListAsync();
        return await _context.QuestionTypes.Where(qt => qt.Value != null && qt.Value.Contains(search)).Skip(skip).Take(take).ToListAsync();
    }
}