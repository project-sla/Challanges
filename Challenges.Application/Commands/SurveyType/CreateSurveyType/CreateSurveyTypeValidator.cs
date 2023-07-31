using FastEndpoints;
using FluentValidation;

namespace Challenges.Application.Commands.SurveyType.CreateSurveyType;

public class CreateSurveyTypeValidator : Validator<CreateSurveyTypeCommand>
{
    public CreateSurveyTypeValidator()
    {
        RuleFor(e => e.SurveyTypeData.Value).NotEmpty().NotNull().WithMessage("Value is required");
    }
}