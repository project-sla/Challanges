using FastEndpoints;
using FluentValidation;

namespace Challenges.Application.Commands.QuestionType.GetQuestionType;

public class GetQuestionTypeValidator : Validator<GetQuestionTypeCommand>
{
    public GetQuestionTypeValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1)
            .WithMessage("PageNumber must be greater than or equal to 1");

        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1)
            .WithMessage("PageSize must be greater than or equal to 1");
    }
}