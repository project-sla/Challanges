using FastEndpoints;
using FluentValidation;

namespace Challenges.Application.Commands.Question.GetQuestions;

public class GetQuestionsValidator : Validator<GetQuestionsCommand>
{
    public GetQuestionsValidator()
    {
        RuleFor(x => x.Page)
            .GreaterThanOrEqualTo(1)
            .WithMessage("PageNumber must be greater than or equal to 1");

        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1)
            .WithMessage("PageSize must be greater than or equal to 1");
    
    }
}