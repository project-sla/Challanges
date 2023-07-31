using Challenges.Domain.Entities.Common;

namespace Challenges.Domain.Entities.Question;

public class QuestionType : Base
{
    private QuestionType()
    {
    }

    public QuestionType(string value)
    {
        Value = value;
        Questions = new HashSet<Question>();
    }

    public string? Value { get; set; }
    public ICollection<Question>? Questions { get; set; }
}