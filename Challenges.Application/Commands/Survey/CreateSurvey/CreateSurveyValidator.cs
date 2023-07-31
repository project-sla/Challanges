using FastEndpoints;
using FluentValidation;

namespace Challenges.Application.Commands.Survey.CreateSurvey;

public class CreateSurveyValidator : Validator<CreateSurveyCommand>
{
    public CreateSurveyValidator()
    {
        RuleFor(x => x.SurveyData.SurveyType)
            .NotNull()
            .WithMessage("Survey type cannot be null");

        RuleFor(x => x.SurveyData.SurveyType.Id)
            .NotNull()
            .WithMessage("Survey type id cannot be null");

        RuleFor(x => x.SurveyData.Content)
            .NotNull()
            .WithMessage("Survey content cannot be null");

        RuleFor(x => x.SurveyData.CreatedBy)
            .NotNull()
            .WithMessage("Survey created by cannot be null");
    }
}