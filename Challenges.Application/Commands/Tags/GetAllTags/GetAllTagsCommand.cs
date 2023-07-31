using FastEndpoints;

namespace Challenges.Application.Commands.Tags.GetAllTags;

public record GetAllTagsCommand(int PageNumber,
    int PageSize, string? SearchTerm) : ICommand<GetAllTagsResponse>;