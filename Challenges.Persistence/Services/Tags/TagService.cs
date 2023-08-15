using Challenges.Domain.Entities;
using Challenges.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Challenges.Persistence.Services.Tags;

public class TagService : ITagService
{
    private readonly ChallengeDbContext _context;

    public TagService(ChallengeDbContext context)
    {
        _context = context;
    }

    public async Task<Tag?> UpdateAsync(Tag? tag)
    {
        if (tag == null) return null;
        _context.Tags.Update(tag);
        await _context.SaveChangesAsync();
        return tag;
    }

    public async Task<Tag?> CreateAsync(string value)
    {
        var tag = new Tag(value);
        await _context.Tags.AddAsync(tag);
        await _context.SaveChangesAsync();
        return tag;
    }

    public async Task<Tag?> GetAsync(Guid id)
    {
        return await _context.Tags.FindAsync(id);
    }

    public async Task<Tag?> GetAsync(string value)
    {
        var tag = await _context.Tags.FirstOrDefaultAsync(x => x.Value == value) ?? await CreateAsync(value);
        return tag;
    }

    public async Task<List<Tag>> GetAsync(IEnumerable<Guid> ids)
    {
        return await _context.Tags.Where(x => ids.Contains(x.Id)).ToListAsync();
    }

    public async Task<List<Tag?>> GetAsync(IEnumerable<string> values)
    {
        List<Tag?> tags = (await _context.Tags.Where(x => values.Contains(x.Value)).ToListAsync())!;
        var missing = values.Except(tags.Select(x => x?.Value));
        foreach (var value in missing)
        {
            var tag = await CreateAsync(value!);
            tags.Add(tag);
        }

        return tags;
    }

    public async Task<List<Tag>> GetAllAsync()
    {
        return await _context.Tags.ToListAsync();
    }

    public async Task<List<Tag>> GetAllAsync(int skip, int take)
    {
        return await _context.Tags.Skip(skip).Take(take).ToListAsync();
    }

    public async Task<List<Tag>> GetAllAsync(int skip, int take, string? search)
    {
        return await _context.Tags.Where(x => search != null && x.Value!.Contains(search)).Skip(skip).Take(take)
            .ToListAsync();
    }
}