using Challenges.Application.Commands.Tags.GetAllTags;
using FastEndpoints;

namespace Challenges.Application.Mappings.Tags;

public class GetAllTagsMapper : IMapper
{
    public static GetAllTagsResponse ToResponseEntity(GetAllTagsResponse tags)
    {
        return tags;
    }
}