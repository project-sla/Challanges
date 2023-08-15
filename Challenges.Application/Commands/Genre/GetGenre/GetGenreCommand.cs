using FastEndpoints;

namespace Challenges.Application.Commands.Genre.GetGenre;

public record GetGenreCommand(
    Guid Id
) : ICommand<GetGenreResponse>;