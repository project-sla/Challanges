using Challenges.Application.Business;
using Challenges.Application.Commands.Common;
using Challenges.Application.Commands.Tags.CreateTags;
using Challenges.Persistence.Services.Tags;
using FastEndpoints;

namespace Challenges.Application.Handlers.Tags;

public class CreateTagsHandler : ICommandHandler<CreateTagsCommand, CreateTagsResponse>
{
    private readonly TagBusinessRules _tagBusinessRules;
    private readonly ITagService _tagService;

    public CreateTagsHandler(ITagService tagService, TagBusinessRules tagBusinessRules)
    {
        _tagService = tagService;
        _tagBusinessRules = tagBusinessRules;
    }

    public async Task<CreateTagsResponse> ExecuteAsync(CreateTagsCommand command, CancellationToken ct)
    {
        var tagExists = await _tagBusinessRules.CheckIfTagExistsAsync(command.Value);
        if (tagExists) return new CreateTagsResponse(new Result(false, "Tag already exists", null, 409, "Conflict"));
        var tag = await _tagService.CreateAsync(command.Value);
        return new CreateTagsResponse(new Result(true, "Tag created successfully", tag, 201, "Created"));
    }
}