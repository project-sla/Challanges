using FastEndpoints;
using FluentValidation;

namespace Challenges.Application.Commands.QuestionType.CreateQuestionType;

public class CreateQuestionTypeValidator : Validator<CreateQuestionTypeCommand>
{
    public CreateQuestionTypeValidator()
    {
        RuleFor(x => x.Value)
            .NotEmpty()
            .WithMessage("Value is required");
    }
}