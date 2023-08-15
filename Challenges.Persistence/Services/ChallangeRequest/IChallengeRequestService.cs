using Challenges.Domain.Entities.Survey;

namespace Challenges.Persistence.Services.ChallangeRequest;

public interface IChallengeRequestService
{
    Task<ChallengeRequest?> CreateAsync(ChallengeRequest challengeRequest);
    Task<ChallengeRequest?> UpdateAsync(ChallengeRequest challengeRequest);
    Task UpdateAsync(IEnumerable<ChallengeRequest> challengeRequests);
    Task<List<ChallengeRequest>?> GetAsync(Guid receivedBy);
    Task<List<ChallengeRequest>?> GetAsync(Guid receivedBy, Guid surveyId);
    Task<List<ChallengeRequest>> GetListByReceivedByIdAsync(Guid receivedBy);
    Task<List<ChallengeRequest>> GetListBySurveyId(Guid surveyId);
    Task<List<ChallengeRequest>> GetListByCreatedByAsync(Guid createdBy);
}