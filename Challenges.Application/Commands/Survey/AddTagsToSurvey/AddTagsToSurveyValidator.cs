using FastEndpoints;
using FluentValidation;

namespace Challenges.Application.Commands.Survey.AddTagsToSurvey;

public class AddTagsToSurveyValidator : Validator<AddTagsToSurveyCommand>
{
    public AddTagsToSurveyValidator()
    {
        RuleFor(x => x.SurveyId).NotEmpty().NotEqual(Guid.Empty).WithMessage("SurveyId is required.");
        RuleFor(x => x.TagIds).NotEmpty().WithMessage("TagIds is required.");
    }
}