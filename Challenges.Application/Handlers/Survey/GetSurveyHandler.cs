using Challenges.Application.Commands.Common;
using Challenges.Application.Commands.Common.Answer;
using Challenges.Application.Commands.Common.QuestionType;
using Challenges.Application.Commands.Common.Survey;
using Challenges.Application.Commands.Survey.GetSurvey;
using Challenges.Persistence.Services.Question;
using Challenges.Persistence.Services.Survey;
using Challenges.Persistence.Services.SurveyType;
using FastEndpoints;

namespace Challenges.Application.Handlers.Survey;

public class GetSurveyHandler : ICommandHandler<GetSurveyCommand, GetSurveyResponse>
{
    private readonly IQuestionService _questionService;
    private readonly ISurveyService _surveyService;
    private readonly ISurveyTypeService _surveyTypeService;

    public GetSurveyHandler(ISurveyService surveyService, ISurveyTypeService surveyTypeService,
        IQuestionService questionService)
    {
        _surveyService = surveyService;
        _surveyTypeService = surveyTypeService;
        _questionService = questionService;
    }

    public async Task<GetSurveyResponse> ExecuteAsync(GetSurveyCommand command, CancellationToken ct)
    {
        List<Domain.Entities.Survey.Survey?> surveyList = new();
        surveyList.Add(await _surveyService.GetAsync(command.SurveyId!.Value));
        if (surveyList.Count == 0) return new GetSurveyResponse(new Result(false, null, null, 400, "Survey not found"));
        List<SurveyDto> surveyResponse = new();

        foreach (var survey in surveyList)
        {
            var surveyType = await _surveyTypeService.GetSurveyTypeAsync(survey!.SurveyTypeId);
            var questions = await _questionService.GetQuestionsBySurveyIdAsync(survey.Id);

            var questionDtos = new List<QuestionData>();
            if (questions.Count == 0)
                return new GetSurveyResponse(new Result(false, null, null, 400, "Questions not found"));
            var ql = questions.OrderBy(e => e.CreatedAt);
            foreach (var question in ql)
            {
                var questionDto = new QuestionData(question.Id, question.CreatedBy,
                    new QuestionTypeData(question.QuestionTypeId, question.Content, question.CreatedBy),
                    question.Content!, new List<AnswerData>());

                foreach (var qq in question.Answers!)
                {
                    var answerDto = new AnswerData(qq.Id, qq.QuestionId, qq.Order, qq.Content, qq.CreatedBy,
                        qq.IsCorrect);
                    questionDto.Answers.Add(answerDto);
                }

                questionDtos.Add(questionDto);
            }

            //surveyResponse.OrderBy(e => e.Questions);
            surveyResponse.Add(new SurveyDto(survey.Id,
                new SurveyTypeData(surveyType.Id, surveyType.Value, surveyType.CreatedBy), questionDtos));
        }

        return new GetSurveyResponse(new Result(true, null, surveyResponse, 200, "Survey found"));
    }
}