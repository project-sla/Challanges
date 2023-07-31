using Challenges.Application.Commands.Common;
using Challenges.Application.Commands.Tags.GetTags;
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
        if (command.Id.HasValue)
        {
            var tag = await _tagService.GetAsync(command.Id.Value);
            return tag is null
                ? new GetTagsResponse(new Result(false, "Tag not found", null, 404, "Not Found"))
                : new GetTagsResponse(new Result(true, null, tag, 200, "OK"));
        }
        if (command.Value is null)
            return new GetTagsResponse(new Result(false, "Invalid request", null, 400, "Bad Request"));
        {
            var tag = await _tagService.GetAsync(command.Value);
            return tag is null
                ? new GetTagsResponse(new Result(false, "Tag not found", null, 404, "Not Found"))
                : new GetTagsResponse(new Result(true, null, tag, 200, "OK"));
        }
    }
}