using System.Linq.Expressions;

namespace Challenges.Persistence.Services.Genre;

public interface IGenreService
{
    Task<Domain.Entities.Genre> CreateAsync(Domain.Entities.Genre genre);
    Task<Domain.Entities.Genre> UpdateAsync(Domain.Entities.Genre genre);
    Task DeleteAsync(Domain.Entities.Genre genre);
    Task<Domain.Entities.Genre?> GetAsync(Guid id);
    Task<Domain.Entities.Genre?> GetAsync(string value);
    Task<IEnumerable<Domain.Entities.Genre>> GetAsync(IEnumerable<Guid> ids);
    Task<IEnumerable<Domain.Entities.Genre>> GetAllAsync();
    Task<IEnumerable<Domain.Entities.Genre>> GetAllAsync(int skip, int take, string? search);
    Task<Domain.Entities.Genre?> QueryAsync(Expression<Func<Domain.Entities.Genre, bool>> predicate);
}