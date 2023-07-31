using FastEndpoints;
using FluentValidation;

namespace Challenges.Application.Commands.Answer.GetAnswers;

public class GetAnswersValidator : Validator<GetAnswersCommand>
{
    public GetAnswersValidator()
    {
        RuleFor(x => x.QuestionId)
            .NotEmpty()
            .When(x => x.AnswerId == null)
            .WithMessage("QuestionId or AnswerId must be provided");

        RuleFor(x => x.AnswerId)
            .NotEmpty()
            .When(x => x.QuestionId == null)
            .WithMessage("QuestionId or AnswerId must be provided");
    }
}