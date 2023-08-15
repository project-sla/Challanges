using Challenges.Domain.Entities.Common;

namespace Challenges.Domain.Entities.Survey;

public class SurveyQuestion : Base
{
    private SurveyQuestion()
    {
        
    }
    public SurveyQuestion(Survey survey,Question.Question question,int order)
    {
        SurveyId = survey.Id;
        QuestionId = question.Id;
        Order = order;
    }
    public Guid SurveyId { get; set; }
    public Guid QuestionId { get; set; }
    public int Order { get; set; }
    public Survey? Survey { get; set; }
    public Question.Question? Question { get; set; }
    
}