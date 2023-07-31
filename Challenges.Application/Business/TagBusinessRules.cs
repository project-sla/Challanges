using Challenges.Persistence.Services.Tags;

namespace Challenges.Application.Business;

public class TagBusinessRules
{
    private readonly ITagService _tagService;

    public TagBusinessRules(ITagService tagService)
    {
        _tagService = tagService;
    }
    
    public async Task<bool> CheckIfTagExistsAsync(string value)
    {
        var tag = await _tagService.GetAsync(value);
        return tag != null;
    }
}