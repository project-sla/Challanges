using Challenges.Application.Commands.Common;
using Challenges.Application.Commands.Tags.GetTags;
using Challenges.Domain.Entities;
using Challenges.Persistence.Services.Tags;
using FastEndpoints;

namespace Challenges.Application.Handlers.Tags;

public class GetTagsHandler : ICommandHandler<GetTagsCommand,GetTagsResponse>
{
    private readonly ITagService _tagService;

    public GetTagsHandler(ITagService tagService)
    {
        _tagService = tagService;
    }

    public async Task<GetTagsResponse> ExecuteAsync(GetTagsCommand command, CancellationToken ct)
    {
        Tag? tags;
        switch (command.Value)
        {
            case null when command.Id is not null:
                tags = await _tagService.GetAsync(command.Id.Value);
                return new GetTagsResponse(new Result(true,null, tags, 200, "Tag retrieved successfully."));
            case null:
                return new GetTagsResponse(new Result(false, null, null, 400, "Tag not found."));
            default:
                tags = await _tagService.GetAsync(command.Value);
                return new GetTagsResponse(new Result(true,null, tags, 200, "Tags retrieved successfully."));
        }
    }
}