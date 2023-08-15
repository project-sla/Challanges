using Challenges.Domain.Entities.Common;

namespace Challenges.Domain.Entities.Survey;

public class ChallengeRequest : Base
{
    private ChallengeRequest()
    {
    }

    public ChallengeRequest(Survey survey, Guid createdBy, Guid receivedBy, Guid? id = null) : base(id, createdBy)
    {
        SurveyId = survey.Id;
        Survey = survey;
        CreatedBy = createdBy;
        ReceivedBy = receivedBy;
        IsActive = true;
        IsCompleted = false;
        CompletedAt = null;
    }

    public Guid SurveyId { get; set; }
    public Survey? Survey { get; set; }
    public Guid ReceivedBy { get; set; }
    public bool IsActive { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime? CompletedAt { get; set; }

    public void DeActivate()
    {
        IsActive = false;
    }

    public void Activate()
    {
        IsActive = true;
    }

    public void Complete()
    {
        IsCompleted = true;
        CompletedAt = DateTime.UtcNow;
    }
}