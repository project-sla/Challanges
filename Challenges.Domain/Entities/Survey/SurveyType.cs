using Challenges.Domain.Entities.Common;

namespace Challenges.Domain.Entities.Survey;

public class SurveyType : Base
{
    private SurveyType()
    {
    }

    public SurveyType(string value, Guid createdBy)
    {
        CreatedBy = createdBy;
        Value = value;
        Surveys = new HashSet<Survey>();
    }

    public string? Value { get; set; }
    public ICollection<Survey>? Surveys { get; set; }
}