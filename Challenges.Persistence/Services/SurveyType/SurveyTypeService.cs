using Challenges.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Challenges.Persistence.Services.SurveyType;

public class SurveyTypeService : ISurveyTypeService
{
    private readonly ChallengeDbContext _context;

    public SurveyTypeService(ChallengeDbContext context)
    {
        _context = context;
    }

    public async Task<Domain.Entities.Survey.SurveyType> CreateSurveyTypeAsync(Domain.Entities.Survey.SurveyType surveyTypeData)
    {
        var surveyType = new Domain.Entities.Survey.SurveyType(
            value:surveyTypeData.Value!,
            createdBy:surveyTypeData.CreatedBy
        );
        await _context.SurveyTypes.AddAsync(surveyType);
        await _context.SaveChangesAsync();
        return surveyType;
    }

    public async Task<Domain.Entities.Survey.SurveyType?> GetSurveyTypeAsync(Guid surveyTypeId)
    {
        var surveyType = await _context.SurveyTypes.FindAsync(surveyTypeId);
        return surveyType;
    }

    public async Task<Domain.Entities.Survey.SurveyType?> GetSurveyTypeAsync(string value)
    {
        var surveyType = await _context.SurveyTypes.FirstOrDefaultAsync(x => x.Value == value);
        return surveyType;
    }

    public async Task<Domain.Entities.Survey.SurveyType> UpdateSurveyTypeAsync(Domain.Entities.Survey.SurveyType surveyTypeData)
    {
        var surveyType = await _context.SurveyTypes.FindAsync(surveyTypeData.Id);
        if (surveyType is null) throw new Exception($"SurveyType {surveyTypeData.Value} not found");
        surveyType.Value = surveyTypeData.Value!;
        surveyType.Update();
        await _context.SaveChangesAsync();
        return surveyType;
    }

    public async Task DeleteSurveyTypeAsync(Guid surveyTypeId)
    {
        var surveyType = await _context.SurveyTypes.FindAsync(surveyTypeId);
        if (surveyType is null) throw new Exception($"SurveyType {surveyTypeId} not found");
        _context.SurveyTypes.Remove(surveyType);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Domain.Entities.Survey.SurveyType>> GetAllSurveyTypesAsync(int page, int pageSize)
    {
        var surveyTypes = await _context.SurveyTypes
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        return surveyTypes;
    }
}