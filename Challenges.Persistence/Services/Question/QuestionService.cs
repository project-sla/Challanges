using Challenges.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Challenges.Persistence.Services.Question;

public class QuestionService : IQuestionService
{
    private readonly ChallengeDbContext _context;

    public QuestionService(ChallengeDbContext context)
    {
        _context = context;
    }

    public async Task<Domain.Entities.Question.Question?> UpdateAsync(Domain.Entities.Question.Question? question)
    {
        if (question == null) return null;
        _context.Questions.Update(question);
        await _context.SaveChangesAsync();
        return question;
    }

    public async Task<Domain.Entities.Question.Question?> UpdateAsync(Domain.Entities.Question.Question? question, Domain.Entities.Question.QuestionType questionType)
    {
        if (question == null) return null;
        await _context.Entry(question).Reference(q => q.QuestionType).LoadAsync();
        question.QuestionType = questionType;
        _context.Questions.Update(question);
        await _context.SaveChangesAsync();
        return question;
    }

    public async Task<Domain.Entities.Question.Question?> CreateAsync(Domain.Entities.Question.QuestionType questionType, string value, Guid createdBy)
    {
        if(await _context.Questions.AnyAsync(q => q.Content == value)) return null;
        var question = new Domain.Entities.Question.Question(questionType:questionType,content: value,createdBy: createdBy);
        await _context.Questions.AddAsync(question);
        await _context.SaveChangesAsync();
        return question;
    }

    public async Task<Domain.Entities.Question.Question?> CreateAsync(Domain.Entities.Question.QuestionType questionType, string value)
    {
        if(await _context.Questions.AnyAsync(q => q.Content == value)) return null;
        var question = new Domain.Entities.Question.Question(questionType:questionType,content: value,createdBy: Guid.Empty);
        await _context.Questions.AddAsync(question);
        await _context.SaveChangesAsync();
        return question;
    }

    public async Task<Domain.Entities.Question.Question?> CreateAsync(string value)
    {
        if(await _context.Questions.AnyAsync(q => q.Content == value)) return null;
        var question = new Domain.Entities.Question.Question(questionType:new Domain.Entities.Question.QuestionType("default"),content: value,createdBy: Guid.Empty);
        await _context.Questions.AddAsync(question);
        await _context.SaveChangesAsync();
        return question;
    }

    public async Task<Domain.Entities.Question.Question?> GetAsync(Guid id)
    {
        return await _context.Questions.FindAsync(id);
    }

    public async Task<Domain.Entities.Question.Question?> GetAsync(string value)
    {
        return await _context.Questions.FirstOrDefaultAsync(q => q.Content == value);
    }

    public async Task<List<Domain.Entities.Question.Question>?> GetQuestionsByQuestionType(Guid qGuid, int skip, int take, string? search)
    {
        return await _context.Questions.Where(q => q.Content != null && search != null && q.QuestionType != null && q.QuestionType.Id == qGuid && q.Content.Contains(search)).Skip(skip).Take(take).ToListAsync();
    }

    public async Task<List<Domain.Entities.Question.Question>?>? GetQuestionsByQuestionType(Guid qGuid, int skip, int take)
    {
        return await _context.Questions.Where(q => q.QuestionType != null && q.QuestionType.Id == qGuid).Skip(skip).Take(take).ToListAsync();
    }

    public async Task<List<Domain.Entities.Question.Question>?> GetQuestionsBySurveyIdAsync(Guid surveyId, int skip, int take)
    {
        return await _context.Questions.Where(q => q.Questions!.Any(sq => sq.SurveyId == surveyId)).Skip(skip).Take(take).ToListAsync();
    }

    public async Task<List<Domain.Entities.Question.Question>?> GetQuestionsBySurveyIdAsync(Guid surveyId, int skip, int take, string? search)
    {
        return await _context.Questions.Where(q =>q.Content != null && search != null && q.Content.Contains(search) && q.Questions!.Any(sq => sq.SurveyId == surveyId)).Skip(skip).Take(take).ToListAsync();
    }

    public async Task<List<Domain.Entities.Question.Question>?> GetQuestionsByCreatedByAsync(Guid createdBy)
    {
        return await _context.Questions.Where(q => q.CreatedBy == createdBy).ToListAsync();
    }

    public async Task<List<Domain.Entities.Question.Question>> GetAsync(IEnumerable<Guid> ids)
    {
        return await _context.Questions.Where(q => ids.Contains(q.Id)).ToListAsync();
    }

    public async Task<List<Domain.Entities.Question.Question>> GetAsync(IEnumerable<string> values)
    {
        return await _context.Questions.Where(q => values.Contains(q.Content)).ToListAsync();
    }

    public async Task<List<Domain.Entities.Question.Question>> GetAllAsync()
    {
        return await _context.Questions.ToListAsync();
    }

    public async Task<List<Domain.Entities.Question.Question>> GetAllAsync(int skip, int take)
    {
        return await _context.Questions.Skip(skip).Take(take).ToListAsync();
    }

    public async Task<List<Domain.Entities.Question.Question>> GetAllAsync(int skip, int take, string? search)
    {
        if (search == null) return await _context.Questions.Skip(skip).Take(take).ToListAsync();
        return await _context.Questions.Where(q => q.Content!.Contains(search)).Skip(skip).Take(take).ToListAsync();
    }

    public async Task AddAnswerAsync(Domain.Entities.Question.Question question, Domain.Entities.Question.QuestionAnswer answer, int order)
    {
        await _context.Entry(question).Collection(q => q.Answers!).LoadAsync();
        if(question.Answers!.Any(a => a.Content == answer.Content)) return;
        answer.QuestionId = question.Id;
        answer.Order = order;
        question.Answers?.Add(answer);
        _context.Questions.Update(question);
        await _context.SaveChangesAsync();
    }

    public async Task AddAnswersAsync(Domain.Entities.Question.Question question, IEnumerable<Domain.Entities.Question.QuestionAnswer> answers)
    {
        await _context.Entry(question).Collection(q => q.Answers!).LoadAsync();
        foreach (var answer in answers)
        {
            if(question.Answers!.Any(a => a.Content == answer.Content)) continue;
            answer.QuestionId = question.Id;
            question.Answers?.Add(answer);
        }
        _context.Questions.Update(question);
        await _context.SaveChangesAsync();
    }

    public async Task AddQuestionTypeAsync(Domain.Entities.Question.Question question, Domain.Entities.Question.QuestionType questionType)
    {
        await _context.Entry(question).Reference(q => q.QuestionType).LoadAsync();
        question.QuestionType = questionType;
        _context.Questions.Update(question);
        await _context.SaveChangesAsync();
    }

    public async Task AddQuestionTypesAsync(Domain.Entities.Question.Question question, IEnumerable<Domain.Entities.Question.QuestionType> questionTypes)
    {
        await _context.Entry(question).Reference(q => q.QuestionType).LoadAsync();
        foreach (var questionType in questionTypes)
        {
            question.QuestionType = questionType;
        }
        _context.Questions.Update(question);
        await _context.SaveChangesAsync();
    }
}