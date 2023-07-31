using Challenges.Application.Commands.Tags.GetAllTags;
using Challenges.Application.Mappings.Tags;
using FastEndpoints;

namespace Challenges.API.Endpoints.Tags;

public class GetAllTags : Endpoint<GetAllTagsCommand,GetAllTagsResponse,GetAllTagsMapper>
{
    public override void Configure()
    {
        Post("tags/getAll");
        AllowAnonymous();
        Validator<GetAllTagsValidator>();
    }

    public override async Task HandleAsync(GetAllTagsCommand req, CancellationToken ct)
    {
        var tags = await new GetAllTagsCommand(req.PageNumber,req.PageSize,req.SearchTerm).ExecuteAsync(ct: ct);
        await SendAsync(GetAllTagsMapper.ToResponseEntity(tags), cancellation: ct);
    }
}