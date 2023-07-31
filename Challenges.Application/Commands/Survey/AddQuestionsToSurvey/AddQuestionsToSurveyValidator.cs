using FastEndpoints;
using FluentValidation;

namespace Challenges.Application.Commands.Survey.AddQuestionsToSurvey;

public class AddQuestionsToSurveyValidator : Validator<AddQuestionsToSurveyCommand>
{
    public AddQuestionsToSurveyValidator()
    {
        RuleFor(x => x.SurveyId).NotEmpty().NotEqual(Guid.Empty).NotNull().WithMessage("SurveyId is required.");
        RuleFor(x => x.Questions).NotEmpty().NotNull().WithMessage("Questions are required.");
    }
}