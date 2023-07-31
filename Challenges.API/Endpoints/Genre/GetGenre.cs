using Challenges.Application.Commands.Genre.GetGenre;
using Challenges.Application.Mappings.Genre;
using FastEndpoints;

namespace Challenges.API.Endpoints.Genre;

public class GetGenre : Endpoint<GetGenreCommand,GetGenreResponse,GetGenreMapper>
{
    public override void Configure()
    {
        Get("genre/{id}");
        AllowAnonymous();
        Validator<GetGenreValidator>();
    }

    public override async Task HandleAsync(GetGenreCommand req, CancellationToken ct)
    {
        var genre = await new GetGenreCommand(req.Id).ExecuteAsync(ct: ct);
        await SendAsync(GetGenreMapper.ToResponseEntity(genre), cancellation: ct);
    }
}