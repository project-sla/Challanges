namespace Challenges.Application.Commands.Common.User;

public record UserData(
    Guid Id,
    string? FirstName,
    string? LastName,
    string? Email,
    string? Username
);