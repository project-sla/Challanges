using Challenges.Domain.Entities;
using Challenges.Persistence.Services;
using Challenges.Persistence.Services.Genre;

namespace Challenges.Application.Business;

public class GenreBusinessRules
{
    private readonly IGenreService _genreService;
    public GenreBusinessRules(IGenreService genreService)
    {
        _genreService = genreService;
    }

    public async Task<bool> CheckGenre(Guid id)
    {
        var existingGenre = await _genreService.GetAsync(id);
        return existingGenre is not null;
    }
}