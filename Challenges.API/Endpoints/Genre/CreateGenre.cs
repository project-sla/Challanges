using Challenges.Application.Commands.Genre.CreateGenre;
using FastEndpoints;

namespace Challenges.API.Endpoints.Genre;

public class CreateGenre : Endpoint<CreateGenreCommand, CreateGenreResponse>
{
    public override void Configure()
    {
        Post("genre/create");
        AllowAnonymous();
        Validator<CreateGenreValidator>();
    }

    public override async Task HandleAsync(CreateGenreCommand req, CancellationToken ct)
    {
        var genre = await new CreateGenreCommand(req.Value, req.CreatedBy).ExecuteAsync(ct);
        await SendAsync(genre, cancellation: ct);
    }
}