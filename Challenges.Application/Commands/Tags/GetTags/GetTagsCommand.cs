using FastEndpoints;

namespace Challenges.Application.Commands.Tags.GetTags;

public record GetTagsCommand(
        Guid? Id,
        string? Value
    ) : ICommand<GetTagsResponse>;