using Challenges.Application.Commands.Common;
using Challenges.Application.Commands.PrepareQuestions;
using Challenges.Domain.Entities.Question;
using Challenges.Domain.Entities.Survey;
using Challenges.Persistence.Services.Answer;
using Challenges.Persistence.Services.ChallangeRequest;
using Challenges.Persistence.Services.Question;
using Challenges.Persistence.Services.QuestionAnswer;
using Challenges.Persistence.Services.QuestionType;
using Challenges.Persistence.Services.Survey;
using Challenges.Persistence.Services.SurveyType;
using FastEndpoints;

namespace Challenges.Application.Handlers.PrepareQuestions;

public class PrepareQuestionsHandler : ICommandHandler<PrepareQuestionsCommand, PrepareQuestionsCommandResponse>
{
    
    private readonly ISurveyService _surveyService;
    private readonly ISurveyTypeService _surveyTypeService;
    private readonly IQuestionService _questionService;
    private readonly IQuestionTypeService _questionTypeService;
    private readonly IQuestionAnswerService  _questionAnswerService;
    private readonly IChallengeRequestService _challengeRequestService;
    public PrepareQuestionsHandler(ISurveyService surveyService, ISurveyTypeService surveyTypeService, IQuestionService questionService, IQuestionTypeService questionTypeService, IAnswerService answerService, IQuestionAnswerService questionAnswerService, IChallengeRequestService challengeRequestService)
    {
        _surveyService = surveyService;
        _surveyTypeService = surveyTypeService;
        _questionService = questionService;
        _questionTypeService = questionTypeService;
        _questionAnswerService = questionAnswerService;
        _challengeRequestService = challengeRequestService;
    }

    public async Task<PrepareQuestionsCommandResponse> ExecuteAsync(PrepareQuestionsCommand command, CancellationToken ct)
    {
        
        var surveyType = await _surveyTypeService.GetSurveyTypeAsync(command.Survey.SurveyTypeId) ?? 
                         await _surveyTypeService.GetSurveyTypeAsync("default");

        var newSurveyEntity = new Domain.Entities.Survey.Survey(surveyType!,command.Survey.Content,command.Survey.CreatedBy);
        var surveyResponse = new SurveyResponse(newSurveyEntity.Id,newSurveyEntity.Content,newSurveyEntity.CreatedBy,newSurveyEntity.SurveyTypeId,new List<QuestionResponse>());
        await _surveyService.CreateAsync(newSurveyEntity);
        var questionEntList = new List<Domain.Entities.Question.Question>();
        foreach (var question in command.Survey.Questions)
        {
            var questionType = await _questionTypeService.GetAsync(question.QuestionTypeId) ?? 
                               await _questionTypeService.GetAsync("default");
            var newQuestionEntity = new Domain.Entities.Question.Question(question.Content,questionType!,newSurveyEntity.Id);
            await _questionService.CreateAsync(newQuestionEntity);
            var questionResponse = new QuestionResponse(newQuestionEntity.Id,newQuestionEntity.Content,newQuestionEntity.QuestionTypeId,new List<AnswerResponse>());
            foreach (var answer in question.Answers)
            {
                var newAnswerEntity = new QuestionAnswer(content:answer.Content,question:newQuestionEntity,order:answer.Order,isCorrect: answer.IsCorrect,createdBy:command.Survey.CreatedBy);
                await _questionAnswerService.CreateAsync(newAnswerEntity);
                var answerResponse = new AnswerResponse(newAnswerEntity.Id,newAnswerEntity.Content,newAnswerEntity.Order,newAnswerEntity.IsCorrect);
                questionResponse.Answers.Add(answerResponse);
            }

            var questionObj = new Domain.Entities.Question.Question(question.Content,questionType,command.Survey.CreatedBy);
            questionEntList.Add(questionObj);
            surveyResponse.Questions.Add(questionResponse);
        }

        await _surveyService.AddQuestionsAsync(newSurveyEntity,questionEntList);
        var challengeRequest = new ChallengeRequest(newSurveyEntity,command.Survey.CreatedBy,command.Survey.ReceivedBy);
        challengeRequest.Activate();
        await _challengeRequestService.CreateAsync(challengeRequest);
        return new PrepareQuestionsCommandResponse(new Result(
                true,
                null,
                null,
                200,
                "Survey created successfully"
            ),surveyResponse);
    }
}