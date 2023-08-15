using Challenges.Application.Commands.Tags.CreateTags;
using Challenges.Application.Mappings.Tags;
using FastEndpoints;

namespace Challenges.API.Endpoints.Tags;

public class CreateTags : Endpoint<CreateTagsCommand, CreateTagsResponse, CreateTagsMapper>
{
    public override void Configure()
    {
        Post("tags/create");
        AllowAnonymous();
        Validator<CreateTagsValidator>();
    }

    public override async Task HandleAsync(CreateTagsCommand req, CancellationToken ct)
    {
        var tags = await new CreateTagsCommand(
            req.Value
        ).ExecuteAsync(ct);
        await SendAsync(CreateTagsMapper.ToResponseEntity(tags), cancellation: ct);
    }
}