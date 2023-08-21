using Challenges.Application.Commands.Common;
using Challenges.Application.Commands.PrepareQuestions;
using Challenges.Application.Helpers.GetAllAccounts;
using Challenges.Domain.Entities;
using Challenges.Domain.Entities.Question;
using Challenges.Domain.Entities.Survey;
using Challenges.Persistence.Services.ChallangeRequest;
using Challenges.Persistence.Services.Notification;
using Challenges.Persistence.Services.Question;
using Challenges.Persistence.Services.QuestionAnswer;
using Challenges.Persistence.Services.QuestionType;
using Challenges.Persistence.Services.Survey;
using Challenges.Persistence.Services.SurveyType;
using FastEndpoints;

namespace Challenges.Application.Handlers.PrepareQuestions;

public class PrepareQuestionsHandler : ICommandHandler<PrepareQuestionsCommand, PrepareQuestionsCommandResponse>
{
    private readonly IChallengeRequestService _challengeRequestService;
    private readonly INotificationService _notificationService;
    private readonly INotificationTypeService _notificationTypeService;
    private readonly IQuestionAnswerService _questionAnswerService;
    private readonly IQuestionService _questionService;
    private readonly IQuestionTypeService _questionTypeService;
    private readonly ISurveyService _surveyService;
    private readonly ISurveyTypeService _surveyTypeService;

    public PrepareQuestionsHandler(ISurveyService surveyService, ISurveyTypeService surveyTypeService,
        IQuestionService questionService, IQuestionTypeService questionTypeService,
        IQuestionAnswerService questionAnswerService, IChallengeRequestService challengeRequestService,
        INotificationTypeService notificationTypeService, INotificationService notificationService)
    {
        _surveyService = surveyService;
        _surveyTypeService = surveyTypeService;
        _questionService = questionService;
        _questionTypeService = questionTypeService;
        _questionAnswerService = questionAnswerService;
        _challengeRequestService = challengeRequestService;
        _notificationTypeService = notificationTypeService;
        _notificationService = notificationService;
    }

    public async Task<PrepareQuestionsCommandResponse> ExecuteAsync(PrepareQuestionsCommand command,
        CancellationToken ct)
    {
        var surveyType = await _surveyTypeService.GetSurveyTypeAsync(command.Survey.SurveyTypeId) ??
                         await _surveyTypeService.GetSurveyTypeAsync("default");

        var newSurveyEntity =
            new Domain.Entities.Survey.Survey(surveyType!, command.Survey.Content, command.Survey.CreatedBy,
                command.Survey.Time, command.Survey.TrueQuestionsToWin);
        var surveyResponse = new SurveyResponse(newSurveyEntity.Id, newSurveyEntity.Content, newSurveyEntity.CreatedBy,
            newSurveyEntity.SurveyTypeId, newSurveyEntity.Time, newSurveyEntity.TrueQuestionsToWin,
            new List<QuestionResponse>());
        await _surveyService.CreateAsync(newSurveyEntity);
        var questionEntList = new List<Question>();
        foreach (var question in command.Survey.Questions)
        {
            var questionType = await _questionTypeService.GetAsync(question.QuestionTypeId) ??
                               await _questionTypeService.GetAsync("default");
            var newQuestionEntity =
                new Question(question.Content, questionType!, newSurveyEntity.Id);
            await _questionService.CreateAsync(newQuestionEntity);
            var questionResponse = new QuestionResponse(newQuestionEntity.Id, newQuestionEntity.Content,
                newQuestionEntity.QuestionTypeId, new List<AnswerResponse>());
            foreach (var newAnswerEntity in question.Answers.Select(answer =>
                         new QuestionAnswer(content: answer.Content, question: newQuestionEntity, order: answer.Order,
                             isCorrect: answer.IsCorrect, createdBy: command.Survey.CreatedBy)))
            {
                await _questionAnswerService.CreateAsync(newAnswerEntity);
                var answerResponse = new AnswerResponse(newAnswerEntity.Id, newAnswerEntity.Content,
                    newAnswerEntity.Order, newAnswerEntity.IsCorrect);
                questionResponse.Answers.Add(answerResponse);
            }

            questionEntList.Add(newQuestionEntity);
            surveyResponse.Questions.Add(questionResponse);
        }

        await _surveyService.AddQuestionsAsync(newSurveyEntity, questionEntList);
        var challengeRequest =
            new ChallengeRequest(newSurveyEntity, newSurveyEntity.CreatedBy, command.Survey.ReceivedBy);
        challengeRequest.Activate();
        var receivedAccount = await GetAllAccountHandler.GetAllAccounts(command.Survey.ReceivedBy.ToString());
        var senderAccount = await GetAllAccountHandler.GetAllAccounts(command.Survey.CreatedBy.ToString());
        if (receivedAccount is null || senderAccount is null)
            return new PrepareQuestionsCommandResponse(new Result(
                false,
                null,
                null,
                404,
                "Account not found"
            ), null);
        await _challengeRequestService.CreateAsync(challengeRequest);
        var notificationType = await _notificationTypeService.GetNotificationType("Challenge Request") ??
                               new NotificationType("Challenge Request");
        var notificationDetail = new NotificationDetail(notificationType, receivedAccount.FcmToken,
            receivedAccount.IsAndroidDevice, "Challenge Request",
            $"{senderAccount.UserName} sent you a challenge request.")
        {
            NotificationType = notificationType
        };
        var notification = new Notification(command.Survey.ReceivedBy, true, true, notificationDetail)
            {
                NotificationDetail = notificationDetail
            };
        await _notificationService.SendNotification(notification);
        return new PrepareQuestionsCommandResponse(new Result(
            true,
            null,
            null,
            200,
            "Survey created successfully"
        ), surveyResponse);
    }
}