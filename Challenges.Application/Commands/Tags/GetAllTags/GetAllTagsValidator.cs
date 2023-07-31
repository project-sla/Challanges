using FastEndpoints;
using FluentValidation;

namespace Challenges.Application.Commands.Tags.GetAllTags;

public class GetAllTagsValidator : Validator<GetAllTagsCommand>
{
    public GetAllTagsValidator()
    {
        RuleFor(x => x.PageNumber).GreaterThanOrEqualTo(1);
        RuleFor(x => x.PageSize).GreaterThanOrEqualTo(1);
        RuleFor(x => x.SearchTerm).MaximumLength(36).When(x => x.SearchTerm != null).WithMessage("Search term must be less than 36 characters.");
    }
}