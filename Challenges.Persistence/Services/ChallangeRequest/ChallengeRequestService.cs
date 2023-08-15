using Challenges.Domain.Entities.Survey;
using Challenges.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Challenges.Persistence.Services.ChallangeRequest;

public class ChallengeRequestService : IChallengeRequestService
{
    private readonly ChallengeDbContext _context;

    public ChallengeRequestService(ChallengeDbContext context)
    {
        _context = context;
    }

    public async Task<ChallengeRequest?> CreateAsync(ChallengeRequest challengeRequest)
    {
        await _context.ChallengeRequests.AddAsync(challengeRequest);
        await _context.SaveChangesAsync();
        return challengeRequest;
    }

    public async Task<ChallengeRequest?> UpdateAsync(ChallengeRequest challengeRequest)
    {
        _context.ChallengeRequests.Update(challengeRequest);
        await _context.SaveChangesAsync();
        return challengeRequest;
    }

    public async Task UpdateAsync(List<ChallengeRequest> challengeRequests)
    {
        _context.ChallengeRequests.UpdateRange(challengeRequests);
        await _context.SaveChangesAsync();
    }

    public async Task<List<ChallengeRequest>?> GetAsync(Guid receivedBy)
    {
        var challengeR= await _context.ChallengeRequests
            .Where(e=>e.ReceivedBy == receivedBy &&  e.IsActive.Equals(true) && e.IsCompleted.Equals(false))
            .OrderDescending()
            .ToListAsync();
        return challengeR;
    }

    public async Task<List<ChallengeRequest>?> GetAsync(Guid receivedBy, Guid surveyId)
    {
        var challengeR = await _context.ChallengeRequests
            .Where(e => e.ReceivedBy == receivedBy && e.SurveyId == surveyId && e.IsActive.Equals(true) && e.IsCompleted.Equals(false))
            .ToListAsync();
        return challengeR;
    }

    public async Task<List<ChallengeRequest>> GetListByReceivedByIdAsync(Guid receivedBy)
    {
        var challengeR = await _context.ChallengeRequests
            .Where(e => e.ReceivedBy == receivedBy)
            .ToListAsync();
        return challengeR;
    }

    public async Task<List<ChallengeRequest>> GetListBySurveyId(Guid surveyId)
    {
        var challengeR = await _context.ChallengeRequests
            .Where(e => e.SurveyId == surveyId)
            .ToListAsync();
        return challengeR;
    }

    public async Task<List<ChallengeRequest>> GetListByCreatedByAsync(Guid createdBy)
    {
        var challengeR = await _context.ChallengeRequests
            .Where(e => e.CreatedBy == createdBy)
            .ToListAsync();
        return challengeR;
    }
}