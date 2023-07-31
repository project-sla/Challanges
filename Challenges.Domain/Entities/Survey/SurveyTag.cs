using Challenges.Domain.Entities.Common;

namespace Challenges.Domain.Entities.Survey;

public class SurveyTag : Base
{
    private SurveyTag()
    {
    }

    public SurveyTag(Base survey, Base tag)
    {
        SurveyId = survey.Id;
        TagId = tag.Id;
    }

    public Guid SurveyId { get; set; }
    public Guid TagId { get; set; }
    public Survey? Survey { get; set; }
    public Tag? Tag { get; set; }
}