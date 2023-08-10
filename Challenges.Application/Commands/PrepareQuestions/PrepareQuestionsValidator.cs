using FastEndpoints;
using FluentValidation;

namespace Challenges.Application.Commands.PrepareQuestions;

public class PrepareQuestionsValidator : Validator<PrepareQuestionsCommand>
{
    public PrepareQuestionsValidator()
    {
        
    }
}