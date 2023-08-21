using Challenges.Application.Commands.AnswerQuestions;
using Challenges.Application.Commands.Common;
using Challenges.Application.Commands.Common.Answer;
using Challenges.Application.Helpers.GetAllAccounts;
using Challenges.Domain.Entities;
using Challenges.Persistence.Services.ChallangeRequest;
using Challenges.Persistence.Services.Notification;
using Challenges.Persistence.Services.Question;
using Challenges.Persistence.Services.QuestionAnswer;
using Challenges.Persistence.Services.Survey;
using FastEndpoints;

namespace Challenges.Application.Handlers.AnswerQuestions;

public class AnswerQuestionsHandler : ICommandHandler<AnswerQuestionsCommand, AnswerQuestionsResponse>
{
    private readonly IChallengeRequestService _challengeRequestService;
    private readonly IQuestionAnswerService _questionAnswerService;
    private readonly IQuestionService _questionService;
    private readonly ISurveyService _surveyService;
    private readonly INotificationTypeService _notificationTypeService;
    private readonly INotificationService _notificationService;
    private readonly GetAllAccountHandler _getAllAccountHandler;
    public AnswerQuestionsHandler(ISurveyService surveyService, IQuestionService questionService,
        IQuestionAnswerService questionAnswerService, IChallengeRequestService challengeRequestService, INotificationTypeService notificationTypeService, INotificationService notificationService, GetAllAccountHandler getAllAccountHandler)
    {
        _surveyService = surveyService;
        _questionService = questionService;
        _questionAnswerService = questionAnswerService;
        _challengeRequestService = challengeRequestService;
        _notificationTypeService = notificationTypeService;
        _notificationService = notificationService;
        _getAllAccountHandler = getAllAccountHandler;
    }

    public async Task<AnswerQuestionsResponse> ExecuteAsync(AnswerQuestionsCommand command, CancellationToken ct)
    {
        //var message = new List
        var receivedBy = await  _getAllAccountHandler.GetAllAccounts(command.Survey.ReceivedBy.ToString());
        var notificationType = await _notificationTypeService.GetNotificationType("Survey answered") ??
                               new NotificationType("Survey answered");
        var notificationDetail = new NotificationDetail(notificationType, receivedBy?.fcmToken, receivedBy!.isAndroidDevice,
            "Survey answered", "")
        {
            NotificationType = notificationType
        };
        var notification = new Notification(command.Survey.ReceivedBy, true,true, notificationDetail)
            {
                NotificationDetail = notificationDetail
            };
        
        var challengeR = await _challengeRequestService.GetAsync(command.Survey.ReceivedBy, command.Survey.Id);
        if (challengeR is null)
            return new AnswerQuestionsResponse(new Result(false, null, null, 404, "Challenge request not found"));
        var survey = await _surveyService.GetAsync(command.Survey.Id);
        if (survey is null)
            return new AnswerQuestionsResponse(new Result(false, null, null, 404, "Survey not found"));
        var questions = await _questionService.GetAsync(command.Survey.Questions.Select(x => x.Id).ToList());
        if (questions.Count == 0)
            return new AnswerQuestionsResponse(new Result(false, null, null, 404, "Questions not found"));
        var questionAnswers =
            await _questionAnswerService.GetAsync(command.Survey.Questions.Select(x => x.AnswerId).ToList());
        if (questionAnswers.Count == 0)
            return new AnswerQuestionsResponse(new Result(false, null, null, 404, "Question answers not found"));

        var trueAnswers = questionAnswers.Where(x => x is { IsCorrect: true }).ToList();
        var falseAnswers = questionAnswers.Where(x => x is { IsCorrect: false }).ToList();
        var answerResults = new Dictionary<string, int>();
        var trueAnswerList = (from answer in trueAnswers
            let question = questions.FirstOrDefault(x => x.Id == answer.QuestionId)
            where question is not null
            select new AnswerData(answer.Id, question.Id, answer.Order, answer.Content, answer.CreatedBy,
                answer.IsCorrect)).ToList();
        var falseAnswerList = (from answer in falseAnswers
            let question = questions.FirstOrDefault(x => x.Id == answer.QuestionId)
            where question is not null
            select new AnswerData(answer.Id, question.Id, answer.Order, answer.Content, answer.CreatedBy,
                answer.IsCorrect)).ToList();
        answerResults.Add("true answers", trueAnswerList.Count);
        answerResults.Add("false answers", falseAnswerList.Count);
        var challengeRequests = challengeR.Select(e =>
        {
            e.Complete();
            e.DeActivate();
            return e;
        }).ToList();
        notification.NotificationDetail.Body = $"{receivedBy.username} answered your survey.";
        await _notificationService.SendNotification(notification);
        var resultData = new ResultData
        {
            TrueAnswers = trueAnswerList.Count,
            FalseAnswers = falseAnswerList.Count,
            AnswerResults = answerResults,
            Message = survey.TrueQuestionsToWin <= trueAnswerList.Count ? "You won the survey" : "You lost the survey"
        };
        await _challengeRequestService.UpdateAsync(challengeRequests);
        return new AnswerQuestionsResponse(new Result(true, null, resultData, 200, "Survey answered successfully"));
    }
}