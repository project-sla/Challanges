using Challenges.Domain.Entities.Common;
using Challenges.Domain.Entities.Survey;

namespace Challenges.Domain.Entities;

public class Tag : Base
{
    private Tag()
    {
    }

    public Tag(string value)
    {
        Value = value;
        Surveys = new HashSet<SurveyTag>();
    }

    public string? Value { get; set; }
    public ICollection<SurveyTag>? Surveys { get; set; }
}