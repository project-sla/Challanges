using Challenges.Domain.Entities.Common;

namespace Challenges.Domain.Entities.Survey;

public class Survey : Base
{
    private Survey()
    {
    }

    public Survey(Base surveyType, string content, Guid createdBy)
    {
        SurveyTypeId = surveyType.Id;
        CreatedBy = createdBy;
        Content = content;
        Questions = new HashSet<SurveyQuestion>();
        Tags = new HashSet<SurveyTag>();
        Genres = new HashSet<SurveyGenre>();
    }

    public Guid SurveyTypeId { get; set; }
    public string? Content { get; set; }
    public SurveyType? SurveyType { get; set; }
    public ICollection<SurveyQuestion>? Questions { get; set; }
    public ICollection<SurveyTag>? Tags { get; set; }
    public ICollection<SurveyGenre>? Genres { get; set; }
}