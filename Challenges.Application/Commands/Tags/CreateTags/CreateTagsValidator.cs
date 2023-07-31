using FastEndpoints;
using FluentValidation;

namespace Challenges.Application.Commands.Tags.CreateTags;

public class CreateTagsValidator : Validator<CreateTagsCommand>
{
    public CreateTagsValidator()
    {
        RuleFor(x => x.Value)
            .NotEmpty()
            .WithMessage("Value must be provided");
    }
}