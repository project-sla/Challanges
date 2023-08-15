using Challenges.Application.Commands.Common;
using Challenges.Application.Commands.Survey.AddGenresToSurvey;
using Challenges.Persistence.Services.Genre;
using Challenges.Persistence.Services.Survey;
using FastEndpoints;

namespace Challenges.Application.Handlers.Survey;

public class AddGenresToSurveyHandler : ICommandHandler<AddGenresToSurveyCommand, AddGenresToSurveyResponse>
{
    private readonly IGenreService _genreService;
    private readonly ISurveyService _surveyService;

    public AddGenresToSurveyHandler(ISurveyService surveyService, IGenreService genreService)
    {
        _surveyService = surveyService;
        _genreService = genreService;
    }

    public async Task<AddGenresToSurveyResponse> ExecuteAsync(AddGenresToSurveyCommand command, CancellationToken ct)
    {
        var survey = await _surveyService.GetAsync(command.SurveyId);
        if (survey == null)
            return new AddGenresToSurveyResponse(new Result(false, null, null, 400, "Survey not found"));
        var genres = await _genreService.GetAsync(command.GenreIds);
        if (genres == null)
            return new AddGenresToSurveyResponse(new Result(false, null, null, 400, "Genres not found"));
        await _surveyService.AddGenresAsync(survey, genres);
        return new AddGenresToSurveyResponse(new Result(true, null, survey, 200, "Genres added to survey"));
    }
}