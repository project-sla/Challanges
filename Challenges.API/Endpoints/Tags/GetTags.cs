using Challenges.Application.Commands.Tags.GetTags;
using Challenges.Application.Mappings.Tags;
using FastEndpoints;

namespace Challenges.API.Endpoints.Tags;

public class GetTags : Endpoint<GetTagsCommand,GetTagsResponse,GetTagsMapper>
{
    public override void Configure()
    {
        Get("tags/{id}");
        AllowAnonymous();
        Validator<GetTagsValidator>();
    }

    public override async Task HandleAsync(GetTagsCommand req, CancellationToken ct)
    {
        var tag = await new GetTagsCommand(req.Id,req.Value).ExecuteAsync(ct: ct);
        await SendAsync(GetTagsMapper.ToResponseEntity(tag), cancellation: ct);
    }
}