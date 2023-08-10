using Challenges.Application.Commands.Common;
using Challenges.Application.Commands.Question.GetQuestions;
using Challenges.Persistence.Services.Answer;
using Challenges.Persistence.Services.Question;
using Challenges.Persistence.Services.Survey;
using FastEndpoints;

namespace Challenges.Application.Handlers.Question;

public class GetQuestionsHandler : ICommandHandler<GetQuestionsCommand,GetQuestionsResponse>
{
    private readonly IQuestionService _questionService;

    public GetQuestionsHandler(IQuestionService questionService, ISurveyService surveyService, IAnswerService answerService)
    {
        _questionService = questionService;
    }

    public async Task<GetQuestionsResponse> ExecuteAsync(GetQuestionsCommand command, CancellationToken ct)
    {
        IEnumerable<Domain.Entities.Question.Question?> questions = new List<Domain.Entities.Question.Question?>();
        if(command.QuestionId is not null) questions = (await _questionService.GetAsync(command.QuestionId.Value));
        if(command.SurveyId is not null) questions = await _questionService.GetQuestionsBySurveyIdAsync(command.SurveyId.Value);
        if(command.QuestionId is null && command.SurveyId is null) return new GetQuestionsResponse(new Result(false, null, null, 400, "QuestionId or SurveyId must be provided"));
        if (command.CreatedBy is not null) questions = await _questionService.GetQuestionsByCreatedByAsync(command.CreatedBy.Value);
        if (command.QuestionTypeId is not null) questions = await _questionService.GetQuestionsByQuestionType(command.QuestionTypeId.Value,(int)command.Page,(int)command.PageSize);
        return new GetQuestionsResponse(new Result(true, null, questions, 200, "Questions retrieved"));
    }
}