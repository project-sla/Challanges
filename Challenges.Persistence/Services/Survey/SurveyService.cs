using Challenges.Domain.Entities;
using Challenges.Domain.Entities.Survey;
using Challenges.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Challenges.Persistence.Services.Survey;

public class SurveyService : ISurveyService
{
    private readonly ChallengeDbContext _context;

    public SurveyService(ChallengeDbContext context)
    {
        _context = context;
    }

    public async Task<Domain.Entities.Survey.Survey?> UpdateAsync(Domain.Entities.Survey.Survey? survey)
    {
        if (survey == null) return null;
        _context.Surveys.Update(survey);
        await _context.SaveChangesAsync();
        return survey;
    }

    public async Task<Domain.Entities.Survey.Survey?> UpdateAsync(Domain.Entities.Survey.Survey? survey,
        Domain.Entities.Survey.SurveyType surveyType)
    {
        if (survey == null) return null;
        survey.SurveyType = surveyType;
        _context.Surveys.Update(survey);
        await _context.SaveChangesAsync();
        return survey;
    }

    public async Task<Domain.Entities.Survey.Survey> CreateAsync(Domain.Entities.Survey.Survey survey)
    {
        await _context.Surveys.AddAsync(survey);
        await _context.SaveChangesAsync();
        return survey;
    }
    

