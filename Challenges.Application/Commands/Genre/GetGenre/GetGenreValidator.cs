using FastEndpoints;
using FluentValidation;

namespace Challenges.Application.Commands.Genre.GetGenre;

public class GetGenreValidator : Validator<GetGenreCommand>
{
    public GetGenreValidator()
    {
        RuleFor(e=>e.Id).NotEmpty().NotNull().WithMessage("Id is required");
    }
}