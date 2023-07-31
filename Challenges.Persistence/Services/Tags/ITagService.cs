using Challenges.Domain.Entities;

namespace Challenges.Persistence.Services.Tags;

public interface ITagService
{
    Task<Tag?> UpdateAsync(Tag? tag);
    Task<Tag?> CreateAsync(string value);
    Task<Tag?> GetAsync(Guid id);
    Task<Tag?> GetAsync(string value);
    Task<List<Tag>> GetAsync(IEnumerable<Guid> ids);
    Task<List<Tag?>> GetAsync(IEnumerable<string> values);
    Task<List<Tag>> GetAllAsync();
    Task<List<Tag>> GetAllAsync(int skip, int take);
    Task<List<Tag>> GetAllAsync(int skip, int take, string? search);
}