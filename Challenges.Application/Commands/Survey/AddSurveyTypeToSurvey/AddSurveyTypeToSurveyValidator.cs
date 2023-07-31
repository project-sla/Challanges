using FastEndpoints;
using FluentValidation;

namespace Challenges.Application.Commands.Survey.AddSurveyTypeToSurvey;

public class AddSurveyTypeToSurveyValidator : Validator<AddSurveyTypeToSurveyCommand>
{
    public AddSurveyTypeToSurveyValidator()
    {
        RuleFor(x => x.SurveyId).NotEmpty().NotEqual(Guid.Empty).WithMessage("SurveyId is required.");
        RuleFor(x => x.SurveyTypeId).NotEmpty().NotEqual(Guid.Empty).WithMessage("SurveyTypeId is required.");
    }
}