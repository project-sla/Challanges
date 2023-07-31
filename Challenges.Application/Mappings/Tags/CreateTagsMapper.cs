using Challenges.Application.Commands.Tags.CreateTags;
using Challenges.Domain.Entities;
using FastEndpoints;

namespace Challenges.Application.Mappings.Tags;

public class CreateTagsMapper : IMapper
{
    public static CreateTagsResponse ToResponseEntity(CreateTagsResponse tags)
    {
        return tags;
    }
}