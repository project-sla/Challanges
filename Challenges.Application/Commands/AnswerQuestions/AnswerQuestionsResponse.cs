using Challenges.Application.Commands.Common;
using Challenges.Application.Commands.Common.Question;

namespace Challenges.Application.Commands.AnswerQuestions;

public record AnswerQuestionsResponse(
    Result Result
);

public class ResultData
{
    public int TrueAnswers {get;set;}
    public int FalseAnswers {get;set;}
    public string Message {get;set;}
    public Dictionary<string,int> AnswerResults {get;set;}
}