using Challenges.Application.Commands.Genre.GetGenre;
using FastEndpoints;

namespace Challenges.Application.Mappings.Genre;

public class GetGenreMapper : IMapper
{
    public static GetGenreResponse ToResponseEntity(GetGenreResponse genre)
    {
        return genre;
    }
}