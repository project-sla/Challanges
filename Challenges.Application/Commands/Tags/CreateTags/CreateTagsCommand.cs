using FastEndpoints;

namespace Challenges.Application.Commands.Tags.CreateTags;

public record CreateTagsCommand(
    string Value
) : ICommand<CreateTagsResponse>;