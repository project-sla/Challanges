using FastEndpoints;
using FluentValidation;

namespace Challenges.Application.Commands.Genre.CreateGenre;

public class CreateGenreValidator : Validator<CreateGenreCommand>
{
    public CreateGenreValidator()
    {
        RuleFor(x => x.Value).NotEmpty().MinimumLength(2).MaximumLength(36).Matches(@"^[a-zA-Z0-9_]*$").WithMessage("Genre name must be alphanumeric and contain no spaces.");
        RuleFor(x => x.CreatedBy).NotEmpty().WithMessage("CreatedBy is required.");
    }
}