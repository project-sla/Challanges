namespace Challenges.Application.Commands.Common;

public record Result(
    bool IsSuccess,
    string? Error,
    object? Data,
    int StatusCode,
    string Message
);