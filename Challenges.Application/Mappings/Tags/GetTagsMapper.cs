using Challenges.Application.Commands.Tags.GetTags;
using FastEndpoints;

namespace Challenges.Application.Mappings.Tags;

public class GetTagsMapper : IMapper
{
    public static GetTagsResponse ToResponseEntity(GetTagsResponse tag)
    {
        return tag;
    }
}