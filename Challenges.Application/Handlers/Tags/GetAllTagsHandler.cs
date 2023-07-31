using Challenges.Application.Commands.Common;
using Challenges.Application.Commands.Tags.GetAllTags;
using Challenges.Persistence.Services.Tags;
using FastEndpoints;

namespace Challenges.Application.Handlers.Tags;

public class GetAllTagsHandler : ICommandHandler<GetAllTagsCommand,GetAllTagsResponse>
{
    private readonly ITagService _tagService;

    public GetAllTagsHandler(ITagService tagService)
    {
        _tagService = tagService;
    }

    public async Task<GetAllTagsResponse> ExecuteAsync(GetAllTagsCommand command, CancellationToken ct)
    {
        var tags = await _tagService.GetAllAsync(command.PageNumber, command.PageSize, command.SearchTerm);
        return new GetAllTagsResponse(new Result(true,null, tags, 200, "Tags retrieved successfully."));
    }
}