using Challenges.Domain.Entities.Common;

namespace Challenges.Domain.Entities.Survey;

public class SurveyGenre : Base
{
    private SurveyGenre()
    {
    }

    public SurveyGenre(Base survey, Base genre)
    {
        SurveyId = survey.Id;
        GenreId = genre.Id;
    }

    public Guid SurveyId { get; set; }
    public Guid GenreId { get; set; }
    public Survey? Survey { get; set; }
    public Genre? Genre { get; set; }
}