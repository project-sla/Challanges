using FastEndpoints;
using FluentValidation;

namespace Challenges.Application.Commands.SurveyType.GetSurveyType;

public class GetSurveyTypeValidator : Validator<GetSurveyTypeCommand>
{
    public GetSurveyTypeValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .When(x => x.Value == null)
            .WithMessage("Id or Value must be provided");

        RuleFor(x => x.Value)
            .NotEmpty()
            .When(x => x.Id == null)
            .WithMessage("Id or Value must be provided");
    }
}