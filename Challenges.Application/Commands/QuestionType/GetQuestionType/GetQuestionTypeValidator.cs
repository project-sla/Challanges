using FastEndpoints;
using FluentValidation;

namespace Challenges.Application.Commands.QuestionType.GetQuestionType;

public class GetQuestionTypeValidator : Validator<GetQuestionTypeCommand>
{
    public GetQuestionTypeValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .When(x => x.SearchTerm is null)
            .WithMessage("Id or SearchTerm must be provided");

        RuleFor(x => x.SearchTerm)
            .NotEmpty()
            .When(x => x.Id is null)
            .WithMessage("Id or SearchTerm must be provided");

        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1)
            .WithMessage("PageNumber must be greater than or equal to 1");

        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1)
            .WithMessage("PageSize must be greater than or equal to 1");
    }
}