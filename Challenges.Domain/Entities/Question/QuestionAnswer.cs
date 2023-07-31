using Challenges.Domain.Entities.Common;

namespace Challenges.Domain.Entities.Question;

public class QuestionAnswer : Base
{
    private QuestionAnswer()
    {
    }

    public QuestionAnswer(Base question, string content, int order, Guid createdBy)
    {
        QuestionId = question.Id;
        CreatedBy = createdBy;
        Content = content;
        Order = order;
    }

    public Guid QuestionId { get; set; }
    public Question? Question { get; set; }
    public string? Content { get; set; }
    public int Order { get; set; }
}