using Challenges.Application.Commands.AnswerQuestions;
using Challenges.Application.Commands.Common;
using Challenges.Application.Commands.Common.Answer;
using Challenges.Persistence.Services.ChallangeRequest;
using Challenges.Persistence.Services.Question;
using Challenges.Persistence.Services.QuestionAnswer;
using Challenges.Persistence.Services.Survey;
using FastEndpoints;

namespace Challenges.Application.Handlers.AnswerQuestions;

public class AnswerQuestionsHandler : ICommandHandler<AnswerQuestionsCommand, AnswerQuestionsResponse>
{
    private readonly ISurveyService _surveyService;
    private readonly IQuestionService _questionService;
    private readonly IQuestionAnswerService _questionAnswerService;
    private readonly IChallengeRequestService _challengeRequestService;

    public AnswerQuestionsHandler(ISurveyService surveyService, IQuestionService questionService, IQuestionAnswerService questionAnswerService, IChallengeRequestService challengeRequestService)
    {
        _surveyService = surveyService;
        _questionService = questionService;
        _questionAnswerService = questionAnswerService;
        _challengeRequestService = challengeRequestService;
    }

    public async Task<AnswerQuestionsResponse> ExecuteAsync(AnswerQuestionsCommand command, CancellationToken ct)
    {
        var challengeR = await _challengeRequestService.GetAsync(command.Survey.ReceivedBy, command.Survey.Id);
        if (challengeR is null) return new AnswerQuestionsResponse(new Result(false, null, null, 404, "Challenge request not found"),null);
        var survey = await _surveyService.GetAsync(command.Survey.Id);
        if (survey is null) return new AnswerQuestionsResponse(new Result(false, null, null, 404, "Survey not found"),null);
        var questions = await _questionService.GetAsync(command.Survey.Questions.Select(x => x.Id).ToList());
        if (questions is null) return new AnswerQuestionsResponse(new Result(false, null, null, 404, "Questions not found"),null);
        var questionAnswers = await _questionAnswerService.GetAsync(command.Survey.Questions.Select(x => x.AnswerId).ToList());
        if (questionAnswers is null) return new AnswerQuestionsResponse(new Result(false, null, null, 404, "Question answers not found"),null);

        var trueAnswers = questionAnswers.Where(x => x is { IsCorrect: true }).ToList();
        var falseAnswers = questionAnswers.Where(x => x is { IsCorrect: false }).ToList();
        var answerResults = new List<List<AnswerData>>();
        var trueAnswerList = (from answer in trueAnswers let question = questions.FirstOrDefault(x => x.Id == answer.QuestionId) where question is not null select new AnswerData(answer.Id, question.Id, answer.Order, answer.Content, answer.CreatedBy, answer.IsCorrect)).ToList();
        var falseAnswerList = (from answer in falseAnswers let question = questions.FirstOrDefault(x => x.Id == answer.QuestionId) where question is not null select new AnswerData(answer.Id, question.Id, answer.Order, answer.Content, answer.CreatedBy, answer.IsCorrect)).ToList();
        answerResults.Add(trueAnswerList);
        answerResults.Add(falseAnswerList);
        var result = new Result(true, null, survey, 200, "Survey answered successfully");
        var challengeRequests = challengeR.Select(e =>
        {
            e.Complete();
            e.DeActivate();
            return e;
        }).ToList();

        await _challengeRequestService.UpdateAsync(challengeRequests);
        return new AnswerQuestionsResponse(result, answerResults);

    }
}