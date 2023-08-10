using Challenges.Application.Commands.Common;
using Challenges.Application.Commands.PrepareQuestions;
using Challenges.Domain.Entities.Question;
using Challenges.Persistence.Services.Answer;
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
    private readonly IAnswerService _answerService;
    private readonly IQuestionAnswerService  _questionAnswerService;

    public PrepareQuestionsHandler(ISurveyService surveyService, ISurveyTypeService surveyTypeService, IQuestionService questionService, IQuestionTypeService questionTypeService, IAnswerService answerService, IQuestionAnswerService questionAnswerService)
    {
        _surveyService = surveyService;
        _surveyTypeService = surveyTypeService;
        _questionService = questionService;
        _questionTypeService = questionTypeService;
        _answerService = answerService;
        _questionAnswerService = questionAnswerService;
    }

    public async Task<PrepareQuestionsCommandResponse> ExecuteAsync(PrepareQuestionsCommand command, CancellationToken ct)
    {
        
        var surveyType = await _surveyTypeService.GetSurveyTypeAsync(command.Survey.SurveyTypeId) ?? 
                         await _surveyTypeService.GetSurveyTypeAsync("default");

        var newSurveyEntity = new Domain.Entities.Survey.Survey(surveyType!,command.Survey.Content,command.Survey.CreatedBy);
        await _surveyService.CreateAsync(newSurveyEntity);

        foreach (var question in command.Survey.Questions)
        {
            var questionType = await _questionTypeService.GetAsync(question.QuestionTypeId) ?? 
                               await _questionTypeService.GetAsync("default");
            var newQuestionEntity = new Domain.Entities.Question.Question(question.Content,questionType!,newSurveyEntity.Id);
            await _questionService.CreateAsync(newQuestionEntity);
            foreach (var answer in question.Answers)
            {
                var newAnswerEntity = new QuestionAnswer(content:answer.Content,question:newQuestionEntity,order:answer.Order,isCorrect: answer.IsCorrect,createdBy:command.Survey.CreatedBy);
                await _questionAnswerService.CreateAsync(newAnswerEntity);
            }
        }
        return new PrepareQuestionsCommandResponse(new Result(
                true,
                null,
                newSurveyEntity,
                200,
                "Survey created successfully"
            ));
    }
}