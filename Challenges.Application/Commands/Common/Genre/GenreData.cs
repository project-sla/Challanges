namespace Challenges.Application.Commands.Common.Genre;

public record GenreData(
    Guid Id,
    string? Value,
    DateTime CreatedOn,
    Guid CreatedBy
);