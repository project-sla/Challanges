using FastEndpoints;
using FluentValidation;

namespace Challenges.Application.Commands.SurveyType.GetAllSurveyType;

public class GetAllSurveyTypeValidator : Validator<GetAllSurveyTypeCommand>
{
    public GetAllSurveyTypeValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThan(0)
            .WithMessage("PageNumber must be greater than 0");

        RuleFor(x => x.PageSize)
            .GreaterThan(0)
            .WithMessage("PageSize must be greater than 0");
    }
}