using FastEndpoints;

namespace Challenges.Application.Commands.Tags.GetTags;

public record GetTagsCommand(
    string? Value = null,
    Guid? Id = null) : ICommand<GetTagsResponse>;