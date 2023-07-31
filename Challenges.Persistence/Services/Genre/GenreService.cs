using System.Linq.Expressions;
using Challenges.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Challenges.Persistence.Services.Genre;

public class GenreService : IGenreService
{
    private readonly ChallengeDbContext _context;
    public GenreService(ChallengeDbContext context)
    {
        _context = context;
    }

    public async Task<Domain.Entities.Genre> CreateAsync(Domain.Entities.Genre genre)
    {
        await _context.AddAsync(genre);
        return genre;
    }

    public Task<Domain.Entities.Genre> UpdateAsync(Domain.Entities.Genre genre)
    {
        _context.Update(genre);
        return Task.FromResult(genre);
    }

    public Task DeleteAsync(Domain.Entities.Genre genre)
    {
        _context.Remove(genre);
        return Task.CompletedTask;
    }

    public async Task<Domain.Entities.Genre?> GetAsync(Guid id)
    {
        return await _context.Genres.Where(e=>e.Id == id).FirstOrDefaultAsync();
    }

    public async Task<Domain.Entities.Genre?> GetAsync(string value)
    {
        return await _context.Genres.Where(e=>e.Value == value).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Domain.Entities.Genre>> GetAsync(IEnumerable<Guid> ids)
    {
        return await _context.Genres.Where(e=>ids.Contains(e.Id)).ToListAsync();
    }

    public async Task<IEnumerable<Domain.Entities.Genre>> GetAllAsync()
    {
        return await _context.Genres.ToListAsync();
    }

    public async Task<IEnumerable<Domain.Entities.Genre>> GetAllAsync(int skip, int take, string? search)
    {
        return await _context.Genres.Where(e=>e.Value != null && e.Value.Contains(search ?? string.Empty)).Skip(skip).Take(take).ToListAsync();
    }

    public async Task<Domain.Entities.Genre?> QueryAsync(Expression<Func<Domain.Entities.Genre, bool>> predicate)
    {
        return await _context.Genres.Where(predicate).FirstOrDefaultAsync();
    }
}