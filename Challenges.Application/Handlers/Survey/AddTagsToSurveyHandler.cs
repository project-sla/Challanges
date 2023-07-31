using Challenges.Application.Commands.Common;
using Challenges.Application.Commands.Survey.AddTagsToSurvey;
using Challenges.Persistence.Services.Survey;
using Challenges.Persistence.Services.Tags;
using FastEndpoints;

namespace Challenges.Application.Handlers.Survey;

public class AddTagsToSurveyHandler : ICommandHandler<AddTagsToSurveyCommand, AddTagsToSurveyResponse>
{
    private readonly ISurveyService _surveyService;
    private readonly ITagService _tagService;

    public AddTagsToSurveyHandler(ISurveyService surveyService, ITagService tagService)
    {
        _surveyService = surveyService;
        _tagService = tagService;
    }

    public async Task<AddTagsToSurveyResponse> ExecuteAsync(AddTagsToSurveyCommand command, CancellationToken ct)
    {
        var survey = await _surveyService.GetAsync(command.SurveyId);
        if (survey == null)
            return new AddTagsToSurveyResponse(new Result(false, null, null, 400, "Survey not found"));
        var tags = await _tagService.GetAsync(command.TagIds);
        if (tags == null)
            return new AddTagsToSurveyResponse(new Result(false, null, null, 400, "Tags not found"));
        await _surveyService.AddTagsAsync(survey, tags);
        return new AddTagsToSurveyResponse(new Result(true, null, survey, 200, "Tags added to survey"));
    }
}