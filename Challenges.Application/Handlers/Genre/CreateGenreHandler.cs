using Challenges.Application.Commands.Common;
using Challenges.Application.Commands.Genre.CreateGenre;
using Challenges.Persistence.Services.Genre;
using FastEndpoints;

namespace Challenges.Application.Handlers.Genre;

public class CreateGenreHandler : ICommandHandler<CreateGenreCommand, CreateGenreResponse>
{
    private readonly IGenreService _genreService;

    public CreateGenreHandler(IGenreService genreService)
    {
        _genreService = genreService;
    }

    public async Task<CreateGenreResponse> ExecuteAsync(CreateGenreCommand command, CancellationToken ct)
    {
        var genre = await _genreService.QueryAsync(e => e.Value == command.Value);
        if (genre is not null)
            return new CreateGenreResponse(new Result(false, "Genre already exists", null, 409, "Conflict"));
        genre = new Domain.Entities.Genre(command.Value, command.CreatedBy);
        genre = await _genreService.CreateAsync(genre);
        return new CreateGenreResponse(new Result(true, "Genre created successfully", genre, 201, "Created"));
    }
}