using FastEndpoints;

namespace Challenges.Application.Commands.Genre.CreateGenre;

public record CreateGenreCommand(
    string Value,
    Guid CreatedBy
) : ICommand<CreateGenreResponse>;