using FastEndpoints;
using FluentValidation;

namespace Challenges.Application.Commands.Question.CreateQuestions;

public class CreateQuestionsValidator : Validator<CreateQuestionsCommand>
{
    public CreateQuestionsValidator()
    {
        RuleFor(x => x.Questions)
            .NotEmpty()
            .WithMessage("Questions cannot be empty");
    }
}