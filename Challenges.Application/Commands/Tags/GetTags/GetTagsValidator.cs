using FastEndpoints;
using FluentValidation;

namespace Challenges.Application.Commands.Tags.GetTags;

public class GetTagsValidator : Validator<GetTagsCommand>
{
    public GetTagsValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .When(x => x.Value == null)
            .WithMessage("Id or Value must be provided");

        RuleFor(x => x.Value)
            .NotEmpty()
            .When(x => x.Id == null)
            .WithMessage("Id or Value must be provided");
    }
}