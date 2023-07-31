using Challenges.Application.Commands.Common;
using Challenges.Application.Commands.Survey.AddQuestionsToSurvey;
using Challenges.Persistence.Services.Question;
using Challenges.Persistence.Services.Survey;
using FastEndpoints;

namespace Challenges.Application.Handlers.Survey;

public class AddQuestionsToSurveyHandler : ICommandHandler<AddQuestionsToSurveyCommand, AddQuestionsToSurveyResponse>
{
    private readonly IQuestionService _questionService;
    private readonly ISurveyService _surveyService;

    public AddQuestionsToSurveyHandler(IQuestionService questionService, ISurveyService surveyService)
    {
        _questionService = questionService;
        _surveyService = surveyService;
    }

    public async Task<AddQuestionsToSurveyResponse> ExecuteAsync(AddQuestionsToSurveyCommand command, CancellationToken ct)
    {
          var survey = await _surveyService.GetAsync(command.SurveyId!.Value);
            if (survey == null)
                return new AddQuestionsToSurveyResponse(new Result(false, null, null, 400, "Survey not found"));
            var questions = await _questionService.GetAsync(command.Questions!.Select(x => x.Id!.Value).ToList());
            if (command.Questions != null && questions.Count != command.Questions.Count)
                return new AddQuestionsToSurveyResponse(new Result(false, null, null, 400, "Not all questions were found"));
            await _surveyService.AddQuestionsAsync(survey, questions);
            return new AddQuestionsToSurveyResponse(new Result(true, null, survey, 200, "Questions added to survey"));
    }
}