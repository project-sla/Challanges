using Challenges.Domain.Entities.Common;

namespace Challenges.Domain.Entities.Survey;

public class Survey : Base
{
    private Survey(double time, int trueQuestionsToWin)
    {
        Time = time;
        TrueQuestionsToWin = trueQuestionsToWin;
    }

    public Survey(Base surveyType, string content, Guid createdBy, double time, int trueQuestionsToWin)
    {
        SurveyTypeId = surveyType.Id;
        CreatedBy = createdBy;
        Content = content;
        Time = time;
        TrueQuestionsToWin = trueQuestionsToWin;
        Questions = new HashSet<SurveyQuestion>();
        Tags = new HashSet<SurveyTag>();
        Genres = new HashSet<SurveyGenre>();
    }

    public Guid SurveyTypeId { get; set; }
    public double Time { get; set; }
    public int TrueQuestionsToWin { get; set; }
    public string? Content { get; set; }
    public SurveyType? SurveyType { get; set; }
    public ICollection<SurveyQuestion>? Questions { get; set; }
    public ICollection<SurveyTag>? Tags { get; set; }
    public ICollection<SurveyGenre>? Genres { get; set; }
}