﻿using System.Diagnostics;
using Challenges.Application.Commands.Common;
using Challenges.Application.Commands.Common.Answer;
using Challenges.Application.Commands.Common.QuestionType;
using Challenges.Application.Commands.Common.Survey;
using Challenges.Application.Commands.PrepareQuestions;
using Challenges.Application.Commands.Survey.GetSurvey;
using Challenges.Persistence.Services.Question;
using Challenges.Persistence.Services.Survey;
using Challenges.Persistence.Services.SurveyType;
using FastEndpoints;
using QuestionDto = Challenges.Application.Commands.AnswerQuestions.QuestionDto;
using SurveyDto = Challenges.Application.Commands.Survey.GetSurvey.SurveyDto;

namespace Challenges.Application.Handlers.Survey;

public class GetSurveyHandler : ICommandHandler<GetSurveyCommand,GetSurveyResponse>
{
    private readonly ISurveyService _surveyService;
    private readonly ISurveyTypeService _surveyTypeService;
    private readonly IQuestionService _questionService;

    public GetSurveyHandler(ISurveyService surveyService, ISurveyTypeService surveyTypeService, IQuestionService questionService)
    {
        _surveyService = surveyService;
        _surveyTypeService = surveyTypeService;
        _questionService = questionService;
    }

    public async Task<GetSurveyResponse> ExecuteAsync(GetSurveyCommand command, CancellationToken ct)
    {
        List<Domain.Entities.Survey.Survey>? surveyList = new();
        if (command.SurveyId is null) surveyList = await _surveyService.GetByUserIdAsync(command.UserId!.Value);
        if(command.UserId is null) surveyList.Add(await _surveyService.GetAsync(command.SurveyId!.Value));
        if(surveyList is null) return new GetSurveyResponse(new Result(false, null, null, 400, "Survey not found"));
        List<SurveyDto> surveyResponse = new();
        
        foreach (var survey in surveyList)
        {



            var surveyType = await _surveyTypeService.GetSurveyTypeAsync(survey.SurveyTypeId);
            var questions = await _questionService.GetQuestionsBySurveyIdAsync(survey.Id);

            var questionDtos = new List<QuestionData>();
            foreach (var question in questions)
            {
                var questionDto = new QuestionData(question.Id, question.CreatedBy,
                    new QuestionTypeData(question.QuestionTypeId, question.Content, question.CreatedBy),
                    question.Content, new List<AnswerData>());

                foreach (var qq in question.Answers)
                {
                    var answerDto = new AnswerData(qq.Id, qq.QuestionId, qq.Order, qq.Content, qq.CreatedBy,
                        qq.IsCorrect);
                    questionDto.Answers.Add(answerDto);
                }

                questionDtos.Add(questionDto);
            }

            surveyResponse.Add(new SurveyDto(survey.Id,
                new SurveyTypeData(surveyType.Id, surveyType.Value, surveyType.CreatedBy), questionDtos));
        }

        return new GetSurveyResponse(new Result(true, null, surveyResponse, 200, "Survey found"));
    }
}