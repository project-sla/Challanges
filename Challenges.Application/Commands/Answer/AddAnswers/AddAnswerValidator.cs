using FastEndpoints;
using FluentValidation;

namespace Challenges.Application.Commands.Answer.AddAnswers;

public class AddAnswerValidator : Validator<AddAnswersCommand>
{
    public AddAnswerValidator()
    {
        RuleFor(x => x.Answers)
            .NotEmpty()
            .WithMessage("Answers cannot be empty");
    }
}