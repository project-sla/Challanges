using Challenges.Domain.Entities.Common;
using Challenges.Domain.Entities.Survey;

namespace Challenges.Domain.Entities;

public class Genre : Base
{
    private Genre()
    {
    }

    public Genre(string value, Guid createdBy)
    {
        Value = value;
        CreatedBy = createdBy;
        Surveys = new HashSet<SurveyGenre>();
    }

    public string? Value { get; set; }
    public ICollection<SurveyGenre>? Surveys { get; set; }
}