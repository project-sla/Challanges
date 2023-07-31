using Challenges.Application.Business;
using Challenges.Application.Commands.Common;
using Challenges.Application.Commands.Genre.GetGenre;
using Challenges.Persistence.Services;
using Challenges.Persistence.Services.Genre;
using FastEndpoints;

namespace Challenges.Application.Handlers.Genre;

public class GetGenreHandler : ICommandHandler<GetGenreCommand,GetGenreResponse>
{
    private readonly IGenreService _genreService;
    private readonly GenreBusinessRules _genreBusinessRules;

    public GetGenreHandler(IGenreService genreService, GenreBusinessRules genreBusinessRules)
    {
        _genreService = genreService;
        _genreBusinessRules = genreBusinessRules;
    }

    public async Task<GetGenreResponse> ExecuteAsync(GetGenreCommand command, CancellationToken ct)
    {
        var genre = await _genreBusinessRules.CheckGenre(command.Id);
        if (genre) return new GetGenreResponse(new Result(false, "Genre not found", null,404,"Genre not found"));
        var existingGenre = await _genreService.GetAsync(command.Id);
        return new GetGenreResponse(new Result(true, "Genre found", existingGenre,200,"Genre found"));
    }
}