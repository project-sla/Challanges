using Challenges.Application.Commands.Tags.CreateTags;
using FastEndpoints;

namespace Challenges.Application.Mappings.Tags;

public class CreateTagsMapper : IMapper
{
    public static CreateTagsResponse ToResponseEntity(CreateTagsResponse tags)
    {
        return tags;
    }
}