    public async Task<Domain.Entities.Survey.Survey?> GetAsync(Guid id)
    {
        var query = _context.Surveys.AsQueryable();

        query = query.Include(x => x.Questions);

        query = query.Include(x => x.Tags);

        query = query.Include(x => x.Genres);

        return await query.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<Domain.Entities.Survey.Survey>?>? GetByUserIdAsync(Guid createdBy)
    {
        var query = _context.Surveys.AsQueryable();

        query = query.Include(x => x.Questions);

        query = query.Include(x => x.Tags);

        query = query.Include(x => x.Genres);

        return await query.Where(x => x.CreatedBy == createdBy).ToListAsync();
    }

    public async Task<List<Domain.Entities.Survey.Survey>> GetAsync(string value)
    {
        var query = _context.Surveys.AsQueryable();

        query = query.Include(x => x.Questions);

        query = query.Include(x => x.Tags);

        query = query.Include(x => x.Genres);

        return await query.Where(x => x.Content == value).ToListAsync();
    }

    public async Task<List<Domain.Entities.Survey.Survey>> GetAsync(IEnumerable<Guid> ids)
    {
        var query = _context.Surveys.AsQueryable();

        query = query.Include(x => x.Questions);

        query = query.Include(x => x.Tags);

        query = query.Include(x => x.Genres);

        return await query.Where(x => ids.Contains(x.Id)).ToListAsync();
    }

    public async Task<List<Domain.Entities.Survey.Survey>> GetAsync(IEnumerable<string> values)
    {
        var query = _context.Surveys.AsQueryable();
        query = query.Include(x => x.Questions);

        query = query.Include(x => x.Tags);

        query = query.Include(x => x.Genres);

        return await query.Where(x => values.Contains(x.Content)).ToListAsync();
    }

    public async Task<List<Domain.Entities.Survey.Survey>> GetAllAsync()
    {
        return await _context.Surveys.ToListAsync();
    }

    public async Task<List<Domain.Entities.Survey.Survey>> GetAllAsync(int skip, int take,
        bool includeQuestions = false, bool includeTags = false,
        bool includeGenres = false)
    {
        var query = _context.Surveys.AsQueryable();
        if (includeQuestions) query = query.Include(x => x.Questions);

        if (includeTags) query = query.Include(x => x.Tags);

        if (includeGenres) query = query.Include(x => x.Genres);

        return await query.Skip(skip).Take(take).ToListAsync();
    }

    public async Task<List<Domain.Entities.Survey.Survey>> GetAllAsync(int skip, int take, string? search,
        bool includeQuestions = false,
        bool includeTags = false, bool includeGenres = false)
    {
        var query = _context.Surveys.AsQueryable();
        if (includeQuestions) query = query.Include(x => x.Questions);

        if (includeTags) query = query.Include(x => x.Tags);

        if (includeGenres) query = query.Include(x => x.Genres);

        if (!string.IsNullOrWhiteSpace(search))
            query = query.Where(x => x.Content != null && x.Content.Contains(search));

        return await query.Skip(skip).Take(take).ToListAsync();
    }


    public async Task AddQuestionAsync(Domain.Entities.Survey.Survey survey, Domain.Entities.Question.Question question,
        int order)
    {
        if (await _context.SurveyQuestions.AnyAsync(x => x.SurveyId == survey.Id && x.QuestionId == question.Id))
            return;
        survey.Questions ??= new HashSet<SurveyQuestion>();
        var surveyQuestion = new SurveyQuestion(survey, question, order);
        await _context.SurveyQuestions.AddAsync(surveyQuestion);
        await _context.SaveChangesAsync();
    }

    public async Task AddQuestionsAsync(Domain.Entities.Survey.Survey survey,
        List<Domain.Entities.Question.Question> questions)
    {
        var surveyQuestionList = new List<SurveyQuestion>();
        foreach (var question in questions)
        {
            if (await _context.SurveyQuestions.AnyAsync(x => x.SurveyId == survey.Id && x.QuestionId == question.Id))
                continue;
            var surveyQuestion = new SurveyQuestion(survey, question, default);
            surveyQuestionList.Add(surveyQuestion);
        }

        await _context.SurveyQuestions.AddRangeAsync(surveyQuestionList);
        await _context.SaveChangesAsync();
    }

    public async Task AddTagAsync(Domain.Entities.Survey.Survey survey, SurveyTag tag)
    {
        if (await _context.SurveyTags.AnyAsync(x => x.SurveyId == survey.Id && x.TagId == tag.Id)) return;
        survey.Tags ??= new HashSet<SurveyTag>();
        survey.Tags.Add(tag);
        var surveyTag = new SurveyTag(survey, tag);
        await _context.SurveyTags.AddAsync(surveyTag);
        await _context.SaveChangesAsync();
    }

    public async Task AddTagsAsync(Domain.Entities.Survey.Survey survey, IEnumerable<Tag> tags)
    {
        survey.Tags ??= new HashSet<SurveyTag>();
        foreach (var tag in tags)
        {
            if (await _context.SurveyTags.AnyAsync(x => x.SurveyId == survey.Id && x.TagId == tag.Id)) continue;
            var surveyTag = new SurveyTag(survey, tag);
            survey.Tags.Add(surveyTag);
            await _context.SurveyTags.AddAsync(surveyTag);
        }

        await _context.SaveChangesAsync();
    }

    public async Task AddGenreAsync(Domain.Entities.Survey.Survey survey, SurveyGenre genre)
    {
        if (await _context.SurveyGenres.AnyAsync(x => x.SurveyId == survey.Id && x.GenreId == genre.Id)) return;
        survey.Genres?.Add(genre);
        var surveyGenre = new SurveyGenre(survey, genre);
        await _context.SurveyGenres.AddAsync(surveyGenre);
        await _context.SaveChangesAsync();
    }

    public async Task AddGenresAsync(Domain.Entities.Survey.Survey survey, IEnumerable<Domain.Entities.Genre> genres)
    {
        foreach (var genre in genres)
        {
            if (await _context.SurveyGenres.AnyAsync(x => x.SurveyId == survey.Id && x.GenreId == genre.Id)) continue;
            var surveyGenre = new SurveyGenre(survey, genre);
            survey.Genres?.Add(surveyGenre);
            await _context.SurveyGenres.AddAsync(surveyGenre);
        }

        await _context.SaveChangesAsync();
    }

    public async Task AddSurveyTypeAsync(Domain.Entities.Survey.Survey survey,
        Domain.Entities.Survey.SurveyType surveyType)
    {
        if (await _context.Surveys.AnyAsync(x => x.Id == survey.Id && x.SurveyType == surveyType)) return;
        survey.SurveyType = surveyType;
        await _context.SaveChangesAsync();
    }

    public async Task AddSurveyTypesAsync(Domain.Entities.Survey.Survey survey,
        IEnumerable<Domain.Entities.Survey.SurveyType> surveyTypes)
    {
        foreach (var surveyType in surveyTypes)
        {
            if (await _context.Surveys.AnyAsync(x => x.Id == survey.Id && x.SurveyType == surveyType)) continue;
            survey.SurveyType = surveyType;
        }

        await _context.SaveChangesAsync();
    }
}