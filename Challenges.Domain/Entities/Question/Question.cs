using Challenges.Domain.Entities.Common;
using Challenges.Domain.Entities.Survey;

namespace Challenges.Domain.Entities.Question;

public class Question : Base
{
    private Question()
    {
    }

    public Question(string? content, Base questionType, Guid createdBy)
    {
        Content = content;
        CreatedBy = createdBy;
        QuestionTypeId = questionType.Id;
        Questions = new HashSet<SurveyQuestion>();
        Answers = new HashSet<QuestionAnswer>();
    }

    public Guid QuestionTypeId { get; set; }
    public string? Content { get; set; }
    public QuestionType? QuestionType { get; set; }
    public ICollection<SurveyQuestion>? Questions { get; set; }
    public ICollection<QuestionAnswer>? Answers { get; set; }
}